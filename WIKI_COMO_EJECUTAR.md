# Wiki: Ejecutar Rclone GUI en Todas las Plataformas

> Esta página puede copiarse directamente a la Wiki de GitHub

## Tabla de Contenidos

1. [Ejecución Rápida](#ejecución-rápida)
2. [Windows](#windows)
3. [Linux](#linux)  
4. [macOS](#macos)
5. [Solución de Problemas](#solución-de-problemas)

---

## Ejecución Rápida

### ¿Primera vez usando Rclone GUI?

**Paso 1: Instala los requisitos**

| Sistema | Requisitos |
|---------|-----------|
| Windows | [.NET 8.0 SDK](https://dotnet.microsoft.com/download) + [Rclone](https://rclone.org/downloads/) |
| Linux | `sudo apt install dotnet-sdk-8.0 rclone` |
| macOS | `brew install dotnet@8 rclone` |
| macOS M1/M2/M3 | Usa el script: `./setup-mac-silicon.sh` |

**Paso 2: Ejecuta la aplicación**

```bash
# Windows
run.bat

# Linux / macOS
chmod +x run.sh
./run.sh
```

---

## Windows

### Instalación Completa

#### 1️⃣ Instalar .NET 8.0 SDK

1. Ve a https://dotnet.microsoft.com/download/dotnet/8.0
2. Descarga "SDK x64" para Windows
3. Ejecuta el instalador
4. ✅ Marca "Add to PATH" durante la instalación
5. Verifica en CMD:
   ```cmd
   dotnet --version
   ```

#### 2️⃣ Instalar Rclone

1. Ve a https://rclone.org/downloads/
2. Descarga "Windows Intel/AMD - 64 Bit"
3. Extrae `rclone.exe`
4. Opción A - Agregar al PATH:
   - Crea carpeta: `C:\Program Files\Rclone\`
   - Copia `rclone.exe` ahí
   - Agregar al PATH:
     * `Win + X` → "System" → "Advanced system settings"
     * "Environment Variables" → En "System variables" selecciona "Path" → "Edit"
     * "New" → Agrega: `C:\Program Files\Rclone`
     * Click "OK" en todo
5. Opción B - Copiar al proyecto:
   - Copia `rclone.exe` a la carpeta `Rclone_Gui_IA`
6. Verifica en nueva ventana CMD:
   ```cmd
   rclone version
   ```

#### 3️⃣ Ejecutar la Aplicación

**Método 1: Script Automático** ⭐ Recomendado
```cmd
run.bat
```

**Método 2: Manual**
```cmd
dotnet build --configuration Release
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

**Método 3: Visual Studio 2022**
1. Abre `RcloneGui.slnx`
2. Presiona `F5`

---

## Linux

### Instalación Completa

#### 1️⃣ Instalar .NET 8.0 SDK

**Ubuntu / Debian:**
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

Verifica:
```bash
dotnet --version
```

#### 2️⃣ Instalar Rclone

```bash
curl https://rclone.org/install.sh | sudo bash
```

O con gestor de paquetes:
```bash
# Ubuntu/Debian
sudo apt install rclone

# Fedora  
sudo dnf install rclone

# Arch
sudo pacman -S rclone
```

Verifica:
```bash
rclone version
```

#### 3️⃣ Ejecutar la Aplicación

**Método 1: Script Automático** ⭐ Recomendado
```bash
chmod +x run.sh
./run.sh
```

**Método 2: Manual**
```bash
dotnet build --configuration Release
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

#### Notas para Linux

Si tienes problemas con la interfaz gráfica:

```bash
# Instalar dependencias de Avalonia
sudo apt install libx11-dev libice-dev libsm-dev

# Si usas Wayland
GDK_BACKEND=x11 ./run.sh
```

---

## macOS

### macOS Intel

#### 1️⃣ Instalar Homebrew (si no lo tienes)

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

#### 2️⃣ Instalar Requisitos

```bash
brew install dotnet@8 rclone
```

#### 3️⃣ Ejecutar

```bash
chmod +x run.sh
./run.sh
```

---

### macOS Apple Silicon (M1/M2/M3) 🍎

#### Instalación Automática Completa ⭐ RECOMENDADO

El proyecto incluye un script que instala todo:

```bash
# Dar permisos y ejecutar
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh
```

Este script instala:
- ✅ Homebrew (si no está)
- ✅ .NET 8.0 SDK para ARM64
- ✅ Rclone para ARM64

#### Instalación Manual

**1️⃣ Instalar Homebrew** (si no lo tienes)
```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Agregar al PATH (importante en Apple Silicon)
echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> ~/.zprofile
eval "$(/opt/homebrew/bin/brew shellenv)"
```

**2️⃣ Instalar Requisitos**
```bash
brew install dotnet@8 rclone
```

**3️⃣ Verificar Instalación**
```bash
# Debe mostrar: arm64
uname -m

# Debe mostrar versión 8.x
dotnet --version

# En Apple Silicon debe mostrar: /opt/homebrew/bin/rclone
which rclone
```

#### Ejecutar la Aplicación

```bash
chmod +x run.sh
./run.sh
```

#### Características Especiales Apple Silicon

✨ **Optimizaciones incluidas:**

- Detección automática de `/opt/homebrew/bin/rclone`
- Comandos bisync optimizados para ARM64
- Casilla "Generate for Mac Silicon" en la interfaz
- Rendimiento nativo en arquitectura ARM64

---

## Solución de Problemas

### ❌ "dotnet: command not found"

**Causa:** .NET no está instalado o no está en PATH

**Solución por Sistema:**

| Sistema | Solución |
|---------|----------|
| Windows | Reinstalar .NET SDK, marcar "Add to PATH" |
| Linux | `sudo apt install dotnet-sdk-8.0` |
| macOS | `brew install dotnet@8` |
| macOS (Apple Silicon) | `eval "$(/opt/homebrew/bin/brew shellenv)"` |

---

### ❌ "rclone: command not found"

**Causa:** Rclone no está instalado o no está en PATH

**Solución por Sistema:**

| Sistema | Solución |
|---------|----------|
| Windows | Ver sección [Instalar Rclone en Windows](#2️⃣-instalar-rclone) |
| Linux | `curl https://rclone.org/install.sh \| sudo bash` |
| macOS | `brew install rclone` |

---

### ❌ "Could not load file or assembly"

**Solución:**
```bash
dotnet clean
dotnet build --configuration Release
```

---

### ❌ La ventana no aparece (Linux)

**Solución:**
```bash
sudo apt install libx11-dev libice-dev libsm-dev
```

Si usas Wayland:
```bash
GDK_BACKEND=x11 ./run.sh
```

---

### ❌ OAuth2 no abre el navegador

**Solución:**
1. La app mostrará una URL en el mensaje
2. Copia la URL
3. Pégala en tu navegador
4. Completa la autenticación
5. Vuelve a la app (detectará el token automáticamente)

---

### ❌ "Access denied" al ejecutar scripts

**Windows:**
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**Linux/macOS:**
```bash
chmod +x run.sh
chmod +x setup-mac-silicon.sh
```

---

### ❌ Error "framework version 8.0.x not found"

**Causa:** Se instaló solo el Runtime, no el SDK

**Solución:** Instalar el **SDK completo**:
- Windows/macOS: https://dotnet.microsoft.com/download
- Linux: `sudo apt install dotnet-sdk-8.0`

---

## Crear Alias / Acceso Directo

### Windows

Crear archivo `rclonegui.bat` en `C:\Windows\`:

```batch
@echo off
cd C:\ruta\a\Rclone_Gui_IA
dotnet run --project src\RcloneGui\RcloneGui.csproj --configuration Release
```

Ejecutar desde CMD:
```cmd
rclonegui
```

### Linux/macOS

Agregar a `~/.bashrc` o `~/.zshrc`:

```bash
alias rclonegui='cd /ruta/a/Rclone_Gui_IA && ./run.sh'
```

Recargar:
```bash
source ~/.bashrc
```

Ejecutar desde cualquier lugar:
```bash
rclonegui
```

---

## Comandos de Verificación Rápida

```bash
# Ver versión de .NET
dotnet --version

# Ver versión de Rclone
rclone version

# Ver arquitectura (macOS)
uname -m

# Probar compilación
dotnet build

# Ver ubicación de Rclone
which rclone  # Linux/macOS
where rclone  # Windows
```

---

## Enlaces Útiles

- 📘 [README.md](README.md) - Documentación completa
- 🚀 [QUICKSTART.md](QUICKSTART.md) - Inicio rápido
- 🍎 [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md) - Guía Mac Silicon detallada
- 🔄 [BISYNC_GUIDE.md](BISYNC_GUIDE.md) - Guía de sincronización
- 🌐 [COMO_EJECUTAR.md](COMO_EJECUTAR.md) - Esta guía en detalle
- 🌐 [HOW_TO_RUN.md](HOW_TO_RUN.md) - Esta guía en inglés

---

## Soporte

- **Reportar problemas**: https://github.com/xtremevice/Rclone_Gui_IA/issues
- **Documentación Rclone**: https://rclone.org/docs/
- **Documentación .NET**: https://docs.microsoft.com/dotnet/

---

**📅 Última actualización:** 2026-02-16  
**✍️ Versión:** 1.0  
**🏷️ Tags:** #rclone #gui #cross-platform #dotnet #avalonia
