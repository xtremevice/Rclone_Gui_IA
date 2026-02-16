namespace RcloneGui.Core.Models;

/// <summary>
/// Represents a configured remote account
/// </summary>
public class RemoteAccount
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Dictionary<string, string> Configuration { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public bool IsActive { get; set; } = true;
}
