# Bisync Feature Guide

## Overview

The Bisync feature in Rclone GUI enables bidirectional synchronization between two remotes or between a remote and local storage. This is particularly useful for keeping files synchronized across multiple cloud services or between cloud and local storage.

## What is Bisync?

Bisync (bidirectional sync) is a powerful rclone feature that:
- Synchronizes changes in both directions
- Detects and handles conflicts
- Maintains file integrity across locations
- Supports incremental syncing

## Getting Started

### Prerequisites

1. **Rclone installed**: Version 1.58 or later recommended
2. **Configured remotes**: At least one remote configured (see "Add Account")
3. **First-time setup**: Use "Resync" mode for initial synchronization

### Basic Workflow

1. **Navigate to Bisync**: Click "Bisync" in the navigation menu
2. **Select remotes**: Choose source and destination remotes
3. **Configure options**: Set sync options based on your needs
4. **Generate command**: Preview the command before running
5. **Run bisync**: Execute the synchronization

## Configuration Options

### Remote Selection

**Source Remote**
- The first location to sync
- Can be a cloud remote or local path

**Source Path** (Optional)
- Specific folder within the remote
- Leave empty to sync the entire remote
- Example: `/photos`, `/documents/work`

**Destination Remote**
- The second location to sync
- Can be different from source remote

**Destination Path** (Optional)
- Specific folder within the destination
- Should typically match source path structure

### Bisync Options

#### Essential Options

**Resync** (First-time setup)
- **When to use**: First time syncing between two locations
- **What it does**: Copies all files from both sides and creates initial state
- **Important**: Enables forced sync, may overwrite files
- Example: Initializing sync between OneDrive and Google Drive

**Check Access**
- **Default**: Enabled
- **What it does**: Verifies you have write access to both locations
- **Recommendation**: Keep enabled for safety

**Dry Run** (Test mode)
- **When to use**: Testing configuration before making changes
- **What it does**: Shows what would happen without actually changing files
- **Recommendation**: Always test first with complex setups

#### Advanced Options

**Create Empty Source Dirs**
- Creates empty directories from source on destination
- Useful for maintaining folder structure

**Remove Empty Dirs**
- Automatically removes empty directories after sync
- Keeps storage clean

**Force**
- Bypasses certain safety checks
- Use with caution

**Max Delete Threshold**
- **Default**: 50%
- **What it does**: Prevents accidental mass deletions
- **Example**: If more than 50% of files would be deleted, bisync stops
- **Recommendation**: Keep at 50% or lower for safety

**Conflict Resolution**
- **none** (default): Shows conflicts without resolving
- **newer**: Keeps the newer file
- **older**: Keeps the older file
- **larger**: Keeps the larger file
- **smaller**: Keeps the smaller file
- **path1**: Always prefer source
- **path2**: Always prefer destination

**Compare Method**
- **size,modtime** (default): Compare by size and modification time
- **size,checksum**: Compare by size and checksum (slower but more accurate)
- **checksum**: Compare only by checksum

## Mac Silicon Support

### Special Configuration

For Mac with Apple Silicon (M1, M2, M3):
1. Check "Generate for Mac Silicon"
2. Command will use `/opt/homebrew/bin/rclone` path
3. This is the default Homebrew installation path on Apple Silicon

### Installation on Mac Silicon

```bash
# Install rclone via Homebrew
brew install rclone

# Verify installation
/opt/homebrew/bin/rclone version
```

## Usage Examples

### Example 1: Sync Personal Photos

**Scenario**: Sync photos between OneDrive and Google Drive

```
Source Remote: OneDrive
Source Path: /Photos
Destination Remote: GoogleDrive  
Destination Path: /Photos
Options:
  - Resync: Yes (first time only)
  - Check Access: Yes
  - Dry Run: Yes (first run to test)
  - Conflict Resolve: newer
  - Compare: size,modtime
```

**Steps**:
1. First run with Resync + Dry Run to see what will happen
2. Second run with Resync (no Dry Run) to initialize
3. Future runs without Resync for incremental sync

### Example 2: Backup Important Documents

**Scenario**: Backup local documents to cloud

```
Source Remote: Local (if configured)
Source Path: /home/user/Documents
Destination Remote: Dropbox
Destination Path: /Backup/Documents
Options:
  - Resync: Yes (first time)
  - Check Access: Yes
  - Max Delete: 25 (more conservative)
  - Conflict Resolve: newer
```

### Example 3: Multi-Cloud Redundancy

**Scenario**: Keep files synchronized across three cloud providers

```
Operation 1: OneDrive ⇄ GoogleDrive
Operation 2: GoogleDrive ⇄ Dropbox
Operation 3: Dropbox ⇄ OneDrive

Schedule: Run all three operations sequentially
Result: Files synchronized across all three services
```

## Saved Operations

### Saving Operations

1. Configure your bisync settings
2. Enter an "Operation Name"
3. Click "Save Operation"
4. Operation is saved for future use

### Loading Saved Operations

1. View saved operations in the list
2. Select an operation
3. Click "Load Selected"
4. All settings are restored

### Deleting Operations

1. Select the operation
2. Click "Delete Selected"
3. Operation is removed from list

## Command Generation

### Preview Before Running

1. Configure your bisync settings
2. Click "Generate Command"
3. Review the generated command
4. Copy and run manually if preferred

### Manual Execution

Generated commands can be:
- Copied to terminal for manual execution
- Added to scripts for automation
- Scheduled with cron (Linux/Mac) or Task Scheduler (Windows)

**Example Generated Command**:
```bash
/opt/homebrew/bin/rclone bisync "OneDrive:/Photos" "GoogleDrive:/Photos" --check-access --max-delete 50 --conflict-resolve newer --compare size,modtime --verbose
```

## Best Practices

### Safety First

1. **Always test with Dry Run** before running for real
2. **Start with Resync** on first sync
3. **Use conservative Max Delete** values (25-50%)
4. **Backup important data** before syncing

### Performance

1. **Use size,modtime** for faster syncing (vs checksum)
2. **Sync specific paths** instead of entire remotes when possible
3. **Avoid syncing large files** that change frequently

### Conflict Management

1. **Choose appropriate conflict resolution** for your use case
2. **Review conflicts** before using automatic resolution
3. **Document your strategy** for team environments

### Scheduling

For automated syncing:

**Linux/Mac (cron)**:
```bash
# Edit crontab
crontab -e

# Add entry to run daily at 2 AM
0 2 * * * /opt/homebrew/bin/rclone bisync "Remote1:/path" "Remote2:/path" --check-access
```

**Windows (Task Scheduler)**:
1. Open Task Scheduler
2. Create Basic Task
3. Set trigger (daily, hourly, etc.)
4. Set action: Run rclone with bisync command

## Troubleshooting

### Common Issues

**"Bisync failed: not found"**
- Ensure rclone is installed and in PATH
- On Mac Silicon, use the generated command with full path

**"Exceeded max-delete threshold"**
- Too many files would be deleted
- Review changes carefully
- Increase threshold if intentional
- May indicate sync state corruption

**"Conflict detected"**
- Files changed on both sides
- Set conflict resolution strategy
- Manually resolve if needed

**"Check access failed"**
- Verify remote credentials
- Check internet connection
- Ensure write permissions

### Recovery

If bisync fails:
1. Review error messages
2. Check remote connectivity
3. Run with `--resync` to reinitialize
4. Contact support if persistent

## Advanced Features

### Custom Arguments

The bisync view supports custom rclone arguments:
- Add via the Options.CustomArguments dictionary in code
- Useful for provider-specific flags
- Example: `--drive-chunk-size` for Google Drive

### Filters

Add file filters to include/exclude specific files:
- Example: `--filter "+ *.jpg"` (include only JPG)
- Example: `--filter "- *.tmp"` (exclude temp files)

## Safety Features

### Built-in Protections

1. **Max Delete Threshold**: Prevents mass deletions
2. **Check Access**: Verifies permissions before sync
3. **Dry Run Mode**: Test without changes
4. **Verbose Output**: Detailed logging for review

### Recommendations

- Keep backups of critical data
- Test configurations thoroughly
- Monitor initial syncs closely
- Review logs for errors

## Further Reading

- [Rclone Bisync Documentation](https://rclone.org/bisync/)
- [Rclone Filters Guide](https://rclone.org/filtering/)
- [Rclone Configuration](https://rclone.org/docs/)

## Support

For issues specific to this GUI:
- GitHub Issues: https://github.com/xtremevice/Rclone_Gui_IA/issues

For rclone-specific questions:
- Rclone Forum: https://forum.rclone.org/
- Rclone Documentation: https://rclone.org/
