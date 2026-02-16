# Changelog

All notable changes to Rclone GUI will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2024-02-16

### Added
- **Bisync functionality**: Full bidirectional sync support
  - Create and manage bisync operations between any two remotes
  - Save and load bisync configurations
  - Comprehensive sync options (resync, dry-run, conflict resolution, etc.)
  - Real-time sync execution with progress feedback
  - Command preview before execution
- **Mac Silicon support**: Optimized command generation for Apple M1/M2/M3
  - Automatic path detection for Homebrew installations
  - Toggle for Mac Silicon specific commands
- **Dynamic provider loading**: Providers are now loaded from `rclone config providers`
  - Automatic discovery of all supported providers
  - Fallback to hardcoded list if rclone unavailable
  - Always up-to-date with latest rclone capabilities
- **Remote path browsing**: Browse folders within remotes
- **Custom arguments support**: Add custom rclone arguments to operations
- **Bisync options**:
  - Conflict resolution strategies (newer, older, larger, smaller, path1, path2)
  - Compare methods (size, modtime, checksum)
  - Max delete threshold protection
  - Dry run mode for testing
  - Force and resilient modes
  - Filter support

### Changed
- Provider service now queries rclone dynamically instead of using hardcoded list
- Updated navigation to include Bisync view
- Enhanced UI with additional view for bisync management

### Documentation
- Added comprehensive BISYNC_GUIDE.md with examples and best practices
- Updated README with bisync usage instructions
- Added Mac Silicon specific documentation

## [1.0.0] - 2024-02-16

### Added
- Initial release of Rclone GUI
- Cross-platform desktop application using Avalonia UI
- Support for multiple cloud storage providers:
  - OneDrive (OAuth2 with browser authentication)
  - Google Drive (OAuth2 with browser authentication)
  - Dropbox (OAuth2)
  - Box (OAuth2)
  - Amazon S3 (API key authentication)
  - FTP (username/password)
  - SFTP (username/password)
  - WebDAV (username/password)
  - Mega (username/password)
  - pCloud (username/password)
- User interface features:
  - Add new cloud storage accounts
  - View all configured remotes
  - Test remote connections
  - Delete remotes
  - Real-time status updates
  - Loading indicators
- Authentication methods:
  - Username/password authentication
  - OAuth2 browser-based authentication
  - API key authentication
- MVVM architecture with clean separation of concerns
- Responsive and intuitive UI design
- Password obscuring using Rclone's built-in security
- Automatic Rclone version detection
- Dynamic form fields based on provider type
- Comprehensive error handling and user feedback

### Security
- Passwords are never stored in plain text
- OAuth2 tokens managed securely by Rclone
- No command injection vulnerabilities
- No sensitive data logging
- Proper input validation and sanitization

### Documentation
- Comprehensive README with features and usage instructions
- Quick start guide for new users
- Architecture documentation for developers
- Security considerations document
- Contributing guidelines
- Launch scripts for easy startup (run.sh, run.bat)

### Technical Details
- Built with .NET 8.0
- Avalonia UI 11.3+ for cross-platform support
- CommunityToolkit.Mvvm for MVVM pattern
- Supports Windows, macOS, and Linux
- Clean project structure with separate Core library
- Service-based architecture for maintainability

## [Unreleased]

### Planned Features
- File browser for remotes
- Copy/Sync operations through UI
- Scheduled tasks
- Bandwidth monitoring and limiting
- Configuration import/export
- Multi-language support
- Dark theme
- Provider icons and logos
- Favorites/pinned remotes
- Search and filter remotes
- Bulk operations
- Transfer queue and history
- Notifications for long-running operations

### Planned Improvements
- Unit tests
- Integration tests
- Dependency injection container
- Logging framework
- Error reporting system
- Auto-update functionality
- Application settings persistence
- Portable mode
- Command-line arguments
- System tray integration

### Known Issues
- OAuth2 authentication requires manual browser interaction
- No visual progress for OAuth2 flow
- Limited error details for some Rclone errors
- No confirmation dialogs for delete operations

---

## Version History Format

### [Version] - YYYY-MM-DD

#### Added
- New features

#### Changed
- Changes to existing functionality

#### Deprecated
- Soon-to-be removed features

#### Removed
- Now removed features

#### Fixed
- Bug fixes

#### Security
- Security fixes and improvements

---

[1.0.0]: https://github.com/xtremevice/Rclone_Gui_IA/releases/tag/v1.0.0
