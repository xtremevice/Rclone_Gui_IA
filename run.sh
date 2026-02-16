#!/bin/bash

# Rclone GUI Launcher Script for Linux/macOS

echo "================================"
echo "  Rclone GUI Launcher"
echo "================================"
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null
then
    echo "ERROR: .NET SDK not found!"
    echo "Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download"
    exit 1
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
echo "✓ .NET SDK version: $DOTNET_VERSION"

# Check if Rclone is installed
if ! command -v rclone &> /dev/null
then
    echo "WARNING: Rclone not found in PATH!"
    echo "The application will run but won't be able to manage remotes."
    echo "Install Rclone from: https://rclone.org/downloads/"
else
    RCLONE_VERSION=$(rclone version | head -n 1)
    echo "✓ $RCLONE_VERSION"
fi

echo ""
echo "Building application..."
dotnet build --configuration Release

if [ $? -eq 0 ]; then
    echo ""
    echo "✓ Build successful!"
    echo ""
    echo "Starting Rclone GUI..."
    echo "================================"
    echo ""
    dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
else
    echo ""
    echo "ERROR: Build failed!"
    echo "Please check the error messages above."
    exit 1
fi
