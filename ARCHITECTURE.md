# Rclone GUI - Architecture and Design Document

## Overview

Rclone GUI is a cross-platform desktop application built with .NET 8 and Avalonia UI that provides a graphical interface for managing Rclone cloud storage configurations.

## Technology Stack

- **Framework**: .NET 8.0
- **UI Framework**: Avalonia UI 11.3+
- **Architecture Pattern**: MVVM (Model-View-ViewModel)
- **MVVM Toolkit**: CommunityToolkit.Mvvm
- **Target Platforms**: Windows, macOS, Linux

## Project Structure

### RcloneGui.Core (Class Library)

Core business logic and services for interacting with Rclone.

#### Models

**ProviderInfo.cs**
- Defines cloud storage provider information
- Properties: Name, Type, Description, AuthType, RequiresBrowser, RequiredFields
- Enumeration: AuthenticationType (None, UsernamePassword, OAuth2, ApiKey, Token)

**RemoteAccount.cs**
- Represents a configured Rclone remote
- Properties: Name, Type, Configuration, CreatedAt, LastModified, IsActive
- Stores remote configuration as a dictionary

**AuthenticationRequest.cs**
- Request model for adding new accounts
- Properties: ProviderType, RemoteName, Username, Password, AdditionalParameters

**AuthenticationResult.cs**
- Response model for authentication operations
- Properties: Success, ErrorMessage, Tokens, ConfigurationData

#### Services

**IProviderService / ProviderService**
- Provides information about supported cloud storage providers
- Pre-configured list of 10+ providers
- Methods:
  - `GetSupportedProviders()`: Returns all supported providers
  - `GetProviderInfo(string type)`: Get info for specific provider type

**IRcloneService / RcloneService**
- Core service for Rclone CLI interaction
- Handles all Rclone command execution
- Methods:
  - `GetConfiguredRemotesAsync()`: Lists all configured remotes
  - `AuthenticateAsync()`: Adds remote with username/password auth
  - `AuthenticateWithBrowserAsync()`: Adds remote with OAuth2 browser auth
  - `DeleteRemoteAsync()`: Removes a remote
  - `TestRemoteAsync()`: Tests remote connectivity
  - `GetRcloneVersionAsync()`: Gets Rclone version info

### RcloneGui (Avalonia Application)

User interface layer built with Avalonia UI.

#### ViewModels

**ViewModelBase.cs**
- Base class for all ViewModels
- Inherits from ObservableObject (CommunityToolkit.Mvvm)

**MainWindowViewModel.cs**
- Root ViewModel for the application
- Manages navigation between pages
- Properties:
  - `CurrentPage`: Currently displayed page
  - `RcloneVersion`: Rclone version string
  - `AddAccountViewModel`: Instance of AddAccountViewModel
  - `RemotesViewModel`: Instance of RemotesViewModel
- Commands:
  - `ShowRemotesCommand`: Navigate to remotes view
  - `ShowAddAccountCommand`: Navigate to add account view

**AddAccountViewModel.cs**
- Handles adding new cloud storage accounts
- Properties:
  - `Providers`: List of available providers
  - `SelectedProvider`: Currently selected provider
  - `RemoteName`, `Username`, `Password`: Input fields
  - `Host`, `Url`, `AccessKeyId`, `SecretAccessKey`: Provider-specific fields
  - `StatusMessage`: Feedback message
  - Field visibility flags (ShowUsernamePassword, ShowHost, etc.)
- Commands:
  - `AddAccountCommand`: Adds the account
  - `ClearFieldsCommand`: Clears all input fields
- Logic:
  - Dynamically shows/hides fields based on provider type
  - Handles both username/password and OAuth2 authentication
  - Provides real-time feedback

**RemotesViewModel.cs**
- Manages list of configured remotes
- Properties:
  - `Remotes`: Collection of RemoteAccount objects
  - `SelectedRemote`: Currently selected remote
  - `StatusMessage`: Feedback message
  - `IsLoading`: Loading indicator
- Commands:
  - `LoadRemotesCommand`: Refreshes the remote list
  - `DeleteRemoteCommand`: Deletes selected remote
  - `TestRemoteCommand`: Tests selected remote

#### Views

**MainWindow.axaml**
- Main application window
- Layout:
  - Header: Application title and Rclone version
  - Navigation panel: Side menu with "My Remotes" and "Add Account"
  - Content area: Dynamic content based on CurrentPage
  - Footer: Application info
- Features:
  - Responsive layout
  - Color-coded UI elements
  - Professional styling

**AddAccountView.axaml**
- Form for adding new accounts
- Components:
  - Provider dropdown with descriptions
  - Remote name input
  - Dynamic fields based on provider type
  - OAuth2 warning for browser-based auth
  - Action buttons (Add, Clear)
  - Status display
- Features:
  - Conditional field visibility
  - Input validation hints
  - Real-time feedback

**RemotesView.axaml**
- List and management of configured remotes
- Components:
  - Scrollable list of remotes with visual cards
  - Action panel with Refresh, Test, Delete buttons
  - Selected remote info panel
  - Status display
- Features:
  - Card-based remote display
  - Visual active status indicator
  - One-click actions

## Design Patterns

### MVVM (Model-View-ViewModel)

- **Models**: Pure data classes in RcloneGui.Core/Models
- **ViewModels**: Business logic and UI state in RcloneGui/ViewModels
- **Views**: XAML UI definitions in RcloneGui/Views
- **Benefits**: Clean separation, testability, maintainability

### Service Layer

- Interface-based services (IProviderService, IRcloneService)
- Dependency injection ready
- Easy to mock for testing

### Command Pattern

- RelayCommand from CommunityToolkit.Mvvm
- Async command support
- Automatic CanExecute handling

### Observer Pattern

- ObservableObject and ObservableProperty
- Automatic property change notifications
- Data binding support

## Authentication Flow

### Username/Password Authentication

1. User selects provider (e.g., FTP, SFTP)
2. Enters credentials in UI
3. ViewModel validates input
4. RcloneService executes `rclone config create` with credentials
5. Password is obscured using `rclone obscure`
6. Configuration is saved by Rclone
7. Success/error message displayed

### OAuth2 Browser Authentication

1. User selects OAuth2 provider (e.g., OneDrive)
2. Warning displayed about browser requirement
3. User clicks Add Account
4. RcloneService executes `rclone config create` with `config_is_local false`
5. Rclone opens system browser
6. User authenticates in browser
7. Token is captured by Rclone
8. Configuration is saved by Rclone
9. Success/error message displayed

## Data Flow

```
User Input
    ↓
View (XAML)
    ↓
ViewModel (Commands)
    ↓
Service (Business Logic)
    ↓
Rclone CLI
    ↓
File System (Rclone Config)
    ↓
Service (Parse Results)
    ↓
ViewModel (Update State)
    ↓
View (Display)
```

## Rclone Integration

### Command Execution

- Uses System.Diagnostics.Process
- Captures stdout and stderr
- Handles both sync and async execution
- Supports interactive commands

### Configuration Storage

- Rclone stores config in standard locations:
  - Windows: `%APPDATA%\rclone\rclone.conf`
  - Linux/macOS: `~/.config/rclone/rclone.conf`
- Application reads from Rclone's config
- No duplicate storage

### Command Examples

```bash
# List remotes
rclone listremotes

# Get remote config
rclone config dump RemoteName

# Create remote (username/password)
rclone config create MyRemote ftp user username pass obscured_password

# Create remote (OAuth2)
rclone config create MyRemote onedrive config_is_local false

# Delete remote
rclone config delete RemoteName

# Test remote
rclone lsd RemoteName:
```

## UI/UX Design Principles

### Color Scheme

- Primary: #2196F3 (Blue)
- Success: #4CAF50 (Green)
- Error: #F44336 (Red)
- Warning: #FF9800 (Orange)
- Neutral: #F5F5F5 (Light Gray)

### Typography

- Headers: 24px, Bold
- Subheaders: 16px, SemiBold
- Body: 14px, Regular
- Small text: 11-12px, Regular

### Layout

- Responsive design
- Consistent spacing (10px, 15px, 20px)
- Card-based components
- Clear visual hierarchy

### Feedback

- Loading indicators for async operations
- Status messages for all actions
- Visual confirmation of success/failure
- Helpful error messages

## Error Handling

### Rclone Not Found

- Check PATH at startup
- Display version in header
- Show helpful error if not found

### Authentication Failures

- Capture Rclone error output
- Display user-friendly messages
- Provide troubleshooting hints

### Network Errors

- Timeout handling
- Retry suggestions
- Connection test option

## Security Considerations

### Password Handling

- Passwords never stored in plain text
- Uses Rclone's `obscure` command
- Masked input fields (PasswordChar)

### OAuth2 Tokens

- Handled entirely by Rclone
- Stored in Rclone's config
- Not exposed to application

### Process Execution

- No shell execution
- Direct process spawning
- Argument validation

## Performance

### Async Operations

- All Rclone commands run asynchronously
- UI remains responsive
- Progress indication

### Resource Usage

- Minimal memory footprint
- No unnecessary background tasks
- Process cleanup

## Extensibility

### Adding New Providers

1. Add ProviderInfo to ProviderService
2. Specify authentication type
3. Define required fields
4. No code changes needed in UI

### Custom Authentication

- Interface-based services
- Easy to extend RcloneService
- Support for new auth types

## Testing Considerations

### Unit Testing

- ViewModels are testable
- Services use interfaces
- Mock Rclone commands

### Integration Testing

- Test with real Rclone
- Verify config file changes
- Test all provider types

## Future Enhancements

### Potential Features

- File browser for remotes
- Copy/sync operations UI
- Scheduled tasks
- Bandwidth monitoring
- Config import/export
- Multi-language support
- Dark theme

### Technical Improvements

- Dependency injection container
- Logging framework
- Error reporting
- Auto-updates
- Settings persistence

## Deployment

### Build Outputs

- Self-contained executables
- Framework-dependent executables
- Platform-specific packages

### Distribution

- GitHub Releases
- Installer packages (MSI, DEB, PKG)
- Portable versions

## Dependencies

### NuGet Packages

- Avalonia (11.3.12+)
- Avalonia.Desktop
- Avalonia.Themes.Fluent
- CommunityToolkit.Mvvm
- System.Text.Json

### External Dependencies

- .NET 8.0 Runtime
- Rclone CLI

## Conclusion

Rclone GUI provides a clean, cross-platform interface for managing Rclone configurations with support for multiple authentication methods and cloud storage providers. The MVVM architecture ensures maintainability and testability, while Avalonia UI delivers a native experience on all platforms.
