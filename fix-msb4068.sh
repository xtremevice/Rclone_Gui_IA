#!/bin/bash

# Fix script for MSB4068 error in RcloneGui.slnx
# This script adds the XML declaration to RcloneGui.slnx if it's missing

echo "=================================================="
echo "  Fix MSB4068 Error - RcloneGui.slnx"
echo "=================================================="
echo ""

SLNX_FILE="RcloneGui.slnx"

# Check if file exists
if [ ! -f "$SLNX_FILE" ]; then
    echo "❌ Error: $SLNX_FILE not found"
    echo "   Make sure you're in the project root directory"
    exit 1
fi

echo "Checking $SLNX_FILE..."

# Check if XML declaration is already present
FIRST_LINE=$(head -n 1 "$SLNX_FILE")

if [[ "$FIRST_LINE" == *"<?xml version"* ]]; then
    echo "✓ XML declaration already exists!"
    echo "  First line: $FIRST_LINE"
    echo ""
    echo "The file is already fixed. If you still get the error:"
    echo "1. Make sure you saved the file"
    echo "2. Try: dotnet clean && dotnet build RcloneGui.slnx --configuration Release"
    echo "3. Check the troubleshooting section in ACTUALIZAR_Y_EJECUTAR.md"
    exit 0
fi

echo "⚠️  XML declaration is missing"
echo "   Current first line: $FIRST_LINE"
echo ""

# Create backup
BACKUP_FILE="${SLNX_FILE}.backup"
cp "$SLNX_FILE" "$BACKUP_FILE"
echo "✓ Backup created: $BACKUP_FILE"

# Add XML declaration
echo '<?xml version="1.0" encoding="utf-8"?>' > "${SLNX_FILE}.tmp"
cat "$SLNX_FILE" >> "${SLNX_FILE}.tmp"
mv "${SLNX_FILE}.tmp" "$SLNX_FILE"

echo "✓ XML declaration added"
echo ""

# Verify the fix
FIRST_LINE=$(head -n 1 "$SLNX_FILE")
if [[ "$FIRST_LINE" == *"<?xml version"* ]]; then
    echo "✅ Fix applied successfully!"
    echo "   First line now: $FIRST_LINE"
    echo ""
    echo "Next steps:"
    echo "1. Test the build: dotnet build RcloneGui.slnx --configuration Release"
    echo "2. Run the application: ./run.sh (or run.bat on Windows)"
    echo ""
    echo "If you want to revert: mv $BACKUP_FILE $SLNX_FILE"
else
    echo "❌ Fix failed. Restoring backup..."
    mv "$BACKUP_FILE" "$SLNX_FILE"
    echo "   Original file restored"
    exit 1
fi

echo "=================================================="
