#!/bin/bash

# Quick Setup Script for Mac Silicon
# Este script configura todo automáticamente para Mac con Apple Silicon

echo "============================================="
echo "  Rclone GUI - Instalación Rápida"
echo "  Para Mac Silicon (M1/M2/M3)"
echo "============================================="
echo ""

# Check if running on Mac
if [[ "$OSTYPE" != "darwin"* ]]; then
    echo "❌ Error: Este script es solo para macOS"
    exit 1
fi

# Check if running on Apple Silicon
ARCH=$(uname -m)
if [[ "$ARCH" != "arm64" ]]; then
    echo "⚠️  Advertencia: Este sistema no parece ser Apple Silicon (M1/M2/M3)"
    echo "   Arquitectura detectada: $ARCH"
    echo "   ¿Continuar de todas formas? (s/n)"
    read -r response
    if [[ ! "$response" =~ ^[sS]$ ]]; then
        exit 0
    fi
fi

echo "✓ Sistema: macOS Apple Silicon detectado"
echo ""

# Check for Homebrew
if ! command -v brew &> /dev/null; then
    echo "📦 Instalando Homebrew..."
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
    
    # Add Homebrew to PATH for Apple Silicon
    echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> ~/.zprofile
    eval "$(/opt/homebrew/bin/brew shellenv)"
else
    echo "✓ Homebrew ya está instalado"
fi

echo ""

# Install .NET 8
if ! command -v dotnet &> /dev/null; then
    echo "📦 Instalando .NET 8.0 SDK..."
    brew install dotnet@8
else
    echo "✓ .NET SDK ya está instalado"
    dotnet --version
fi

echo ""

# Install Rclone
if ! command -v rclone &> /dev/null; then
    echo "📦 Instalando Rclone..."
    brew install rclone
else
    echo "✓ Rclone ya está instalado"
    rclone version | head -n 1
fi

echo ""
echo "============================================="
echo "  Instalación completada exitosamente! ✓"
echo "============================================="
echo ""
echo "Ahora puedes ejecutar la aplicación:"
echo ""
echo "  1. cd $(pwd)"
echo "  2. ./run.sh"
echo ""
echo "O manualmente:"
echo "  dotnet run --project src/RcloneGui/RcloneGui.csproj"
echo ""
echo "Para más información, consulta MAC_SILICON_SETUP.md"
echo ""
