# Fix MSB4068 Error Scripts

These scripts automatically fix the MSB4068 XML parsing error in RcloneGui.slnx.

## The Error

```
/path/to/RcloneGui.slnx(1,1): error MSB4068: The element
```

This error occurs when the `RcloneGui.slnx` file is missing the XML declaration header.

## Quick Fix

### Linux / macOS
```bash
bash fix-msb4068.sh
```

### Windows
```cmd
fix-msb4068.bat
```

## What the Scripts Do

1. ✅ Check if `RcloneGui.slnx` exists
2. ✅ Verify if XML declaration is present
3. ✅ If missing:
   - Create a backup (`RcloneGui.slnx.backup`)
   - Add the XML declaration: `<?xml version="1.0" encoding="utf-8"?>`
   - Verify the fix worked
4. ✅ If already fixed, inform you

## Safe to Use

- Creates backup before making changes
- Only modifies the file if needed
- Easy to revert if needed: `mv RcloneGui.slnx.backup RcloneGui.slnx`

## After Running

Test the build:
```bash
dotnet build RcloneGui.slnx --configuration Release
```

Run the application:
```bash
./run.sh          # Linux / macOS
run.bat           # Windows
```

## More Information

- See [ACTUALIZAR_Y_EJECUTAR.md](ACTUALIZAR_Y_EJECUTAR.md) for Spanish documentation
- See [UPDATE_AND_RUN.md](UPDATE_AND_RUN.md) for English documentation
