@echo off
REM Rclone GUI Launcher Script for Windows

echo ================================
echo   Rclone GUI Launcher
echo ================================
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK not found!
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Display .NET version
for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo [OK] .NET SDK version: %DOTNET_VERSION%

REM Check if Rclone is installed
where rclone >nul 2>&1
if %errorlevel% neq 0 (
    echo WARNING: Rclone not found in PATH!
    echo The application will run but won't be able to manage remotes.
    echo Install Rclone from: https://rclone.org/downloads/
) else (
    for /f "tokens=*" %%i in ('rclone version ^| findstr /r "rclone v"') do (
        echo [OK] %%i
    )
)

echo.
echo Building application...
dotnet build --configuration Release

if %errorlevel% equ 0 (
    echo.
    echo [OK] Build successful!
    echo.
    echo Starting Rclone GUI...
    echo ================================
    echo.
    dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
) else (
    echo.
    echo ERROR: Build failed!
    echo Please check the error messages above.
    pause
    exit /b 1
)

pause
