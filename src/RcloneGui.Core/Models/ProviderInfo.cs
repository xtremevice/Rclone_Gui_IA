namespace RcloneGui.Core.Models;

/// <summary>
/// Represents information about a cloud storage provider
/// </summary>
public class ProviderInfo
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AuthenticationType AuthType { get; set; }
    public bool RequiresBrowser { get; set; }
    public List<string> RequiredFields { get; set; } = new();
    public string IconPath { get; set; } = string.Empty;
}

/// <summary>
/// Represents the type of authentication required
/// </summary>
public enum AuthenticationType
{
    None,
    UsernamePassword,
    OAuth2,
    ApiKey,
    Token
}
