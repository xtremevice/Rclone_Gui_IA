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
}
