using RcloneGui.Core.Models;

namespace RcloneGui.Core.Services;

/// <summary>
/// Interface for provider information service
/// </summary>
public interface IProviderService
{
    List<ProviderInfo> GetSupportedProviders();
    ProviderInfo? GetProviderInfo(string type);
}

/// <summary>
/// Service that provides information about supported cloud storage providers
/// </summary>
public class ProviderService : IProviderService
{
    private readonly List<ProviderInfo> _providers;

    public ProviderService()
    {
        _providers = InitializeProviders();
    }

    public List<ProviderInfo> GetSupportedProviders()
    {
        return _providers;
    }

    public ProviderInfo? GetProviderInfo(string type)
    {
        return _providers.FirstOrDefault(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
    }

    private List<ProviderInfo> InitializeProviders()
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
