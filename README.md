# Rclone GUI - Multi-Platform Cloud Storage Manager

A cross-platform graphical user interface for managing Rclone cloud storage accounts, configurations, and bidirectional sync operations.

> 📖 **How to Run?** / **¿Cómo Ejecutar?**
> 
> - 🇪🇸 Spanish: [COMO_EJECUTAR.md](COMO_EJECUTAR.md) - Guía completa de ejecución en todas las plataformas
> - 🇬🇧 English: [HOW_TO_RUN.md](HOW_TO_RUN.md) - Complete execution guide for all platforms
> - 📚 Wiki: [WIKI_COMO_EJECUTAR.md](WIKI_COMO_EJECUTAR.md) - Wiki-ready format
> 
> 🍎 **Mac Silicon Users** / **Usuarios de Mac Silicon**: See [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md) for complete installation guide

## Features

- **Multi-Provider Support**: Configure accounts for various cloud storage providers including:
  - OneDrive (OAuth2 with browser authentication)
  - Google Drive (OAuth2 with browser authentication)
  - Dropbox
  - Amazon S3
  - FTP/SFTP
  - WebDAV
  - Mega
  - pCloud
  - Box
  - And many more (dynamically loaded from rclone)...

- **Bidirectional Sync (Bisync)**: 
  - Set up bidirectional sync between any two remotes
  - Multiple sync operations with save/load functionality
  - Conflict resolution strategies
  - Dry run mode for testing
  - Mac Silicon optimized command generation
  - Custom arguments support

- **User-Friendly Interface**: 
  - Easy-to-use GUI for adding cloud storage accounts
  - Visual management of configured remotes
  - Test and delete remotes with one click
  - Status notifications and real-time feedback
  - Dynamic provider loading from rclone

- **Multiple Authentication Methods**:
  - Username/Password authentication
  - OAuth2 browser-based authentication (for OneDrive, Google Drive, etc.)
  - API Key authentication (for S3 and similar services)
  - Automatic token management

- **Cross-Platform**: Built with Avalonia UI, runs on:
  - Windows
  - macOS (including Apple Silicon M1/M2/M3)
  - Linux

## Prerequisites

1. **.NET 8.0 SDK or later**: Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. **Rclone**: Download and install from [https://rclone.org/downloads/](https://rclone.org/downloads/)
   - Make sure `rclone` is in your system PATH
   - Or place the rclone executable in a known location

## Building the Application

### Clone the Repository

```bash
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA
```

### Build the Solution

```bash
dotnet build
```

### Run the Application

```bash
dotnet run --project src/RcloneGui/RcloneGui.csproj
```

## Usage

### Adding a New Account

1. Click on "Add Account" in the navigation menu
2. Select your cloud storage provider from the dropdown
3. Enter a name for your remote (e.g., "MyDrive", "BackupStorage")
4. Depending on the provider type:
   - **For OAuth2 providers** (OneDrive, Google Drive, Dropbox): Click "Add Account" and a browser window will open for authentication
   - **For username/password providers** (FTP, SFTP, Mega, pCloud): Enter your credentials
   - **For API-based providers** (S3): Enter your access keys
5. Click "Add Account" to save the configuration

### Managing Remotes

1. Click on "My Remotes" in the navigation menu
2. View all your configured remotes
3. Select a remote to:
   - Test the connection
   - Delete the remote
4. Click "Refresh" to reload the list of remotes

### Testing a Remote

1. Go to "My Remotes"
2. Select a remote from the list
3. Click "Test Selected"
4. The status message will show if the remote is working correctly

### Deleting a Remote

1. Go to "My Remotes"
2. Select a remote from the list
3. Click "Delete Selected"
4. The remote will be removed from your rclone configuration

### Setting Up Bisync

1. Click on "Bisync" in the navigation menu
2. Select source and destination remotes
3. Optionally specify paths within remotes
4. Configure bisync options:
   - Enable "Resync" for first-time setup
   - Set conflict resolution strategy
   - Choose comparison method
   - Configure other options as needed
5. Click "Generate Command" to preview the command
6. Click "Run Bisync Now" to execute the sync
7. Optionally save the configuration for future use

For detailed bisync documentation, see [BISYNC_GUIDE.md](BISYNC_GUIDE.md).

## Project Structure

```
Rclone_Gui_IA/
├── src/
│   ├── RcloneGui/              # Main Avalonia UI application
│   │   ├── ViewModels/         # MVVM ViewModels
│   │   ├── Views/              # XAML Views
│   │   └── Assets/             # Application assets
│   └── RcloneGui.Core/         # Core business logic
│       ├── Models/             # Data models
│       └── Services/           # Services for Rclone interaction
├── RcloneGui.slnx              # Solution file
└── README.md                   # This file
```

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

- **Models**: Define data structures for providers, accounts, and authentication
- **Services**: Handle interaction with the Rclone CLI
- **ViewModels**: Provide data and commands for the UI
- **Views**: XAML-based user interface

### Key Components

- **ProviderService**: Manages information about supported cloud storage providers
- **RcloneService**: Executes Rclone commands and manages remotes
- **AddAccountViewModel**: Handles adding new cloud storage accounts
- **RemotesViewModel**: Manages the list of configured remotes

## Supported Providers

The application supports numerous cloud storage providers through Rclone:

| Provider | Authentication Type | Browser Required |
|----------|---------------------|------------------|
| OneDrive | OAuth2 | Yes |
| Google Drive | OAuth2 | Yes |
| Dropbox | OAuth2 | Yes |
| Box | OAuth2 | Yes |
| Amazon S3 | API Key | No |
| FTP | Username/Password | No |
| SFTP | Username/Password | No |
| WebDAV | Username/Password | No |
| Mega | Username/Password | No |
| pCloud | Username/Password | No |

## Development

### Requirements

- .NET 8.0 SDK
- Visual Studio 2022, Rider, or VS Code
- Avalonia UI templates

### Building for Release

```bash
dotnet publish -c Release -r win-x64 --self-contained
dotnet publish -c Release -r linux-x64 --self-contained
dotnet publish -c Release -r osx-x64 --self-contained
```

## Troubleshooting

### "Rclone not found" error

Make sure Rclone is installed and available in your system PATH. Test by running:

```bash
rclone version
```

### OAuth2 authentication fails

- Ensure you have a web browser installed
- Check your internet connection
- Some providers may require you to register your own OAuth2 application

### Permission errors

On Linux/macOS, you may need to make the rclone binary executable:

```bash
chmod +x /path/to/rclone
```

## Platform-Specific Guides

### Execution Guides
- **Spanish**: [COMO_EJECUTAR.md](COMO_EJECUTAR.md) - Guía completa para ejecutar en Windows, Linux y macOS
- **English**: [HOW_TO_RUN.md](HOW_TO_RUN.md) - Complete guide for running on Windows, Linux, and macOS
- **Wiki Format**: [WIKI_COMO_EJECUTAR.md](WIKI_COMO_EJECUTAR.md) - Ready to copy to GitHub Wiki

### Platform-Specific Documentation
- **Mac Silicon (M1/M2/M3)**: [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md) - Complete setup guide in Spanish
- **Quick Start**: [QUICKSTART.md](QUICKSTART.md) - General quick start guide
- **Bisync Guide**: [BISYNC_GUIDE.md](BISYNC_GUIDE.md) - Bidirectional sync documentation

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues.

## License

This project is provided as-is for educational and personal use.

## Credits

- Built with [Avalonia UI](https://avaloniaui.net/)
- Powered by [Rclone](https://rclone.org/)
- Uses [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) for MVVM implementation

## Acknowledgments

This project was created to provide an accessible, multi-platform interface for managing Rclone cloud storage configurations.

