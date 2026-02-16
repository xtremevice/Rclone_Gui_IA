# 🖥️ Comandos para Terminal - Hacer Visible Todo el Proyecto

## Opción 1: Merge Simple (Recomendado)

```bash
# 1. Ir al directorio del proyecto (ajusta la ruta según tu ubicación)
cd ~/Rclone_Gui_IA
# O si lo acabas de clonar:
# cd Rclone_Gui_IA

# 2. Traer últimos cambios
git fetch origin

# 3. Ir a la rama main
git checkout main

# 4. Hacer merge con la rama de desarrollo
git merge origin/copilot/add-rclone-user-interface --allow-unrelated-histories

# 5. Si NO hay conflictos, hacer push:
git push origin main

# 6. ¡LISTO! Ve a https://github.com/xtremevice/Rclone_Gui_IA
```

---

## Si Hay Conflicto en README.md

```bash
# Después del paso 4, si ves conflicto en README.md:

# Usar la versión de la rama de desarrollo:
git checkout origin/copilot/add-rclone-user-interface -- README.md

# Agregar el archivo resuelto:
git add README.md

# Completar el merge:
git commit -m "Merge copilot/add-rclone-user-interface to main"

# Hacer push:
git push origin main
```

---

## Opción 2: Clonar Desde Cero

```bash
# 1. Clonar el repositorio
git clone https://github.com/xtremevice/Rclone_Gui_IA.git

# 2. Entrar al directorio
cd Rclone_Gui_IA

# 3. Ir a main
git checkout main

# 4. Hacer merge
git merge origin/copilot/add-rclone-user-interface --allow-unrelated-histories

# 5. Si hay conflicto en README:
git checkout origin/copilot/add-rclone-user-interface -- README.md
git add README.md
git commit -m "Merge development to main"

# 6. Push
git push origin main
```

---

## Opción 3: Reemplazar Completamente Main (Más Directo)

⚠️ **CUIDADO**: Esto reemplaza totalmente main con la rama de desarrollo

```bash
# 1. Ir al directorio
cd Rclone_Gui_IA

# 2. Traer cambios
git fetch origin

# 3. Ir a la rama de desarrollo
git checkout copilot/add-rclone-user-interface

# 4. Asegurarse de estar actualizado
git pull origin copilot/add-rclone-user-interface

# 5. Eliminar main local
git branch -D main

# 6. Crear nueva main desde aquí
git checkout -b main

# 7. Force push a main
git push origin main --force
```

---

## Verificar que Funcionó

```bash
# Ver archivos en main
git checkout main
ls -la

# Debería mostrar 52 archivos incluyendo:
# - carpeta src/
# - muchos archivos .md
# - run.sh, run.bat
# - RcloneGui.slnx
# etc.

# Contar archivos
git ls-files | wc -l
# Debería mostrar 52
```

---

## Verificar en GitHub

Después de hacer push, ve a:
```
https://github.com/xtremevice/Rclone_Gui_IA
```

Deberías ver:
- ✅ Carpeta `src/`
- ✅ Muchos archivos .md (ARCHITECTURE.md, BISYNC_GUIDE.md, etc.)
- ✅ Scripts (run.sh, run.bat, setup-mac-silicon.sh)
- ✅ 52 archivos en total

---

## Si Tienes Problemas

### Error: "Permission denied"
```bash
# Asegúrate de estar autenticado
git config --global user.name "Tu Nombre"
git config --global user.email "tu@email.com"

# Si usas HTTPS, necesitarás tu token de GitHub
# O cambia a SSH
```

### Error: "Conflictos de merge"
```bash
# Ver qué archivos tienen conflicto
git status

# Para cada archivo con conflicto, puedes:
# Opción A: Usar versión de copilot
git checkout origin/copilot/add-rclone-user-interface -- ARCHIVO

# Opción B: Editar manualmente y resolver
# Luego:
git add ARCHIVO
git commit -m "Resolve merge conflicts"
```

### Ver ramas disponibles
```bash
git branch -a
```

### Ver estado actual
```bash
git status
git log --oneline -5
```

---

## Resumen de Comandos Rápidos

**Si tienes el repo clonado:**
```bash
cd Rclone_Gui_IA
git fetch origin
git checkout main
git merge origin/copilot/add-rclone-user-interface --allow-unrelated-histories
git push origin main
```

**Si no tienes el repo:**
```bash
git clone https://github.com/xtremevice/Rclone_Gui_IA.git
cd Rclone_Gui_IA
git checkout main
git merge origin/copilot/add-rclone-user-interface --allow-unrelated-histories
git push origin main
```

---

## Notas Importantes

1. **Autenticación**: Necesitas tener permisos de push en el repositorio
2. **Conflictos**: Si hay conflicto, usa la versión de `copilot/add-rclone-user-interface`
3. **Verificación**: Después del push, verifica en GitHub que todo esté visible
4. **Backup**: Si tienes dudas, puedes hacer backup de main antes:
   ```bash
   git branch backup-main main
   ```

---

## Ayuda Adicional

Si necesitas más ayuda:
- Lee `COMO_HACER_VISIBLE_TODO.md` para opciones alternativas
- Lee `RESUMEN_SOLUCION_FINAL.md` para contexto completo
- O crea un issue en GitHub con el error específico que ves
