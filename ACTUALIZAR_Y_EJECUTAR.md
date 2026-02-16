# 🚀 Actualizar y Ejecutar Rclone GUI

**Comandos rápidos para actualizar a la última versión y ejecutar la aplicación**

---

## 📦 Actualizar y Ejecutar (Todos los Sistemas)

### Opción 1: Comando Único (Recomendado)

```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

**Para Windows:**
```cmd
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

### Opción 2: Paso a Paso

```bash
# 1. Actualizar el código desde GitHub
git pull origin main

# 2. Compilar la aplicación
dotnet build RcloneGui.slnx --configuration Release

# 3. Ejecutar la aplicación
./run.sh          # Linux / macOS
run.bat           # Windows
```

---

## 🔄 Solo Actualizar (Sin Ejecutar)

```bash
# Descargar la última versión
git pull origin main

# Compilar
dotnet build RcloneGui.slnx --configuration Release
```

---

## ▶️ Solo Ejecutar (Sin Actualizar)

```bash
./run.sh          # Linux / macOS
run.bat           # Windows
```

O manualmente:
```bash
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release
```

---

## 🆕 Primera Instalación

Si es tu primera vez, necesitas clonar el repositorio:

```bash
# Clonar el repositorio
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Instalar y ejecutar
./run.sh          # Linux / macOS
run.bat           # Windows
```

**Requisitos previos:**
- .NET 8.0 SDK o superior → https://dotnet.microsoft.com/download
- Rclone → https://rclone.org/downloads/

---

## 🍎 macOS Apple Silicon (M1/M2/M3)

### Primera Instalación Completa

```bash
# Clonar el repositorio
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Instalación automática de todo (Homebrew, .NET, Rclone)
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh

# Ejecutar
./run.sh
```

### Actualizar y Ejecutar

```bash
cd Rclone_Gui_IA
git pull origin main
dotnet build RcloneGui.slnx --configuration Release
./run.sh
```

O en un solo comando:
```bash
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

---

## 🔍 Verificar la Versión Actual

```bash
# Ver la versión instalada
git log -1 --oneline

# Ver todas las actualizaciones disponibles
git fetch origin
git log HEAD..origin/main --oneline
```

---

## ⚙️ Comandos Útiles

### Limpiar y Recompilar

Si tienes problemas con la compilación:

```bash
# Limpiar artefactos de compilación anteriores
dotnet clean

# Recompilar desde cero
dotnet build RcloneGui.slnx --configuration Release
```

### Forzar Actualización

Si hay conflictos o cambios locales:

```bash
# Guardar cambios locales (opcional)
git stash

# Forzar actualización
git fetch origin
git reset --hard origin/main

# Recuperar cambios guardados (si usaste stash)
git stash pop
```

---

## 📋 Resumen de Comandos por Plataforma

### Windows

```cmd
REM Actualizar
git pull origin main

REM Compilar
dotnet build RcloneGui.slnx --configuration Release

REM Ejecutar
run.bat
```

**Todo en uno:**
```cmd
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

### Linux

```bash
# Actualizar
git pull origin main

# Compilar
dotnet build RcloneGui.slnx --configuration Release

# Ejecutar
./run.sh
```

**Todo en uno:**
```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

### macOS (Intel y Apple Silicon)

```bash
# Actualizar
git pull origin main

# Compilar
dotnet build RcloneGui.slnx --configuration Release

# Ejecutar
./run.sh
```

**Todo en uno:**
```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

---

## 🆘 Solución de Problemas

### Error: "git: command not found"

Instala Git:
- **Windows**: https://git-scm.com/download/win
- **macOS**: `brew install git` o ya viene incluido
- **Linux**: `sudo apt install git` (Ubuntu/Debian)

### Error: "dotnet: command not found"

Instala .NET SDK:
- Descarga desde: https://dotnet.microsoft.com/download/dotnet/8.0
- Ver guía completa: [COMO_EJECUTAR.md](COMO_EJECUTAR.md)

### Error: "Already up to date" pero no se ve la actualización

```bash
# Forzar actualización
git fetch origin
git reset --hard origin/main
dotnet build RcloneGui.slnx --configuration Release
```

### Error al ejecutar: Display/X11 error

Esto es normal en entornos sin interfaz gráfica (servidores). La aplicación necesita un entorno de escritorio para ejecutarse.

### Error: MSB4068 en RcloneGui.slnx (macOS)

Si después de actualizar ves este error:
```
RcloneGui.slnx(1,1): error MSB4068: The element
```

**Solución automática (Recomendado):**

Ejecuta el script de reparación incluido:
```bash
# Linux / macOS
bash fix-msb4068.sh

# Windows
fix-msb4068.bat
```

El script:
- ✅ Detecta automáticamente si falta la declaración XML
- ✅ Crea un backup antes de hacer cambios
- ✅ Agrega la declaración XML si es necesaria
- ✅ Verifica que el fix funcionó

**Solución manual:**

1. Verifica que tienes la última versión con el fix:
```bash
git pull origin main
```

2. Si el error persiste, verifica el contenido de RcloneGui.slnx:
```bash
head -1 RcloneGui.slnx
```

Debe mostrar: `<?xml version="1.0" encoding="utf-8"?>`

3. Si NO tiene la declaración XML, actualiza manualmente:
```bash
# Opción A: Descargar la versión corregida
curl -o RcloneGui.slnx https://raw.githubusercontent.com/xtremevice/Rclone_Gui_IA/main/RcloneGui.slnx

# Opción B: Agregar manualmente la declaración XML
sed -i '1s/^/<?xml version="1.0" encoding="utf-8"?>\n/' RcloneGui.slnx
```

4. Vuelve a compilar:
```bash
dotnet build RcloneGui.slnx --configuration Release
```

**Nota**: Este error ocurre en versiones antiguas del repositorio. La solución permanente es asegurarse de tener la última versión.

---

## 📚 Documentación Adicional

- **Guía completa de ejecución**: [COMO_EJECUTAR.md](COMO_EJECUTAR.md)
- **Guía en inglés**: [UPDATE_AND_RUN.md](UPDATE_AND_RUN.md)
- **Mac Silicon setup**: [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md)
- **Inicio rápido**: [QUICKSTART.md](QUICKSTART.md)
- **Wiki**: [WIKI_HOME.md](WIKI_HOME.md)

---

## 🎯 Comando Favorito (Copia y Pega)

**El comando más usado - Actualizar y ejecutar en un solo paso:**

```bash
# Linux / macOS
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

```cmd
# Windows
cd Rclone_Gui_IA && git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

---

**Última actualización:** 2026-02-16
