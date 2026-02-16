@echo off
REM Fix script for MSB4068 error in RcloneGui.slnx
REM This script adds the XML declaration to RcloneGui.slnx if it's missing

echo ==================================================
echo   Fix MSB4068 Error - RcloneGui.slnx
echo ==================================================
echo.

set SLNX_FILE=RcloneGui.slnx

REM Check if file exists
if not exist "%SLNX_FILE%" (
    echo [ERROR] %SLNX_FILE% not found
    echo         Make sure you're in the project root directory
    pause
    exit /b 1
)

echo Checking %SLNX_FILE%...

REM Check if XML declaration is already present
for /f "delims=" %%i in ('type "%SLNX_FILE%" ^| findstr /C:"<?xml version"') do set FOUND=%%i

if defined FOUND (
    echo [OK] XML declaration already exists!
    echo.
    echo The file is already fixed. If you still get the error:
    echo 1. Make sure you saved the file
    echo 2. Try: dotnet clean ^&^& dotnet build RcloneGui.slnx --configuration Release
    echo 3. Check the troubleshooting section in ACTUALIZAR_Y_EJECUTAR.md
    pause
    exit /b 0
)

echo [WARNING] XML declaration is missing
echo.

REM Create backup
set BACKUP_FILE=%SLNX_FILE%.backup
copy "%SLNX_FILE%" "%BACKUP_FILE%" >nul
echo [OK] Backup created: %BACKUP_FILE%

REM Add XML declaration
echo ^<?xml version="1.0" encoding="utf-8"?^> > "%SLNX_FILE%.tmp"
type "%SLNX_FILE%" >> "%SLNX_FILE%.tmp"
move /y "%SLNX_FILE%.tmp" "%SLNX_FILE%" >nul

echo [OK] XML declaration added
echo.

REM Verify the fix
for /f "delims=" %%i in ('type "%SLNX_FILE%" ^| findstr /C:"<?xml version"') do set VERIFIED=%%i

if defined VERIFIED (
    echo [SUCCESS] Fix applied successfully!
    echo.
    echo Next steps:
    echo 1. Test the build: dotnet build RcloneGui.slnx --configuration Release
    echo 2. Run the application: run.bat
    echo.
    echo If you want to revert: move %BACKUP_FILE% %SLNX_FILE%
) else (
    echo [ERROR] Fix failed. Restoring backup...
    move /y "%BACKUP_FILE%" "%SLNX_FILE%" >nul
    echo         Original file restored
    pause
    exit /b 1
)

echo ==================================================
pause
