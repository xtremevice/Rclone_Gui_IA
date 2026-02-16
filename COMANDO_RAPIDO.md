# 🚀 COMANDO RÁPIDO - Descargar y Ejecutar

## El Comando Que Necesitas

### Linux / macOS

```bash
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

### Windows

```cmd
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && run.bat
```

---

## ¿Primera Vez?

Si no tienes el proyecto todavía:

```bash
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA
git pull origin main && dotnet build RcloneGui.slnx --configuration Release && ./run.sh
```

---

## ¿Qué Hace Este Comando?

1. **`git pull origin main`** - Descarga la última versión corregida desde GitHub
2. **`dotnet build RcloneGui.slnx --configuration Release`** - Compila la aplicación
3. **`./run.sh`** (o `run.bat`) - Ejecuta la aplicación

---

## ¿No Funciona?

### Error: "git: command not found"
Instala Git: https://git-scm.com/downloads

### Error: "dotnet: command not found"  
Instala .NET 8.0: https://dotnet.microsoft.com/download/dotnet/8.0

### Error: MSB4068
Ejecuta el fix automático:
```bash
bash fix-msb4068.sh
```

### Más Ayuda
- Ver guía completa: [ACTUALIZAR_Y_EJECUTAR.md](ACTUALIZAR_Y_EJECUTAR.md)
- Ver guía en inglés: [UPDATE_AND_RUN.md](UPDATE_AND_RUN.md)

---

**Última actualización:** 2026-02-16
