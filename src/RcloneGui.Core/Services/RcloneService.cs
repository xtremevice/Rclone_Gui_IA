using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RcloneGui.Core.Models;

namespace RcloneGui.Core.Services;

/// <summary>
/// Interface for Rclone command execution
/// </summary>
public interface IRcloneService
{
    Task<List<RemoteAccount>> GetConfiguredRemotesAsync();
    Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request);
    Task<AuthenticationResult> AuthenticateWithBrowserAsync(AuthenticationRequest request);
    Task<bool> DeleteRemoteAsync(string remoteName);
    Task<bool> TestRemoteAsync(string remoteName);
    Task<string> GetRcloneVersionAsync();
    Task<BisyncResult> RunBisyncAsync(BisyncOperation operation);
    string GenerateBisyncCommand(BisyncOperation operation, bool forMacSilicon = false);
    Task<List<string>> ListRemotePathsAsync(string remoteName, string path = "");
}

/// <summary>
/// Service for interacting with rclone CLI
/// </summary>
public class RcloneService : IRcloneService
{
    private readonly string _rclonePath;

    public RcloneService()
    {
        // Try to find rclone in PATH or use default location
        _rclonePath = FindRclonePath();
    }

    private string FindRclonePath()
    {
        // Check if rclone is in PATH
        var processStartInfo = new ProcessStartInfo
        {
            FileName = OperatingSystem.IsWindows() ? "where" : "which",
            Arguments = "rclone",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using var process = Process.Start(processStartInfo);
            if (process != null)
            {
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output))
                {
                    return output.Split('\n')[0].Trim();
                }
            }
        }
        catch
        {
            // Ignore errors and use default
        }

        return "rclone"; // Assume it's in PATH
    }

    public async Task<List<RemoteAccount>> GetConfiguredRemotesAsync()
    {
        var remotes = new List<RemoteAccount>();

        try
        {
            var output = await ExecuteRcloneCommandAsync("listremotes");
            if (string.IsNullOrWhiteSpace(output))
                return remotes;

            var remoteNames = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var remoteName in remoteNames)
            {
                var name = remoteName.TrimEnd(':');
                var configOutput = await ExecuteRcloneCommandAsync($"config dump {name}");
                
                if (!string.IsNullOrWhiteSpace(configOutput))
                {
                    try
                    {
                        var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(configOutput);
                        if (config != null && config.TryGetValue(name, out var remoteConfig))
                        {
                            remotes.Add(new RemoteAccount
                            {
                                Name = name,
                                Type = remoteConfig.GetValueOrDefault("type", "unknown"),
                                Configuration = remoteConfig,
                                CreatedAt = DateTime.Now,
                                IsActive = true
                            });
                        }
                    }
                    catch
                    {
                        // Skip malformed config
                    }
                }
            }
        }
        catch
        {
            // Return empty list on error
        }

        return remotes;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request)
    {
        var result = new AuthenticationResult();

        try
        {
            var providerInfo = new ProviderService().GetProviderInfo(request.ProviderType);
            if (providerInfo == null)
            {
                result.ErrorMessage = "Unknown provider type";
                return result;
            }

            // Build config command based on provider type
            var configArgs = new StringBuilder($"config create {request.RemoteName} {request.ProviderType}");

            // Add username/password for basic auth providers
            if (providerInfo.AuthType == AuthenticationType.UsernamePassword)
            {
                if (!string.IsNullOrEmpty(request.Username))
                {
                    var userField = providerInfo.Type switch
                    {
                        "ftp" or "sftp" or "webdav" => "user",
                        "mega" or "pcloud" => "user",
                        _ => "username"
                    };
                    configArgs.Append($" {userField} {request.Username}");
                }

                if (!string.IsNullOrEmpty(request.Password))
                {
                    var passField = providerInfo.Type switch
                    {
                        "ftp" or "sftp" or "webdav" => "pass",
                        "mega" or "pcloud" => "pass",
                        _ => "password"
                    };
                    // Obscure the password
                    var obscuredPass = await ObscurePasswordAsync(request.Password);
                    configArgs.Append($" {passField} {obscuredPass}");
                }
            }

            // Add additional parameters
            foreach (var param in request.AdditionalParameters)
            {
                configArgs.Append($" {param.Key} {param.Value}");
            }

            var output = await ExecuteRcloneCommandAsync(configArgs.ToString());
            result.Success = !output.Contains("error", StringComparison.OrdinalIgnoreCase) && 
                           !output.Contains("failed", StringComparison.OrdinalIgnoreCase);
            
            if (!result.Success)
            {
                result.ErrorMessage = output;
            }
            else
            {
                result.ConfigurationData = output;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<AuthenticationResult> AuthenticateWithBrowserAsync(AuthenticationRequest request)
    {
        var result = new AuthenticationResult();

        try
        {
            // For OAuth2 providers, we need to use the config process interactively
            // This will open a browser window automatically
            var configArgs = $"config create {request.RemoteName} {request.ProviderType} config_is_local false";
            
            // Add additional parameters if provided
            foreach (var param in request.AdditionalParameters)
            {
                configArgs += $" {param.Key} {param.Value}";
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _rclonePath,
                    Arguments = configArgs,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = false // Show window for user interaction
                }
            };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            process.OutputDataReceived += (sender, e) => 
            {
                if (!string.IsNullOrEmpty(e.Data))
                    outputBuilder.AppendLine(e.Data);
            };

            process.ErrorDataReceived += (sender, e) => 
            {
                if (!string.IsNullOrEmpty(e.Data))
                    errorBuilder.AppendLine(e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            
            await process.WaitForExitAsync();

            var output = outputBuilder.ToString();
            var error = errorBuilder.ToString();

            result.Success = process.ExitCode == 0;
            result.ConfigurationData = output;
            
            if (!result.Success)
            {
                result.ErrorMessage = string.IsNullOrWhiteSpace(error) ? output : error;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<bool> DeleteRemoteAsync(string remoteName)
    {
        try
        {
            var output = await ExecuteRcloneCommandAsync($"config delete {remoteName}");
            return !output.Contains("error", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> TestRemoteAsync(string remoteName)
    {
        try
        {
            var output = await ExecuteRcloneCommandAsync($"lsd {remoteName}:");
            return !output.Contains("error", StringComparison.OrdinalIgnoreCase) &&
                   !output.Contains("failed", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetRcloneVersionAsync()
    {
        try
        {
            return await ExecuteRcloneCommandAsync("version");
        }
        catch
        {
            return "Rclone not found";
        }
    }

    private async Task<string> ObscurePasswordAsync(string password)
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _rclonePath,
                    Arguments = "obscure -",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            await process.StandardInput.WriteLineAsync(password);
            process.StandardInput.Close();
            
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return output.Trim();
        }
        catch
        {
            return password; // Fallback to plain password
        }
    }

    private async Task<string> ExecuteRcloneCommandAsync(string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _rclonePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        process.Start();
        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        // Return output or error if output is empty
        return string.IsNullOrWhiteSpace(output) ? error : output;
    }

    public async Task<BisyncResult> RunBisyncAsync(BisyncOperation operation)
    {
        var startTime = DateTime.Now;
        var result = new BisyncResult
        {
            ExecutedAt = startTime
        };

        try
        {
            var arguments = BuildBisyncArguments(operation);
            
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _rclonePath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            result.Output = await process.StandardOutput.ReadToEndAsync();
            result.ErrorOutput = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            result.Duration = DateTime.Now - startTime;
            result.Success = process.ExitCode == 0;

            // Parse output for statistics
            ParseBisyncOutput(result);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorOutput = ex.Message;
            result.Duration = DateTime.Now - startTime;
        }

        return result;
    }

    public string GenerateBisyncCommand(BisyncOperation operation, bool forMacSilicon = false)
    {
        var arguments = BuildBisyncArguments(operation);
        var rcloneCommand = forMacSilicon ? "/opt/homebrew/bin/rclone" : "rclone";
        return $"{rcloneCommand} {arguments}";
    }

    public async Task<List<string>> ListRemotePathsAsync(string remoteName, string path = "")
    {
        var paths = new List<string>();

        try
        {
            var remotePath = string.IsNullOrEmpty(path) 
                ? $"{remoteName}:" 
                : $"{remoteName}:{path}";
            
            var output = await ExecuteRcloneCommandAsync($"lsf --dirs-only {remotePath}");
            
            if (!string.IsNullOrWhiteSpace(output))
            {
                paths = output.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                              .Select(p => p.TrimEnd('/'))
                              .ToList();
            }
        }
        catch
        {
            // Return empty list on error
        }

        return paths;
    }

    private string BuildBisyncArguments(BisyncOperation operation)
    {
        var args = new StringBuilder("bisync");

        // Add source and destination
        var source = string.IsNullOrEmpty(operation.SourcePath)
            ? $"{operation.SourceRemote}:"
            : $"{operation.SourceRemote}:{operation.SourcePath}";

        var destination = string.IsNullOrEmpty(operation.DestinationPath)
            ? $"{operation.DestinationRemote}:"
            : $"{operation.DestinationRemote}:{operation.DestinationPath}";

        args.Append($" \"{source}\" \"{destination}\"");

        // Add options
        var options = operation.Options;

        if (options.Resync)
            args.Append(" --resync");

        if (options.CheckAccess)
            args.Append(" --check-access");

        if (options.CreateEmptySrcDirs)
            args.Append(" --create-empty-src-dirs");

        if (options.RemoveEmptyDirs)
            args.Append(" --remove-empty-dirs");

        if (options.CheckSync > 0)
            args.Append($" --check-sync={options.CheckSync}");

        if (options.DryRun)
            args.Append(" --dry-run");

        if (options.Force)
            args.Append(" --force");

        if (options.MaxDelete > 0)
            args.Append($" --max-delete {options.MaxDelete}");

        if (!string.IsNullOrEmpty(options.ConflictResolve) && options.ConflictResolve != "none")
            args.Append($" --conflict-resolve {options.ConflictResolve}");

        if (options.Compare)
        {
            if (!string.IsNullOrEmpty(options.CompareFlag))
                args.Append($" --compare {options.CompareFlag}");
        }

        if (options.IgnoreListingChecksum)
            args.Append(" --ignore-listing-checksum");

        if (options.Resilient)
            args.Append(" --resilient");

        if (options.RecoverMaxTime > 0)
            args.Append($" --recover --max-time {options.RecoverMaxTime}s");

        // Add filters
        foreach (var filter in options.Filters)
        {
            args.Append($" --filter \"{filter}\"");
        }

        // Add custom arguments
        foreach (var kvp in options.CustomArguments)
        {
            if (string.IsNullOrEmpty(kvp.Value))
                args.Append($" --{kvp.Key}");
            else
                args.Append($" --{kvp.Key} \"{kvp.Value}\"");
        }

        // Add verbose flag for better output
        args.Append(" --verbose");

        return args.ToString();
    }

    private void ParseBisyncOutput(BisyncResult result)
    {
        if (string.IsNullOrWhiteSpace(result.Output))
            return;

        var lines = result.Output.Split('\n');
        foreach (var line in lines)
        {
            if (line.Contains("Transferred:") && line.Contains("/"))
            {
                var parts = line.Split('/');
                if (parts.Length > 0 && int.TryParse(parts[0].Trim().Split(' ').Last(), out int transferred))
                {
                    result.FilesTransferred = transferred;
                }
            }
            else if (line.Contains("Errors:") || line.Contains("ERROR"))
            {
                result.ErrorsEncountered++;
            }
        }
    }
}
