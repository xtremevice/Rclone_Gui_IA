# Bisync UI Overview

## Navigation

The application now has three main sections accessible from the navigation panel:

```
┌──────────────────────────────────────────────────────────────┐
│  📁 My Remotes     │  Main view to manage cloud accounts     │
│  ➕ Add Account    │  Add new cloud storage providers        │
│  ⇄ Bisync         │  NEW: Bidirectional sync operations     │
└──────────────────────────────────────────────────────────────┘
```

## Bisync View Layout

```
┌─────────────────────────────────────────────────────────────────┐
│  Bisync Operations                                              │
│  Create and manage bidirectional sync operations between remotes│
│ ─────────────────────────────────────────────────────────────── │
│                                                                  │
│  [Saved Operations List]                                        │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │ • Sync Photos      OneDrive ⇄ GoogleDrive               │  │
│  │ • Backup Documents Local ⇄ Dropbox                      │  │
│  └──────────────────────────────────────────────────────────┘  │
│  [📥 Load Selected] [🗑️ Delete Selected]                       │
│                                                                  │
│ ─────────────────────────────────────────────────────────────── │
│                                                                  │
│  Operation Configuration                                        │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │ Operation Name: [____________________________]           │  │
│  │                                                           │  │
│  │ Source Remote *:  [OneDrive          ▼]                  │  │
│  │ Source Path:      [/Photos          ] [📁 Browse]        │  │
│  │                                                           │  │
│  │ Destination Remote *: [GoogleDrive   ▼]                  │  │
│  │ Destination Path: [/Photos          ] [📁 Browse]        │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                  │
│ ─────────────────────────────────────────────────────────────── │
│                                                                  │
│  Bisync Options                                                 │
│  ┌────────────────────────────┬────────────────────────────┐   │
│  │ ☑ Resync (first time)      │ ☑ Dry Run (test mode)     │   │
│  │ ☑ Check Access             │ ☐ Force                   │   │
│  │ ☐ Create Empty Source Dirs │                           │   │
│  │ ☐ Remove Empty Dirs        │                           │   │
│  └────────────────────────────┴────────────────────────────┘   │
│                                                                  │
│  Max Delete Threshold: [50] %                                   │
│  Conflict Resolution:  [newer           ▼]                      │
│  Compare Method:       [size,modtime    ]                       │
│                                                                  │
│ ─────────────────────────────────────────────────────────────── │
│                                                                  │
│  Generated Command                                              │
│  ☑ Generate for Mac Silicon (Apple M1/M2/M3)                   │
│  [⚙️ Generate Command]                                          │
│                                                                  │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │ /opt/homebrew/bin/rclone bisync "OneDrive:/Photos"      │  │
│  │ "GoogleDrive:/Photos" --resync --check-access            │  │
│  │ --max-delete 50 --conflict-resolve newer                 │  │
│  │ --compare size,modtime --verbose                         │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                  │
│ ─────────────────────────────────────────────────────────────── │
│                                                                  │
│  [▶️ Run Bisync Now] [💾 Save Operation] [🗑️ Clear] [🔄 Refresh]│
│                                                                  │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │ ℹ️ Bisync completed successfully! Transferred 127 files  │  │
│  │    in 45.3s                                              │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                  │
│  ╔════════════════════════════════════════════════════════════╗│
│  ║ ℹ️ Bisync Tips                                             ║│
│  ║ • First time: Enable "Resync" to initialize               ║│
│  ║ • Use "Dry Run" to test before making changes             ║│
│  ║ • Set conflict resolution strategy                         ║│
│  ║ • Mac Silicon: Command uses /opt/homebrew/bin/rclone      ║│
│  ╚════════════════════════════════════════════════════════════╝│
└─────────────────────────────────────────────────────────────────┘
```

## Key UI Elements

### 1. Saved Operations Section
- Shows previously saved bisync configurations
- Click to select an operation
- Load button: Restores all settings
- Delete button: Removes saved operation

### 2. Operation Configuration
- **Operation Name**: Give your sync a memorable name
- **Source Remote**: Select from configured remotes
- **Source Path**: Optional path within remote (with browse)
- **Destination Remote**: Target remote for sync
- **Destination Path**: Optional path (with browse)

### 3. Bisync Options
**Left Column:**
- Resync: Initialize/force sync (first time use)
- Check Access: Verify write permissions
- Create Empty Source Dirs: Maintain folder structure
- Remove Empty Dirs: Clean up after sync

**Right Column:**
- Dry Run: Test mode, no actual changes
- Force: Override safety checks

**Additional Options:**
- Max Delete Threshold: Safety limit (default 50%)
- Conflict Resolution: How to handle file conflicts
- Compare Method: How to detect changes

### 4. Command Generation
- Toggle for Mac Silicon path
- Preview button to see exact command
- Command displayed in monospace font
- Can copy and run manually

### 5. Action Buttons
- **Run Bisync Now**: Execute the operation
- **Save Operation**: Store for future use
- **Clear**: Reset all fields
- **Refresh Remotes**: Reload remote list

### 6. Status Display
- Real-time feedback on operations
- Success/error messages
- Statistics (files transferred, duration)

### 7. Help Section
- Quick tips for using bisync
- Important reminders
- Platform-specific notes

## Workflow Examples

### First-Time Sync Setup

```
Step 1: Configure
├── Select source: OneDrive
├── Select destination: GoogleDrive
├── Enter paths (optional)
└── Enable "Resync" ✓

Step 2: Test
├── Enable "Dry Run" ✓
├── Click "Generate Command"
├── Review command
└── Click "Run Bisync Now"

Step 3: Execute
├── Disable "Dry Run"
├── Click "Run Bisync Now"
└── Monitor progress

Step 4: Save
├── Enter operation name
├── Click "Save Operation"
└── Available for future use
```

### Running a Saved Operation

```
Step 1: Load
├── Select from saved operations
└── Click "Load Selected"

Step 2: Review
├── Check settings are correct
└── Update if needed

Step 3: Execute
├── Click "Run Bisync Now"
└── Monitor results
```

## Color Coding

- **Blue (#2196F3)**: Information, primary actions
- **Green (#4CAF50)**: Success, run operations
- **Orange (#FF9800)**: Save, warnings
- **Red (#F44336)**: Delete, errors
- **Gray**: Disabled, neutral actions

## Conflict Resolution Options

| Option   | Behavior                                    |
|----------|---------------------------------------------|
| none     | Show conflicts, don't resolve automatically |
| newer    | Keep the file with latest modification time|
| older    | Keep the file with earliest modification   |
| larger   | Keep the larger file                       |
| smaller  | Keep the smaller file                      |
| path1    | Always prefer source (left side)           |
| path2    | Always prefer destination (right side)     |

## Compare Methods

| Method          | Description                                     |
|-----------------|------------------------------------------------|
| size,modtime    | Compare file size and modification time (fast) |
| size,checksum   | Compare size and calculate checksums (slower)  |
| checksum        | Only use checksums (most accurate, slowest)    |

## Mac Silicon Path

When "Generate for Mac Silicon" is checked:
- Path: `/opt/homebrew/bin/rclone`
- This is the default Homebrew location on Apple Silicon
- Compatible with M1, M2, M3 processors

Standard path (unchecked):
- Path: `rclone`
- Uses system PATH to find rclone

## Tips for Best Results

1. **Always test first** with Dry Run enabled
2. **Use Resync** only on first sync or to reinitialize
3. **Choose conflict resolution** based on your needs
4. **Save operations** you use frequently
5. **Monitor results** especially on first run
6. **Check status messages** for any errors
7. **Copy command** if you want to run manually or schedule

## Platform-Specific Notes

### Mac Silicon (M1/M2/M3)
- Install via: `brew install rclone`
- Location: `/opt/homebrew/bin/rclone`
- Check the Mac Silicon toggle

### Intel Mac / Linux
- Install via package manager
- Usually in `/usr/local/bin/rclone`
- Standard path works

### Windows
- Download from rclone.org
- Add to PATH or use full path
- Standard path typically works

## Integration with Other Features

The bisync feature works seamlessly with:
- **My Remotes**: Uses configured remotes
- **Add Account**: Add remotes to use in bisync
- All authentication methods supported
- Dynamic provider discovery

## Keyboard Shortcuts

While focused on fields:
- **Tab**: Move to next field
- **Shift+Tab**: Move to previous field
- **Enter**: Activate focused button (in some contexts)
- **Space**: Toggle checkboxes

## Accessibility

- Clear labels for all inputs
- Keyboard navigation support
- Color coding with text indicators
- Status messages for screen readers
- Logical tab order

## Summary

The Bisync UI provides:
✅ Complete control over sync operations
✅ Visual configuration without command-line
✅ Safety features (dry-run, max delete)
✅ Mac Silicon optimization
✅ Save/load configurations
✅ Real-time feedback
✅ Command preview
✅ Comprehensive options
✅ Help and documentation
