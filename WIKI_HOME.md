# Rclone GUI - Documentación del Wiki / Wiki Documentation

> Esta página sirve como índice para toda la documentación del proyecto
> 
> This page serves as an index for all project documentation

---

## 🚀 Inicio Rápido / Quick Start

### ¿Cómo ejecuto la aplicación? / How do I run the application?

**La respuesta completa está aquí / The complete answer is here:**

- 🇪🇸 **Español**: [COMO_EJECUTAR.md](COMO_EJECUTAR.md) o [WIKI_COMO_EJECUTAR.md](WIKI_COMO_EJECUTAR.md)
- 🇬🇧 **English**: [HOW_TO_RUN.md](HOW_TO_RUN.md)

**Respuesta Rápida / Quick Answer:**

```bash
# Windows
run.bat

# Linux / macOS
chmod +x run.sh
./run.sh

# macOS Apple Silicon - Instalación completa / Complete setup
chmod +x setup-mac-silicon.sh
./setup-mac-silicon.sh
```

---

## 📚 Documentación Principal / Main Documentation

### Guías de Usuario / User Guides

| Documento | Descripción | Language |
|-----------|-------------|----------|
| [README.md](README.md) | Documentación principal del proyecto | 🇬🇧 EN |
| [COMO_EJECUTAR.md](COMO_EJECUTAR.md) | Guía completa de ejecución por plataforma | 🇪🇸 ES |
| [HOW_TO_RUN.md](HOW_TO_RUN.md) | Complete execution guide per platform | 🇬🇧 EN |
| [WIKI_COMO_EJECUTAR.md](WIKI_COMO_EJECUTAR.md) | Guía de ejecución formato Wiki | 🇪🇸 ES |
| [QUICKSTART.md](QUICKSTART.md) | Guía de inicio rápido | 🇬🇧 EN |
| [LEEME_PRIMERO.md](LEEME_PRIMERO.md) | Información sobre las ramas del proyecto | 🇪🇸 ES |

### Guías Específicas de Plataforma / Platform-Specific Guides

| Documento | Descripción | Language |
|-----------|-------------|----------|
| [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md) | Guía completa para Mac Silicon (M1/M2/M3) | 🇪🇸 ES |
| [MAC_SILICON_DOCS_SUMMARY.md](MAC_SILICON_DOCS_SUMMARY.md) | Resumen de la documentación Mac Silicon | 🇪🇸 ES |

### Guías de Características / Feature Guides

| Documento | Descripción | Language |
|-----------|-------------|----------|
| [BISYNC_GUIDE.md](BISYNC_GUIDE.md) | Guía completa de sincronización bidireccional | 🇬🇧 EN |
| [BISYNC_IMPLEMENTATION.md](BISYNC_IMPLEMENTATION.md) | Detalles de implementación de Bisync | 🇬🇧 EN |
| [BISYNC_UI_OVERVIEW.md](BISYNC_UI_OVERVIEW.md) | Descripción de la interfaz de Bisync | 🇬🇧 EN |

### Documentación Técnica / Technical Documentation

| Documento | Descripción | Language |
|-----------|-------------|----------|
| [ARCHITECTURE.md](ARCHITECTURE.md) | Arquitectura del proyecto | 🇬🇧 EN |
| [UI_DESIGN.md](UI_DESIGN.md) | Diseño de la interfaz de usuario | 🇬🇧 EN |
| [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) | Resumen de implementación | 🇬🇧 EN |

### Para Desarrolladores / For Developers

| Documento | Descripción | Language |
|-----------|-------------|----------|
| [CONTRIBUTING.md](CONTRIBUTING.md) | Guía de contribución | 🇬🇧 EN |
| [CHANGELOG.md](CHANGELOG.md) | Registro de cambios | 🇬🇧 EN |
| [MERGE_TO_MAIN_INSTRUCTIONS.md](MERGE_TO_MAIN_INSTRUCTIONS.md) | Instrucciones para merge a main | 🇬🇧 EN |

---

## 🎯 Casos de Uso Comunes / Common Use Cases

### 1. Primera Instalación / First-Time Installation

**Windows:**
1. Lee: [COMO_EJECUTAR.md - Windows](COMO_EJECUTAR.md#windows)
2. Instala .NET 8.0 SDK y Rclone
3. Ejecuta: `run.bat`

**Linux:**
1. Lee: [COMO_EJECUTAR.md - Linux](COMO_EJECUTAR.md#linux)
2. Instala .NET 8.0 SDK y Rclone
3. Ejecuta: `chmod +x run.sh && ./run.sh`

**macOS (Intel):**
1. Lee: [COMO_EJECUTAR.md - macOS](COMO_EJECUTAR.md#macos)
2. Instala con Homebrew: `brew install dotnet@8 rclone`
3. Ejecuta: `chmod +x run.sh && ./run.sh`

**macOS (Apple Silicon M1/M2/M3):**
1. Lee: [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md)
2. Ejecuta: `chmod +x setup-mac-silicon.sh && ./setup-mac-silicon.sh`
3. Ejecuta: `./run.sh`

### 2. Agregar una Cuenta de Almacenamiento / Adding a Storage Account

Ver: [README.md - Adding a New Account](README.md#adding-a-new-account)

### 3. Configurar Sincronización Bidireccional / Setting Up Bidirectional Sync

Ver: [BISYNC_GUIDE.md](BISYNC_GUIDE.md)

### 4. Solucionar Problemas / Troubleshooting

Ver:
- [COMO_EJECUTAR.md - Solución de Problemas](COMO_EJECUTAR.md#solución-de-problemas-comunes)
- [HOW_TO_RUN.md - Common Troubleshooting](HOW_TO_RUN.md#common-troubleshooting)

---

## 🔧 Comandos de Referencia Rápida / Quick Reference Commands

### Verificación de Instalación / Installation Verification

```bash
# Verificar .NET / Check .NET
dotnet --version

# Verificar Rclone / Check Rclone
rclone version

# Verificar arquitectura (macOS) / Check architecture (macOS)
uname -m
```

### Compilación y Ejecución / Build and Run

```bash
# Compilar / Build
dotnet build --configuration Release

# Ejecutar / Run
dotnet run --project src/RcloneGui/RcloneGui.csproj --configuration Release

# Limpiar y recompilar / Clean and rebuild
dotnet clean
dotnet build --configuration Release
```

---

## 🆘 Obtener Ayuda / Getting Help

### Problemas Comunes / Common Issues

1. **"dotnet: command not found"**
   - Ver: [COMO_EJECUTAR.md - Solución de Problemas](COMO_EJECUTAR.md#error-dotnet-command-not-found)

2. **"rclone: command not found"**
   - Ver: [COMO_EJECUTAR.md - Solución de Problemas](COMO_EJECUTAR.md#error-rclone-command-not-found)

3. **OAuth2 no abre el navegador / OAuth2 doesn't open browser**
   - Ver: [COMO_EJECUTAR.md - Solución de Problemas](COMO_EJECUTAR.md#oauth2-no-abre-el-navegador)

### Reportar un Problema / Report an Issue

- **GitHub Issues**: https://github.com/xtremevice/Rclone_Gui_IA/issues

### Recursos Externos / External Resources

- **Rclone Documentation**: https://rclone.org/docs/
- **.NET Documentation**: https://docs.microsoft.com/dotnet/

---

## 📝 Notas de Versión / Version Notes

- **Versión actual / Current version**: En desarrollo / In development
- **Última actualización / Last updated**: 2026-02-16
- **Rama principal / Main branch**: `main`
- **Rama de desarrollo / Development branch**: `copilot/add-rclone-user-interface`

---

## 🏗️ Estructura del Proyecto / Project Structure

```
Rclone_Gui_IA/
├── src/
│   ├── RcloneGui/              # Aplicación Avalonia UI
│   │   ├── ViewModels/         # MVVM ViewModels
│   │   ├── Views/              # XAML Views
│   │   └── Assets/             # Application assets
│   └── RcloneGui.Core/         # Lógica de negocio
│       ├── Models/             # Data models
│       └── Services/           # Servicios de Rclone
├── run.sh                      # Script de ejecución Linux/macOS
├── run.bat                     # Script de ejecución Windows
├── setup-mac-silicon.sh        # Script de instalación Mac Silicon
└── docs/                       # Documentación
```

---

## 🤝 Contribuir / Contributing

¿Quieres contribuir? / Want to contribute?

1. Lee: [CONTRIBUTING.md](CONTRIBUTING.md)
2. Revisa los issues abiertos / Check open issues
3. Crea un Pull Request / Create a Pull Request

---

## 📄 Licencia / License

Ver: [LICENSE](LICENSE)

---

## 🙏 Agradecimientos / Acknowledgments

- Built with [Avalonia UI](https://avaloniaui.net/)
- Powered by [Rclone](https://rclone.org/)
- Uses [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)

---

**Última actualización / Last updated**: 2026-02-16  
**Idiomas disponibles / Available languages**: 🇪🇸 Español, 🇬🇧 English
