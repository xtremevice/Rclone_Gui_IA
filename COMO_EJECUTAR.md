# Cómo Ejecutar Rclone GUI - Guía Completa por Plataforma

Esta guía detalla cómo ejecutar Rclone GUI en Windows, Linux y macOS, incluyendo todos los métodos disponibles.

## 📋 Tabla de Contenidos

- [Windows](#windows)
- [Linux](#linux)
- [macOS](#macos)
  - [macOS Intel](#macos-intel)
  - [macOS Apple Silicon (M1/M2/M3)](#macos-apple-silicon-m1m2m3)
- [Verificación de Instalación](#verificación-de-instalación)
- [Solución de Problemas Comunes](#solución-de-problemas-comunes)

---

## Windows

### Requisitos Previos

1. **.NET 8.0 SDK o superior**
   - Descargar de: https://dotnet.microsoft.com/download/dotnet/8.0
   - Durante la instalación, asegúrate de marcar "Add to PATH"

2. **Rclone**
   - Descargar de: https://rclone.org/downloads/
   - Extraer el archivo ZIP
   - Agregar al PATH del sistema o copiar `rclone.exe` a una carpeta conocida

### Método 1: Script Automático (Recomendado)

El método más fácil es usar el script incluido:

```cmd
run.bat
```

Este script:
- ✅ Verifica que .NET esté instalado
- ✅ Verifica que Rclone esté instalado (advertencia si falta)
- ✅ Compila la aplicación automáticamente
- ✅ Ejecuta la aplicación

### Método 2: Línea de Comandos Manual

Abre PowerShell o CMD en la carpeta del proyecto:

```cmd
# Compilar el proyecto
dotnet build --configuration Release

# Ejecutar la aplicación
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

### Método 3: Visual Studio

Si tienes Visual Studio 2022:

1. Abre el archivo `RcloneGui.slnx`
2. Selecciona `RcloneGui` como proyecto de inicio
3. Presiona `F5` o haz clic en "Start"

### Agregar Rclone al PATH en Windows

Si recibes el error "Rclone not found":

1. Descarga Rclone de https://rclone.org/downloads/
2. Extrae `rclone.exe` a `C:\Program Files\Rclone\`
3. Agregar al PATH:
   - Presiona `Win + X` y selecciona "System"
   - Click en "Advanced system settings"
   - Click en "Environment Variables"
   - En "System variables", selecciona "Path" y click "Edit"
   - Click "New" y agrega: `C:\Program Files\Rclone`
   - Click "OK" en todas las ventanas
4. Abre una nueva ventana de CMD y verifica:
   ```cmd
   rclone version
   ```

---

## Linux

### Requisitos Previos

1. **.NET 8.0 SDK o superior**

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

O con el gestor de paquetes:
```bash
# Ubuntu/Debian
sudo apt install rclone

# Fedora
sudo dnf install rclone

# Arch Linux
sudo pacman -S rclone
```

### Método 1: Script Automático (Recomendado)

El método más fácil es usar el script incluido:

```bash
# Dale permisos de ejecución (solo la primera vez)
chmod +x run.sh

# Ejecuta el script
./run.sh
```

Este script:
- ✅ Verifica que .NET esté instalado
- ✅ Verifica que Rclone esté instalado (advertencia si falta)
- ✅ Compila la aplicación automáticamente
- ✅ Ejecuta la aplicación

### Método 2: Línea de Comandos Manual

```bash
# Compilar el proyecto
dotnet build --configuration Release

# Ejecutar la aplicación
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

### Método 3: Usando JetBrains Rider

Si tienes Rider instalado:

1. Abre la carpeta del proyecto en Rider
2. Selecciona la configuración de ejecución `RcloneGui`
3. Presiona `Shift+F10` o click en el botón "Run"

### Notas para Linux

- En algunas distribuciones puede necesitar instalar dependencias adicionales para Avalonia:
  ```bash
  sudo apt install libx11-dev libice-dev libsm-dev
  ```

- Si usas Wayland, puede necesitar ejecutar con XWayland:
  ```bash
  GDK_BACKEND=x11 ./run.sh
  ```

---

## macOS

### macOS Intel

#### Requisitos Previos

1. **Homebrew** (si no lo tienes):
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

#### Método 1: Script Automático (Recomendado)

```bash
# Dale permisos de ejecución (solo la primera vez)
chmod +x run.sh

# Ejecuta el script
./run.sh
```

#### Método 2: Línea de Comandos Manual

```bash
# Compilar el proyecto
dotnet build --configuration Release

# Ejecutar la aplicación
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

---

### macOS Apple Silicon (M1/M2/M3)

macOS con procesadores Apple Silicon (M1, M2, M3) requiere algunos pasos adicionales. La aplicación está completamente optimizada para estos procesadores.

#### Requisitos Previos

1. **Homebrew** (si no lo tienes):
   ```bash
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   ```

   Después de instalar, asegúrate de agregar Homebrew al PATH:
   ```bash
   echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> ~/.zprofile
   eval "$(/opt/homebrew/bin/brew shellenv)"
   ```

2. **Instalación Completa con Script Automático**:
   
   El proyecto incluye un script que instala todo automáticamente:
   
   ```bash
   # Dale permisos de ejecución (solo la primera vez)
   chmod +x setup-mac-silicon.sh
   
   # Ejecuta el script de instalación
   ./setup-mac-silicon.sh
   ```
   
   Este script instala automáticamente:
   - ✅ Homebrew (si no está instalado)
   - ✅ .NET 8.0 SDK
   - ✅ Rclone

3. **Instalación Manual** (si el script falla):
   
   ```bash
   # Instalar .NET 8.0 SDK
   brew install dotnet@8
   
   # Instalar Rclone
   brew install rclone
   ```

#### Método 1: Script Automático (Recomendado)

Después de instalar los requisitos previos:

```bash
# Dale permisos de ejecución (solo la primera vez)
chmod +x run.sh

# Ejecuta el script
./run.sh
```

#### Método 2: Línea de Comandos Manual

```bash
# Compilar el proyecto
dotnet build --configuration Release

# Ejecutar la aplicación
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

#### Características Específicas de Apple Silicon

La aplicación incluye optimizaciones para Apple Silicon:

- **Ruta de Rclone Optimizada**: Detecta automáticamente `/opt/homebrew/bin/rclone` (ubicación en Apple Silicon)
- **Comandos de Bisync**: Genera comandos optimizados cuando activas "Generate for Mac Silicon"
- **Rendimiento ARM64**: Ejecución nativa en arquitectura ARM64

#### Verificación de Apple Silicon

Para confirmar que estás ejecutando en Apple Silicon:

```bash
# Ver arquitectura del sistema
uname -m
# Debe mostrar: arm64

# Ver versión de .NET
dotnet --version

# Ver ubicación de Rclone
which rclone
# En Apple Silicon debe mostrar: /opt/homebrew/bin/rclone
# En Intel debe mostrar: /usr/local/bin/rclone
```

---

## Verificación de Instalación

Antes de ejecutar la aplicación, verifica que todo esté instalado correctamente:

### Verificar .NET

```bash
# Windows (CMD/PowerShell), Linux, macOS
dotnet --version
```

Debe mostrar versión 8.0.x o superior.

### Verificar Rclone

```bash
# Windows (CMD/PowerShell), Linux, macOS
rclone version
```

Debe mostrar la versión de Rclone instalada.

### Verificar la Compilación

```bash
# Todos los sistemas
dotnet build
```

Si la compilación es exitosa, verás: `Build succeeded.`

---

## Solución de Problemas Comunes

### Error: "dotnet: command not found"

**Causa**: .NET SDK no está instalado o no está en el PATH.

**Solución**:
- **Windows**: Reinstala .NET SDK y asegúrate de marcar "Add to PATH"
- **Linux**: Instala .NET SDK usando el gestor de paquetes
- **macOS**: Instala con `brew install dotnet@8`

### Error: "rclone: command not found"

**Causa**: Rclone no está instalado o no está en el PATH.

**Solución**:
- **Windows**: Sigue los pasos en [Agregar Rclone al PATH en Windows](#agregar-rclone-al-path-en-windows)
- **Linux**: Instala con `curl https://rclone.org/install.sh | sudo bash`
- **macOS**: Instala con `brew install rclone`

### Error: "Could not load file or assembly"

**Causa**: Archivos de compilación corruptos o incompletos.

**Solución**:
```bash
# Limpiar y recompilar
dotnet clean
dotnet build --configuration Release
```

### La aplicación no muestra la ventana (Linux)

**Causa**: Faltan dependencias de Avalonia UI.

**Solución**:
```bash
sudo apt install libx11-dev libice-dev libsm-dev
```

### OAuth2 no abre el navegador

**Causa**: Problema con la apertura automática del navegador.

**Solución**:
1. La aplicación mostrará una URL en el mensaje de estado
2. Copia la URL manualmente
3. Ábrela en tu navegador
4. Completa la autenticación
5. La aplicación detectará el token automáticamente

### Error: "Access denied" al ejecutar scripts

**Windows**:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**Linux/macOS**:
```bash
chmod +x run.sh
chmod +x setup-mac-silicon.sh
```

### La compilación es muy lenta

**Solución**: Usa compilación incremental y considera compilar en modo Debug para desarrollo:
```bash
dotnet build  # Debug es más rápido para desarrollo
```

Usa Release solo para producción:
```bash
dotnet build --configuration Release
```

### Error: "framework version 8.0.x not found"

**Causa**: Se instaló el runtime pero no el SDK.

**Solución**: Instala el SDK completo, no solo el runtime:
- Windows/macOS: Descarga el SDK desde https://dotnet.microsoft.com/download
- Linux: Usa el gestor de paquetes para instalar `dotnet-sdk-8.0`

---

## Ejecutar desde Cualquier Ubicación

### Windows

Crea un archivo batch en una carpeta del PATH:

```batch
@echo off
cd C:\ruta\a\Rclone_Gui_IA
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

### Linux/macOS

Crea un alias en tu `.bashrc` o `.zshrc`:

```bash
alias rclonegui='cd /ruta/a/Rclone_Gui_IA && ./run.sh'
```

Luego recarga el shell:
```bash
source ~/.bashrc  # o source ~/.zshrc
```

Ahora puedes ejecutar desde cualquier lugar:
```bash
rclonegui
```

---

## Resumen de Comandos Rápidos

### Windows
```cmd
# Instalación
# 1. Instalar .NET 8.0 SDK desde https://dotnet.microsoft.com/download
# 2. Instalar Rclone desde https://rclone.org/downloads/

# Ejecutar
run.bat
```

### Linux (Ubuntu/Debian)
```bash
# Instalación
sudo apt-get install -y dotnet-sdk-8.0
curl https://rclone.org/install.sh | sudo bash

# Ejecutar
chmod +x run.sh
./run.sh
```

### macOS Intel
```bash
# Instalación
brew install dotnet@8 rclone

# Ejecutar
chmod +x run.sh
./run.sh
```

### macOS Apple Silicon (M1/M2/M3)
```bash
# Instalación completa
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh

# Ejecutar
./run.sh
```

---

## Documentación Adicional

- **README.md**: Documentación general del proyecto
- **QUICKSTART.md**: Guía de inicio rápido
- **MAC_SILICON_SETUP.md**: Guía detallada para Mac Silicon
- **BISYNC_GUIDE.md**: Guía de sincronización bidireccional
- **LEEME_PRIMERO.md**: Información sobre las ramas del proyecto

---

## Soporte y Contribuciones

- **Issues**: https://github.com/xtremevice/Rclone_Gui_IA/issues
- **Pull Requests**: https://github.com/xtremevice/Rclone_Gui_IA/pulls
- **Rclone Documentation**: https://rclone.org/docs/

---

**Última actualización**: 2026-02-16
