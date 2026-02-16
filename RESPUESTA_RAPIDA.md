# ¿Cómo descargar y probar en Mac Silicon? - Respuesta Rápida

## 🎯 Respuesta Directa

Para descargar y probar el proyecto Rclone GUI en tu Mac con Apple Silicon (M1/M2/M3), sigue estos pasos:

## 📥 Método Rápido (Recomendado)

### 1. Descarga el Proyecto

Abre la Terminal y ejecuta:

```bash
# Navega a donde quieras guardar el proyecto (ejemplo: Documentos)
cd ~/Documents

# Descarga el proyecto
git clone https://github.com/xtremevice/Rclone_Gui_IA.git

# Entra al directorio
cd Rclone_Gui_IA
```

### 2. Instala Todo Automáticamente

Ejecuta el script de instalación automática:

```bash
./setup-mac-silicon.sh
```

Este script instalará automáticamente:
- ✅ Homebrew (si no lo tienes)
- ✅ .NET 8.0 SDK
- ✅ Rclone

### 3. Ejecuta la Aplicación

```bash
./run.sh
```

¡Listo! La aplicación se abrirá.

---

## 📖 ¿Necesitas Más Información?

Consulta estos documentos:

- **[MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md)** - Guía completa en español
- **[README.md](README.md)** - Documentación general del proyecto
- **[BISYNC_GUIDE.md](BISYNC_GUIDE.md)** - Guía de sincronización bidireccional

---

## 🔍 Resumen Visual

```
┌─────────────────────────────────────────────────────────┐
│  Paso 1: Descargar                                      │
├─────────────────────────────────────────────────────────┤
│  $ git clone https://github.com/xtremevice/...         │
│  $ cd Rclone_Gui_IA                                     │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│  Paso 2: Instalar (automático)                          │
├─────────────────────────────────────────────────────────┤
│  $ ./setup-mac-silicon.sh                               │
│                                                          │
│  Instala:                                                │
│  ✓ Homebrew                                             │
│  ✓ .NET 8.0                                             │
│  ✓ Rclone                                               │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│  Paso 3: Ejecutar                                       │
├─────────────────────────────────────────────────────────┤
│  $ ./run.sh                                             │
│                                                          │
│  ¡La aplicación se abre! ✅                             │
└─────────────────────────────────────────────────────────┘
```

---

## ⏱️ Tiempo Estimado

- **Con script automático**: 5-10 minutos
- **Manual (siguiendo MAC_SILICON_SETUP.md)**: 15-20 minutos

---

## 💡 Características Especiales para Mac Silicon

- ✅ **Optimizado para M1/M2/M3**: Código nativo ARM64
- ✅ **Paths correctos**: Usa `/opt/homebrew/bin/rclone` automáticamente
- ✅ **Bisync optimizado**: La función de sincronización genera comandos específicos para Mac Silicon
- ✅ **Instalación simple**: Un script hace todo

---

## 🆘 ¿Problemas?

Si algo no funciona, revisa:

1. **MAC_SILICON_SETUP.md** - Sección de solución de problemas
2. Verifica que tu Mac sea Apple Silicon:
   ```bash
   uname -m
   # Debe mostrar: arm64
   ```
3. Asegúrate de tener macOS actualizado

---

## 🎉 ¡Listo para Usar!

Después de la instalación, la aplicación te permite:

- 📁 **Gestionar remotos**: Agregar cuentas de OneDrive, Google Drive, Dropbox, etc.
- ⇄ **Bisync**: Sincronización bidireccional entre servicios
- 🔐 **Seguridad**: Autenticación OAuth2 y almacenamiento seguro de credenciales

---

## 📞 Soporte

- Documentación completa: [MAC_SILICON_SETUP.md](MAC_SILICON_SETUP.md)
- Issues: https://github.com/xtremevice/Rclone_Gui_IA/issues
- Rclone docs: https://rclone.org/docs/
