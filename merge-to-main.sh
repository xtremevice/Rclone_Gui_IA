#!/bin/bash
# Script para hacer merge de copilot/add-rclone-user-interface a main
# Uso: bash merge-to-main.sh

echo "🚀 Iniciando merge a main..."
echo ""

# Verificar que estamos en el directorio correcto
if [ ! -d ".git" ]; then
    echo "❌ Error: No estás en el directorio del repositorio git"
    echo "   Ejecuta: cd Rclone_Gui_IA"
    exit 1
fi

# Traer últimos cambios
echo "📥 Trayendo últimos cambios..."
git fetch origin

# Ir a main
echo "📍 Cambiando a rama main..."
git checkout main

# Hacer merge
echo "🔀 Haciendo merge con copilot/add-rclone-user-interface..."
git merge origin/copilot/add-rclone-user-interface --allow-unrelated-histories -m "Merge development to main - make all files visible"

# Verificar si hay conflictos
if [ $? -ne 0 ]; then
    echo ""
    echo "⚠️  Hay conflictos de merge. Resolviéndolos automáticamente..."
    
    # Si hay conflicto en README, usar versión de copilot
    if git status | grep -q "README.md"; then
        echo "📝 Resolviendo conflicto en README.md..."
        git checkout origin/copilot/add-rclone-user-interface -- README.md
        git add README.md
        git commit -m "Merge development to main - resolve conflicts"
    fi
fi

# Verificar estado
echo ""
echo "📊 Estado del merge:"
git status

# Contar archivos
FILE_COUNT=$(git ls-files | wc -l)
echo ""
echo "📁 Archivos en main: $FILE_COUNT"

if [ $FILE_COUNT -gt 40 ]; then
    echo "✅ ¡Merge completado exitosamente!"
    echo ""
    echo "📤 Para hacer push a GitHub, ejecuta:"
    echo "   git push origin main"
    echo ""
    echo "Luego ve a: https://github.com/xtremevice/Rclone_Gui_IA"
else
    echo "⚠️  Algo salió mal. Deberías tener más de 40 archivos."
    echo "   Ejecuta: git status"
fi
