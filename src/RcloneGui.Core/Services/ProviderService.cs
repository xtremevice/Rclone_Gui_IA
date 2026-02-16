using System.Diagnostics;
using RcloneGui.Core.Models;

namespace RcloneGui.Core.Services;

/// <summary>
/// Interface for provider information service
/// </summary>
public interface IProviderService
{
    Task<List<ProviderInfo>> GetSupportedProvidersAsync();
    List<ProviderInfo> GetSupportedProviders();
    ProviderInfo? GetProviderInfo(string type);
}

/// <summary>
/// Service that provides information about supported cloud storage providers
/// </summary>
public class ProviderService : IProviderService
{
    private List<ProviderInfo>? _providers;
    private readonly string _rclonePath;

    public ProviderService()
    {
        _rclonePath = FindRclonePath();
    }

    private string FindRclonePath()
    {
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
            // Ignore errors
        }

        return "rclone";
    }

    public async Task<List<ProviderInfo>> GetSupportedProvidersAsync()
    {
        if (_providers != null)
            return _providers;

        try
        {
            var dynamicProviders = await GetProvidersFromRcloneAsync();
            if (dynamicProviders.Count > 0)
            {
                _providers = dynamicProviders;
                return _providers;
            }
        }
        catch
        {
            // Fall back to hardcoded providers
        }

        _providers = GetFallbackProviders();
        return _providers;
    }

    public List<ProviderInfo> GetSupportedProviders()
    {
        return _providers ?? GetFallbackProviders();
    }

    public ProviderInfo? GetProviderInfo(string type)
    {
        var providers = _providers ?? GetFallbackProviders();
        return providers.FirstOrDefault(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<List<ProviderInfo>> GetProvidersFromRcloneAsync()
    {
        var providers = new List<ProviderInfo>();

        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _rclonePath,
                    Arguments = "config providers",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output))
            {
                var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.StartsWith(" - "))
                    {
                        var parts = line.Substring(3).Split(" - ", 2);
                        if (parts.Length == 2)
                        {
                            var type = parts[0].Trim();
                            var description = parts[1].Trim();
                            
                            providers.Add(new ProviderInfo
                            {
                                Name = FormatProviderName(type),
                                Type = type,
                                Description = description,
                                AuthType = GuessAuthType(type),
                                RequiresBrowser = RequiresBrowserAuth(type),
                                RequiredFields = new List<string>()
                            });
                        }
                    }
                }
            }
        }
        catch
        {
            // Return empty list on error
        }

        return providers;
    }

    private string FormatProviderName(string type)
    {
        return type switch
        {
            "onedrive" => "OneDrive",
            "drive" => "Google Drive",
            "s3" => "Amazon S3",
            "ftp" => "FTP",
            "sftp" => "SFTP",
            "webdav" => "WebDAV",
            "pcloud" => "pCloud",
            _ => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type)
        };
    }

    private AuthenticationType GuessAuthType(string type)
    {
        return type switch
        {
            "onedrive" or "drive" or "dropbox" or "box" => AuthenticationType.OAuth2,
            "s3" => AuthenticationType.ApiKey,
            "ftp" or "sftp" or "webdav" or "mega" or "pcloud" => AuthenticationType.UsernamePassword,
            _ => AuthenticationType.None
        };
    }

    private bool RequiresBrowserAuth(string type)
    {
        return type is "onedrive" or "drive" or "dropbox" or "box";
    }

    private List<ProviderInfo> GetFallbackProviders()
    {
        return new List<ProviderInfo>
        {
            new ProviderInfo
            {
                Name = "OneDrive",
                Type = "onedrive",
                Description = "Microsoft OneDrive cloud storage",
                AuthType = AuthenticationType.OAuth2,
                RequiresBrowser = true,
                RequiredFields = new List<string> { "client_id", "client_secret" }
            },
            new ProviderInfo
            {
                Name = "Google Drive",
                Type = "drive",
                Description = "Google Drive cloud storage",
                AuthType = AuthenticationType.OAuth2,
                RequiresBrowser = true,
                RequiredFields = new List<string> { "client_id", "client_secret" }
            },
            new ProviderInfo
            {
                Name = "Dropbox",
                Type = "dropbox",
                Description = "Dropbox cloud storage",
                AuthType = AuthenticationType.OAuth2,
                RequiresBrowser = true,
                RequiredFields = new List<string>()
            },
            new ProviderInfo
            {
                Name = "Amazon S3",
                Type = "s3",
                Description = "Amazon S3 compatible storage",
                AuthType = AuthenticationType.ApiKey,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "access_key_id", "secret_access_key" }
            },
            new ProviderInfo
            {
                Name = "FTP",
                Type = "ftp",
                Description = "FTP server connection",
                AuthType = AuthenticationType.UsernamePassword,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "host", "user", "pass" }
            },
            new ProviderInfo
            {
                Name = "SFTP",
                Type = "sftp",
                Description = "SSH/SFTP server connection",
                AuthType = AuthenticationType.UsernamePassword,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "host", "user", "pass" }
            },
            new ProviderInfo
            {
                Name = "WebDAV",
                Type = "webdav",
                Description = "WebDAV server connection",
                AuthType = AuthenticationType.UsernamePassword,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "url", "user", "pass" }
            },
            new ProviderInfo
            {
                Name = "Mega",
                Type = "mega",
                Description = "Mega.nz cloud storage",
                AuthType = AuthenticationType.UsernamePassword,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "user", "pass" }
            },
            new ProviderInfo
            {
                Name = "pCloud",
                Type = "pcloud",
                Description = "pCloud cloud storage",
                AuthType = AuthenticationType.UsernamePassword,
                RequiresBrowser = false,
                RequiredFields = new List<string> { "username", "password" }
            },
            new ProviderInfo
            {
                Name = "Box",
                Type = "box",
                Description = "Box cloud storage",
                AuthType = AuthenticationType.OAuth2,
                RequiresBrowser = true,
                RequiredFields = new List<string>()
            }
        };
    }
}
