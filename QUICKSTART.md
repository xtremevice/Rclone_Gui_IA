# Quick Start Guide - Rclone GUI

## Installation Steps

### 1. Install Prerequisites

#### Install .NET 8.0 SDK

**Windows:**
- Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- Run the installer

**macOS:**
```bash
brew install dotnet@8
```

**Linux (Ubuntu/Debian):**
```bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

#### Install Rclone

**Windows:**
- Download from: https://rclone.org/downloads/
- Extract and add to PATH

**macOS:**
```bash
brew install rclone
```

**Linux:**
```bash
sudo -v ; curl https://rclone.org/install.sh | sudo bash
```

### 2. Build and Run the Application

```bash
# Clone the repository
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Build the application
dotnet build

# Run the application
dotnet run --project src/RcloneGui/RcloneGui.csproj
```

## First Time Setup

1. **Launch the Application**: The main window will show your current remotes (empty if first time)

2. **Check Rclone Status**: The top of the window shows the Rclone version. If it says "Rclone not found", make sure Rclone is installed and in your PATH.

## Adding Your First Cloud Account

### Example: Adding an FTP Server

1. Click **"Add Account"** in the navigation menu
2. Select **"FTP"** from the provider dropdown
3. Enter a name, e.g., "MyFTP"
4. Fill in:
   - Host: `ftp.example.com`
   - Username: your FTP username
   - Password: your FTP password
5. Click **"Add Account"**
6. Check the status message for success

### Example: Adding OneDrive (OAuth2)

1. Click **"Add Account"** in the navigation menu
2. Select **"OneDrive"** from the provider dropdown
3. Enter a name, e.g., "MyOneDrive"
4. Note the browser authentication warning
5. Click **"Add Account"**
6. A browser window will open for Microsoft authentication
7. Log in with your Microsoft account
8. Grant permissions
9. Return to the application to see the success message

### Example: Adding Amazon S3

1. Click **"Add Account"** in the navigation menu
2. Select **"Amazon S3"** from the provider dropdown
3. Enter a name, e.g., "MyS3"
4. Fill in:
   - Access Key ID: your AWS access key
   - Secret Access Key: your AWS secret key
5. Click **"Add Account"**

## Managing Your Remotes

1. Click **"My Remotes"** in the navigation menu
2. You'll see all your configured remotes
3. Click on a remote to select it
4. Use the action buttons:
   - **Refresh**: Reload the list
   - **Test Selected**: Verify the remote is working
   - **Delete Selected**: Remove the remote

## Common Provider Configurations

### Google Drive
- Type: OAuth2
- Browser: Required
- No credentials needed (uses Google OAuth)

### Dropbox
- Type: OAuth2
- Browser: Required
- No credentials needed (uses Dropbox OAuth)

### SFTP
- Type: Username/Password
- Required fields: Host, Username, Password
- Example: `ssh.example.com`

### WebDAV
- Type: Username/Password
- Required fields: URL, Username, Password
- Example: `https://webdav.example.com`

### Mega
- Type: Username/Password
- Required fields: Username (email), Password
- Example: `your@email.com`

## Troubleshooting

### "Rclone not found"
- Verify Rclone is installed: `rclone version`
- Add Rclone to your PATH
- Restart the application

### OAuth2 fails to open browser
- Ensure you have a web browser installed
- Check your firewall settings
- Try running as administrator (Windows) or with sudo (Linux/macOS)

### Remote test fails
- Verify your credentials are correct
- Check your internet connection
- Some providers may have rate limits

## Advanced Usage

### Using with Rclone CLI

All remotes configured in the GUI are available in Rclone CLI:

```bash
# List all remotes
rclone listremotes

# List files in a remote
rclone ls RemoteName:

# Copy files
rclone copy /local/path RemoteName:/remote/path

# Sync directories
rclone sync /local/path RemoteName:/remote/path
```

## Tips

1. **Naming Remotes**: Use descriptive names like "PersonalOneDrive" or "WorkGoogleDrive"
2. **Testing**: Always test a new remote after adding it
3. **Security**: The application uses Rclone's secure password obscuring
4. **Multiple Accounts**: You can add multiple accounts of the same provider type

## Getting Help

- Rclone Documentation: https://rclone.org/docs/
- GitHub Issues: https://github.com/xtremevice/Rclone_Gui_IA/issues
