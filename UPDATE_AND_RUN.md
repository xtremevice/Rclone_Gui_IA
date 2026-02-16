# 🚀 Update and Run Rclone GUI

**Quick commands to update to the latest version and run the application**

---

## 📦 Update and Run (All Systems)

### Option 1: Single Command (Recommended)

```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

**For Windows:**
```cmd
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

### Option 2: Step by Step

```bash
# 1. Update code from GitHub
git pull origin main

# 2. Build the application
dotnet build RcloneGui.slnx --configuration Release

# 3. Run the application
./run.sh          # Linux / macOS
run.bat           # Windows
```

---

## 🔄 Update Only (Without Running)

```bash
# Download the latest version
git pull origin main

# Build
dotnet build RcloneGui.slnx --configuration Release
```

---

## ▶️ Run Only (Without Updating)

```bash
./run.sh          # Linux / macOS
run.bat           # Windows
```

Or manually:
```bash
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

---

## 🆕 First Time Installation

If it's your first time, you need to clone the repository:

```bash
# Clone the repository
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Install and run
./run.sh          # Linux / macOS
run.bat           # Windows
```

**Prerequisites:**
- .NET 8.0 SDK or later → https://dotnet.microsoft.com/download
- Rclone → https://rclone.org/downloads/

---

## 🍎 macOS Apple Silicon (M1/M2/M3)

### Complete First Time Installation

```bash
# Clone the repository
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Automatic installation of everything (Homebrew, .NET, Rclone)
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh

# Run
./run.sh
```

### Update and Run

```bash
cd Rclone_Gui_IA
git pull origin main
dotnet build RcloneGui.slnx --configuration Release
./run.sh
```

Or in a single command:
```bash
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

---

## 🔍 Check Current Version

```bash
# View installed version
git log -1 --oneline

# View all available updates
git fetch origin
git log HEAD..origin/main --oneline
```

---

## ⚙️ Useful Commands

### Clean and Rebuild

If you have build issues:

```bash
# Clean previous build artifacts
dotnet clean

# Rebuild from scratch
dotnet build RcloneGui.slnx --configuration Release
```

### Force Update

If there are conflicts or local changes:

```bash
# Save local changes (optional)
git stash

# Force update
git fetch origin
git reset --hard origin/main

# Restore saved changes (if you used stash)
git stash pop
```

---

## 📋 Command Summary by Platform

### Windows

```cmd
REM Update
git pull origin main

REM Build
dotnet build RcloneGui.slnx --configuration Release

REM Run
run.bat
```

**All in one:**
```cmd
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

### Linux

```bash
# Update
git pull origin main

# Build
dotnet build RcloneGui.slnx --configuration Release

# Run
./run.sh
```

**All in one:**
```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

### macOS (Intel and Apple Silicon)

```bash
# Update
git pull origin main

# Build
dotnet build RcloneGui.slnx --configuration Release

# Run
./run.sh
```

**All in one:**
```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

---

## 🆘 Troubleshooting

### Error: "git: command not found"

Install Git:
- **Windows**: https://git-scm.com/download/win
- **macOS**: `brew install git` or already included
- **Linux**: `sudo apt install git` (Ubuntu/Debian)

### Error: "dotnet: command not found"

Install .NET SDK:
- Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- See complete guide: [HOW_TO_RUN.md](HOW_TO_RUN.md)

### Error: "Already up to date" but don't see the update

```bash
# Force update
git fetch origin
git reset --hard origin/main
dotnet build RcloneGui.slnx --configuration Release
```

### Error running: Display/X11 error

This is normal in headless environments (servers). The application needs a desktop environment to run.

### Error: MSB4068 in RcloneGui.slnx (macOS)

If after updating you see this error:
```
RcloneGui.slnx(1,1): error MSB4068: The element
```

**Automatic fix (Recommended):**

Run the included repair script:
```bash
# Linux / macOS
bash fix-msb4068.sh

# Windows
fix-msb4068.bat
```

The script:
- ✅ Automatically detects if XML declaration is missing
- ✅ Creates a backup before making changes
- ✅ Adds the XML declaration if needed
- ✅ Verifies the fix worked

**Manual solution:**

1. Verify you have the latest version with the fix:
```bash
git pull origin main
```

2. If the error persists, check the content of RcloneGui.slnx:
```bash
head -1 RcloneGui.slnx
```

Should show: `<?xml version="1.0" encoding="utf-8"?>`

3. If it does NOT have the XML declaration, update manually:
```bash
# Option A: Download the corrected version
curl -o RcloneGui.slnx https://raw.githubusercontent.com/xtremevice/Rclone_Gui_IA/main/RcloneGui.slnx

# Option B: Manually add the XML declaration
sed -i '1s/^/<?xml version="1.0" encoding="utf-8"?>\n/' RcloneGui.slnx
```

4. Rebuild:
```bash
dotnet build RcloneGui.slnx --configuration Release
```

**Note**: This error occurs in old versions of the repository. The permanent solution is to ensure you have the latest version.

---

## 📚 Additional Documentation

- **Complete execution guide (Spanish)**: [COMO_EJECUTAR.md](COMO_EJECUTAR.md)
- **Complete execution guide (English)**: [HOW_TO_RUN.md](HOW_TO_RUN.md)
- **Spanish version**: [ACTUALIZAR_Y_EJECUTAR.md](ACTUALIZAR_Y_EJECUTAR.md)
- **Mac Silicon setup**: [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md)
- **Quick start**: [QUICKSTART.md](QUICKSTART.md)
- **Wiki**: [WIKI_HOME.md](WIKI_HOME.md)

---

## 🎯 Favorite Command (Copy and Paste)

**Most used command - Update and run in one step:**

```bash
# Linux / macOS
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

```cmd
# Windows
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

---

**Last updated:** 2026-02-16
