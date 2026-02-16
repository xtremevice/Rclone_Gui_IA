namespace RcloneGui.Core.Models;

/// <summary>
/// Represents a bisync operation configuration
/// </summary>
public class BisyncOperation
{
    public string Name { get; set; } = string.Empty;
    public string SourceRemote { get; set; } = string.Empty;
    public string SourcePath { get; set; } = string.Empty;
    public string DestinationRemote { get; set; } = string.Empty;
    public string DestinationPath { get; set; } = string.Empty;
    public BisyncOptions Options { get; set; } = new();
    public DateTime? LastRun { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Options for bisync operations
/// </summary>
public class BisyncOptions
{
    public bool Resync { get; set; }
    public bool CheckAccess { get; set; } = true;
    public bool CreateEmptySrcDirs { get; set; }
    public bool RemoveEmptyDirs { get; set; }
    public int CheckSync { get; set; } = 1;
    public bool DryRun { get; set; }
    public bool Force { get; set; }
    public List<string> Filters { get; set; } = new();
    public int MaxDelete { get; set; } = 50;
    public string ConflictResolve { get; set; } = "none"; // none, newer, older, larger, smaller, path1, path2
    public bool Compare { get; set; } = true;
    public string CompareFlag { get; set; } = "size,modtime"; // size, modtime, checksum
    public bool IgnoreListingChecksum { get; set; }
    public bool Resilient { get; set; }
    public int RecoverMaxTime { get; set; }
    public Dictionary<string, string> CustomArguments { get; set; } = new();
}

/// <summary>
/// Result of a bisync operation
/// </summary>
public class BisyncResult
{
    public bool Success { get; set; }
    public string Output { get; set; } = string.Empty;
    public string ErrorOutput { get; set; } = string.Empty;
    public int FilesTransferred { get; set; }
    public int ErrorsEncountered { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime ExecutedAt { get; set; }
}
