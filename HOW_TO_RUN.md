# How to Run Rclone GUI - Complete Platform Guide

This guide details how to run Rclone GUI on Windows, Linux, and macOS, including all available methods.

## 📋 Table of Contents

- [Windows](#windows)
- [Linux](#linux)
- [macOS](#macos)
  - [macOS Intel](#macos-intel)
  - [macOS Apple Silicon (M1/M2/M3)](#macos-apple-silicon-m1m2m3)
- [Installation Verification](#installation-verification)
- [Common Troubleshooting](#common-troubleshooting)

---

## Windows

### Prerequisites

1. **.NET 8.0 SDK or later**
   - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   - During installation, make sure to check "Add to PATH"

2. **Rclone**
   - Download from: https://rclone.org/downloads/
   - Extract the ZIP file
   - Add to system PATH or copy `rclone.exe` to a known folder

### Method 1: Automatic Script (Recommended)

The easiest method is to use the included script:

```cmd
run.bat
```

This script:
- ✅ Verifies .NET is installed
- ✅ Verifies Rclone is installed (warns if missing)
- ✅ Builds the application automatically
- ✅ Runs the application

### Method 2: Manual Command Line

Open PowerShell or CMD in the project folder:

```cmd
# Build the project
dotnet build --configuration Release

# Run the application
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

### Method 3: Visual Studio

If you have Visual Studio 2022:

1. Open the `RcloneGui.slnx` file
2. Select `RcloneGui` as the startup project
3. Press `F5` or click "Start"

### Adding Rclone to PATH on Windows

If you get "Rclone not found" error:

1. Download Rclone from https://rclone.org/downloads/
2. Extract `rclone.exe` to `C:\Program Files\Rclone\`
3. Add to PATH:
   - Press `Win + X` and select "System"
   - Click "Advanced system settings"
   - Click "Environment Variables"
   - Under "System variables", select "Path" and click "Edit"
   - Click "New" and add: `C:\Program Files\Rclone`
   - Click "OK" on all windows
4. Open a new CMD window and verify:
   ```cmd
   rclone version
   ```

---

## Linux

### Prerequisites

1. **.NET 8.0 SDK or later**

**Ubuntu/Debian:**
```bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

**Fedora:**
```bash
sudo dnf install dotnet-sdk-8.0
```

**Arch Linux:**
```bash
sudo pacman -S dotnet-sdk
```

2. **Rclone**

```bash
curl https://rclone.org/install.sh | sudo bash
```

Or with package manager:
```bash
# Ubuntu/Debian
sudo apt install rclone

# Fedora
sudo dnf install rclone

# Arch Linux
sudo pacman -S rclone
```

### Method 1: Automatic Script (Recommended)

The easiest method is to use the included script:

```bash
# Give execution permissions (first time only)
chmod +x run.sh

# Run the script
./run.sh
```

This script:
- ✅ Verifies .NET is installed
- ✅ Verifies Rclone is installed (warns if missing)
- ✅ Builds the application automatically
- ✅ Runs the application

### Method 2: Manual Command Line

```bash
# Build the project
dotnet build --configuration Release

# Run the application
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

### Method 3: Using JetBrains Rider

If you have Rider installed:

1. Open the project folder in Rider
2. Select the `RcloneGui` run configuration
3. Press `Shift+F10` or click the "Run" button

### Linux Notes

- On some distributions, you may need to install additional dependencies for Avalonia:
  ```bash
  sudo apt install libx11-dev libice-dev libsm-dev
  ```

- If using Wayland, you may need to run with XWayland:
  ```bash
  GDK_BACKEND=x11 ./run.sh
  ```

---

## macOS

### macOS Intel

#### Prerequisites

1. **Homebrew** (if you don't have it):
   ```bash
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   ```

2. **.NET 8.0 SDK**:
   ```bash
   brew install dotnet@8
   ```

3. **Rclone**:
   ```bash
   brew install rclone
   ```

#### Method 1: Automatic Script (Recommended)

```bash
# Give execution permissions (first time only)
chmod +x run.sh

# Run the script
./run.sh
```

#### Method 2: Manual Command Line

```bash
# Build the project
dotnet build --configuration Release

# Run the application
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

---

### macOS Apple Silicon (M1/M2/M3)

macOS with Apple Silicon processors (M1, M2, M3) requires some additional steps. The application is fully optimized for these processors.

#### Prerequisites

1. **Homebrew** (if you don't have it):
   ```bash
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   ```

   After installation, make sure to add Homebrew to PATH:
   ```bash
   echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> ~/.zprofile
   eval "$(/opt/homebrew/bin/brew shellenv)"
   ```

2. **Complete Installation with Automatic Script**:
   
   The project includes a script that installs everything automatically:
   
   ```bash
   # Give execution permissions (first time only)
   chmod +x setup-mac-silicon.sh
   
   # Run the installation script
   ./setup-mac-silicon.sh
   ```
   
   This script automatically installs:
   - ✅ Homebrew (if not installed)
   - ✅ .NET 8.0 SDK
   - ✅ Rclone

3. **Manual Installation** (if the script fails):
   
   ```bash
   # Install .NET 8.0 SDK
   brew install dotnet@8
   
   # Install Rclone
   brew install rclone
   ```

#### Method 1: Automatic Script (Recommended)

After installing prerequisites:

```bash
# Give execution permissions (first time only)
chmod +x run.sh

# Run the script
./run.sh
```

#### Method 2: Manual Command Line

```bash
# Build the project
dotnet build --configuration Release

# Run the application
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

#### Apple Silicon Specific Features

The application includes optimizations for Apple Silicon:

- **Optimized Rclone Path**: Automatically detects `/opt/homebrew/bin/rclone` (Apple Silicon location)
- **Bisync Commands**: Generates optimized commands when you enable "Generate for Mac Silicon"
- **ARM64 Performance**: Native execution on ARM64 architecture

#### Apple Silicon Verification

To confirm you're running on Apple Silicon:

```bash
# View system architecture
uname -m
# Should show: arm64

# View .NET version
dotnet --version

# View Rclone location
which rclone
# On Apple Silicon should show: /opt/homebrew/bin/rclone
# On Intel should show: /usr/local/bin/rclone
```

---

## Installation Verification

Before running the application, verify everything is installed correctly:

### Verify .NET

```bash
# Windows (CMD/PowerShell), Linux, macOS
dotnet --version
```

Should show version 8.0.x or higher.

### Verify Rclone

```bash
# Windows (CMD/PowerShell), Linux, macOS
rclone version
```

Should show the installed Rclone version.

### Verify Build

```bash
# All systems
dotnet build
```

If the build is successful, you'll see: `Build succeeded.`

---

## Common Troubleshooting

### Error: "dotnet: command not found"

**Cause**: .NET SDK is not installed or not in PATH.

**Solution**:
- **Windows**: Reinstall .NET SDK and make sure to check "Add to PATH"
- **Linux**: Install .NET SDK using package manager
- **macOS**: Install with `brew install dotnet@8`

### Error: "rclone: command not found"

**Cause**: Rclone is not installed or not in PATH.

**Solution**:
- **Windows**: Follow the steps in [Adding Rclone to PATH on Windows](#adding-rclone-to-path-on-windows)
- **Linux**: Install with `curl https://rclone.org/install.sh | sudo bash`
- **macOS**: Install with `brew install rclone`

### Error: "Could not load file or assembly"

**Cause**: Corrupt or incomplete build files.

**Solution**:
```bash
# Clean and rebuild
dotnet clean
dotnet build --configuration Release
```

### Application doesn't show window (Linux)

**Cause**: Missing Avalonia UI dependencies.

**Solution**:
```bash
sudo apt install libx11-dev libice-dev libsm-dev
```

### OAuth2 doesn't open browser

**Cause**: Issue with automatic browser opening.

**Solution**:
1. The application will show a URL in the status message
2. Copy the URL manually
3. Open it in your browser
4. Complete the authentication
5. The application will detect the token automatically

### Error: "Access denied" when running scripts

**Windows**:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**Linux/macOS**:
```bash
chmod +x run.sh
chmod +x setup-mac-silicon.sh
```

### Build is very slow

**Solution**: Use incremental build and consider building in Debug mode for development:
```bash
dotnet build  # Debug is faster for development
```

Use Release only for production:
```bash
dotnet build --configuration Release
```

### Error: "framework version 8.0.x not found"

**Cause**: Runtime was installed but not the SDK.

**Solution**: Install the complete SDK, not just the runtime:
- Windows/macOS: Download the SDK from https://dotnet.microsoft.com/download
- Linux: Use package manager to install `dotnet-sdk-8.0`

---

## Running from Any Location

### Windows

Create a batch file in a PATH folder:

```batch
@echo off
cd C:\path\to\Rclone_Gui_IA
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

### Linux/macOS

Create an alias in your `.bashrc` or `.zshrc`:

```bash
alias rclonegui='cd /path/to/Rclone_Gui_IA && ./run.sh'
```

Then reload the shell:
```bash
source ~/.bashrc  # or source ~/.zshrc
```

Now you can run from anywhere:
```bash
rclonegui
```

---

## Quick Command Summary

### Windows
```cmd
# Installation
# 1. Install .NET 8.0 SDK from https://dotnet.microsoft.com/download
# 2. Install Rclone from https://rclone.org/downloads/

# Run
run.bat
```

### Linux (Ubuntu/Debian)
```bash
# Installation
sudo apt-get install -y dotnet-sdk-8.0
curl https://rclone.org/install.sh | sudo bash

# Run
chmod +x run.sh
./run.sh
```

### macOS Intel
```bash
# Installation
brew install dotnet@8 rclone

# Run
chmod +x run.sh
./run.sh
```

### macOS Apple Silicon (M1/M2/M3)
```bash
# Complete installation
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh

# Run
./run.sh
```

---

## Additional Documentation

- **README.md**: General project documentation
- **QUICKSTART.md**: Quick start guide
- **MAC_SILICON_SETUP.md**: Detailed guide for Mac Silicon
- **BISYNC_GUIDE.md**: Bidirectional sync guide
- **COMO_EJECUTAR.md**: This guide in Spanish

---

## Support and Contributions

- **Issues**: https://github.com/xtremevice/Rclone_Gui_IA/issues
- **Pull Requests**: https://github.com/xtremevice/Rclone_Gui_IA/pulls
- **Rclone Documentation**: https://rclone.org/docs/

---

**Last updated**: 2026-02-16
