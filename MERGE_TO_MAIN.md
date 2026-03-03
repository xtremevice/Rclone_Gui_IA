# 🔀 Comandos para Pasar Todo al Main

## Comando Rápido (Todo-en-Uno)

```bash
git checkout copilot/add-documentation-for-execution && \
git fetch origin main && \
git checkout main && \
git pull origin main && \
git merge copilot/add-documentation-for-execution && \
git push origin main
```

## Paso a Paso

### 1. Preparar
```bash
git checkout copilot/add-documentation-for-execution
git fetch origin main
```

### 2. Ir a Main
```bash
git checkout main
git pull origin main
```

### 3. Hacer el Merge
```bash
git merge copilot/add-documentation-for-execution
```

### 4. Subir los Cambios
```bash
git push origin main
```

## Si Hay Conflictos

```bash
# Ver archivos con conflictos
git status

# Después de resolver manualmente:
git add .
git commit -m "Merge branch 'copilot/add-documentation-for-execution' into main"
git push origin main
```

## Limpiar Después

```bash
# Eliminar rama local
git branch -d copilot/add-documentation-for-execution

# Eliminar rama remota
git push origin --delete copilot/add-documentation-for-execution
```

## Verificar

```bash
git checkout main
git log --oneline -5
git status
```

## Qué se va a Pasar a Main

- ✅ RcloneGui.slnx con XML declaration (fix MSB4068)
- ✅ Scripts: fix-msb4068.sh y fix-msb4068.bat
- ✅ COMANDO_RAPIDO.md
- ✅ FIX_MSB4068.md
- ✅ ACTUALIZAR_Y_EJECUTAR.md (actualizado)
- ✅ UPDATE_AND_RUN.md (actualizado)
- ✅ README.md (actualizado)
- ✅ run.sh y run.bat (actualizados)

---

**Última actualización:** 2026-03-03
