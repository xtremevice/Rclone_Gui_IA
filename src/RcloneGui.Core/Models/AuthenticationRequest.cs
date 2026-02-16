namespace RcloneGui.Core.Models;

/// <summary>
/// Represents an authentication request
/// </summary>
public class AuthenticationRequest
{
    public string ProviderType { get; set; } = string.Empty;
    public string RemoteName { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Dictionary<string, string> AdditionalParameters { get; set; } = new();
}

/// <summary>
/// Represents the result of an authentication attempt
/// </summary>
public class AuthenticationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public Dictionary<string, string> Tokens { get; set; } = new();
    public string? ConfigurationData { get; set; }
}
