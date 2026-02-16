# Guía de Instalación para Mac Silicon (Apple M1/M2/M3)

Esta guía te mostrará cómo descargar, compilar y ejecutar Rclone GUI en tu Mac con procesador Apple Silicon (M1, M2 o M3).

## 🚀 Instalación Rápida (Recomendado)

Si quieres instalar todo automáticamente, después de descargar el proyecto ejecuta:

```bash
./setup-mac-silicon.sh
```

Este script instalará automáticamente:
- Homebrew (si no lo tienes)
- .NET 8.0 SDK
- Rclone

Luego puedes ejecutar la aplicación con:

```bash
./run.sh
```

## 📋 Instalación Manual (Paso a Paso)

Si prefieres instalar manualmente o el script automático falla, sigue estos pasos:

## Requisitos Previos

### 1. Instalar Homebrew (si no lo tienes)

Homebrew es el gestor de paquetes para macOS. Abre la Terminal y ejecuta:

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

Después de la instalación, asegúrate de seguir las instrucciones en pantalla para agregar Homebrew a tu PATH. Para Mac Silicon, esto generalmente significa ejecutar:

```bash
echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> ~/.zprofile
eval "$(/opt/homebrew/bin/brew shellenv)"
```

### 2. Instalar .NET 8.0 SDK

Ejecuta en la Terminal:

```bash
brew install dotnet@8
```

Verifica la instalación:

```bash
dotnet --version
```

Deberías ver algo como `8.0.x` o superior.

### 3. Instalar Rclone

Ejecuta en la Terminal:

```bash
brew install rclone
```

Verifica la instalación:

```bash
rclone version
```

**Nota importante**: En Mac Silicon, Rclone se instala en `/opt/homebrew/bin/rclone`. La aplicación detecta automáticamente esta ruta cuando generas comandos de bisync.

## Descargar el Proyecto

### Opción 1: Usando Git (Recomendado)

Si tienes Git instalado (viene preinstalado en macOS):

```bash
# Navega a donde quieras guardar el proyecto, por ejemplo:
cd ~/Documents

# Clona el repositorio
git clone https://github.com/xtremevice/Rclone_Gui_IA.git

# Entra al directorio del proyecto
cd Rclone_Gui_IA
```

### Opción 2: Descarga Manual

1. Ve a https://github.com/xtremevice/Rclone_Gui_IA
2. Haz clic en el botón verde "Code"
3. Selecciona "Download ZIP"
4. Descomprime el archivo ZIP
5. Abre la Terminal y navega a la carpeta:
   ```bash
   cd ~/Downloads/Rclone_Gui_IA-main
   ```

## Compilar y Ejecutar

### Método 1: Usando el Script de Inicio (Más Fácil)

El proyecto incluye un script que hace todo automáticamente:

```bash
# Dale permisos de ejecución al script (solo la primera vez)
chmod +x run.sh

# Ejecuta el script
./run.sh
```

El script:
- Verifica que .NET y Rclone estén instalados
- Compila la aplicación
- La ejecuta automáticamente

### Método 2: Comandos Manuales

Si prefieres hacerlo paso a paso:

```bash
# 1. Compilar el proyecto
dotnet build

# 2. Ejecutar la aplicación
dotnet run --project src/RcloneGui/RcloneGui.csproj
```

O para compilación optimizada (Release):

```bash
dotnet build --configuration Release
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

## Primer Uso

Una vez que la aplicación se inicie, verás la ventana principal con tres secciones:

### 1. My Remotes (Mis Remotos)
- Muestra tus cuentas de almacenamiento en la nube configuradas
- Puedes probar y eliminar remotos existentes

### 2. Add Account (Agregar Cuenta)
- Agrega nuevas cuentas de almacenamiento (OneDrive, Google Drive, Dropbox, etc.)
- Soporta autenticación OAuth2, usuario/contraseña y claves API

### 3. Bisync (Sincronización Bidireccional)
- **¡Nuevo!** Sincroniza archivos entre dos remotos o entre remoto y local
- **Optimizado para Mac Silicon**: Los comandos generados usan la ruta correcta `/opt/homebrew/bin/rclone`

## Usando Bisync en Mac Silicon

La función de Bisync está optimizada para Mac Silicon:

1. Ve a la pestaña "Bisync"
2. Marca la casilla "Generate for Mac Silicon (Apple M1/M2/M3)"
3. Configura tu sincronización:
   - Selecciona el remoto de origen
   - Selecciona el remoto de destino
   - Configura las opciones (Resync para primera vez, Dry Run para probar)
4. Haz clic en "Generate Command" para ver el comando
5. El comando usará `/opt/homebrew/bin/rclone` automáticamente

Ejemplo de comando generado:
```bash
/opt/homebrew/bin/rclone bisync "OneDrive:/Photos" "GoogleDrive:/Photos" --resync --check-access --max-delete 50 --conflict-resolve newer --compare size,modtime --verbose
```

## Solución de Problemas

### Error: "dotnet: command not found"

**Solución**: Asegúrate de que .NET esté en tu PATH. Ejecuta:

```bash
echo 'export PATH="$PATH:/usr/local/share/dotnet"' >> ~/.zprofile
source ~/.zprofile
```

### Error: "rclone: command not found"

**Solución**: Rclone no está instalado o no está en el PATH. Instálalo:

```bash
brew install rclone
```

Verifica que esté en la ruta correcta:

```bash
which rclone
# Debería mostrar: /opt/homebrew/bin/rclone
```

### Error: "No se puede abrir la aplicación porque Apple no puede verificar..."

**Solución**: macOS requiere que las aplicaciones estén firmadas. Como esta es una aplicación de desarrollo:

1. Ve a Preferencias del Sistema > Seguridad y Privacidad
2. En la pestaña "General", haz clic en "Abrir de todas formas"

O ejecuta desde Terminal (método recomendado):

```bash
./run.sh
```

### La aplicación compila pero no se ve la ventana

**Solución**: Asegúrate de tener X11 o el sistema de ventanas de Avalonia instalado:

```bash
brew install --cask xquartz
```

Luego reinicia la Terminal y vuelve a ejecutar.

### Error: "Could not load file or assembly"

**Solución**: Limpia y recompila:

```bash
dotnet clean
dotnet build --configuration Release
```

### Problemas con OAuth2 (OneDrive, Google Drive)

Si el navegador no se abre automáticamente:

1. La aplicación mostrará un mensaje con la URL
2. Copia y pega la URL en tu navegador manualmente
3. Completa la autenticación
4. La aplicación detectará el token automáticamente

## Funciones Específicas para Mac Silicon

### Comandos Optimizados

Cuando usas Bisync con la opción "Mac Silicon" activada:
- Usa la ruta nativa de Homebrew: `/opt/homebrew/bin/rclone`
- Optimizado para arquitectura ARM64
- Mejor rendimiento en procesadores Apple Silicon

### Verificar Arquitectura

Para confirmar que estás ejecutando la aplicación nativa de ARM64:

```bash
file $(which dotnet)
# Debería mencionar: arm64
```

## Actualizaciones Futuras

Para actualizar el proyecto a la última versión:

```bash
cd Rclone_Gui_IA
git pull origin main
dotnet build
```

## Comandos Útiles

```bash
# Ver versión de .NET
dotnet --version

# Ver versión de Rclone
rclone version

# Ver información del sistema
uname -m
# Debería mostrar: arm64

# Limpiar compilaciones anteriores
dotnet clean

# Compilar en modo Release (más rápido)
dotnet build --configuration Release

# Ejecutar tests (si existen)
dotnet test
```

## Recursos Adicionales

- **Documentación de Rclone**: https://rclone.org/docs/
- **Guía de Bisync**: Ver `BISYNC_GUIDE.md` en el proyecto
- **Documentación de .NET en macOS**: https://docs.microsoft.com/dotnet/core/install/macos

## Soporte

Si encuentras problemas:

1. Revisa esta guía y la sección de solución de problemas
2. Consulta `README.md` para documentación general
3. Consulta `BISYNC_GUIDE.md` para ayuda con sincronización
4. Abre un issue en: https://github.com/xtremevice/Rclone_Gui_IA/issues

## Notas de Seguridad

- Las contraseñas se obscurecen usando el comando `rclone obscure`
- Los tokens OAuth2 se almacenan de forma segura por Rclone
- La configuración se guarda en: `~/.config/rclone/rclone.conf`
- **Nunca compartas tu archivo de configuración** - contiene tus credenciales

## ¡Listo!

Ahora deberías tener Rclone GUI funcionando en tu Mac Silicon. La aplicación está completamente optimizada para procesadores Apple M1, M2 y M3.

¡Disfruta gestionando tu almacenamiento en la nube! 🚀
