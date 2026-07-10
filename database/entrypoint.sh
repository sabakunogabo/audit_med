#!/bin/bash
# =============================================================================
# Script de entrada (Entrypoint) para el contenedor de SQL Server
# =============================================================================
# Este script:
# 1. Inicia SQL Server en segundo plano
# 2. Espera a que esté listo para recibir conexiones
# 3. Ejecuta los scripts SQL en orden numérico
# 4. Mantiene el contenedor vivo
# =============================================================================

set -e

# Contraseña por defecto si no se pasa por variable de entorno
SA_PASSWORD="${SA_PASSWORD:-AuditMed_2024!}"

echo "========================================"
echo "  AUDITMED - Inicializando Base de Datos"
echo "========================================"

# 1. Iniciar SQL Server en segundo plano
echo "[1/4] Iniciando SQL Server..."
/opt/mssql/bin/sqlservr &

# 2. Esperar a que SQL Server esté listo
echo "[2/4] Esperando a que SQL Server esté listo..."
MAX_RETRIES=30
RETRY_COUNT=0

until /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1; do
    RETRY_COUNT=$((RETRY_COUNT + 1))
    if [ $RETRY_COUNT -eq $MAX_RETRIES ]; then
        echo "❌ Error: SQL Server no pudo iniciar después de $MAX_RETRIES intentos"
        exit 1
    fi
    echo "   Esperando... (intento $RETRY_COUNT de $MAX_RETRIES)"
    sleep 2
done

echo "✅ SQL Server está listo"

# 3. Ejecutar scripts SQL en orden
echo "[3/4] Ejecutando scripts de inicialización..."

SCRIPTS_DIR="/scripts"

# Obtener archivos .sql y ordenarlos numéricamente (01, 02, 03...)
SCRIPTS=$(find "$SCRIPTS_DIR" -maxdepth 1 -name "*.sql" -type f | sort -V)

if [ -z "$SCRIPTS" ]; then
    echo "No se encontraron scripts SQL en $SCRIPTS_DIR"
else
    for SCRIPT_PATH in $SCRIPTS; do
        SCRIPT_NAME=$(basename "$SCRIPT_PATH")

        echo "▶ Ejecutando: $SCRIPT_NAME"

        if /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i "$SCRIPT_PATH"; then
            echo "$SCRIPT_NAME completado exitosamente"
        else
            echo "Error al ejecutar $SCRIPT_NAME"
        fi
    done
fi
echo "Datos de conexión:"
echo "  Servidor: localhost,1433"
echo "  Usuario:  sa"
echo "  Base de datos: AuditMed"

echo "Presione Ctrl+C para detener."
wait
