-- =============================================================================
-- AUDITMED - Sistema de Auditoría Médica
-- Script 03: Consulta de Facturación por Paciente
-- =============================================================================
-- REQUERIMIENTO (Parte 1 de la prueba):
--   Devolver Documento y Nombre de los pacientes, junto con el total del valor
--   de las atenciones, agrupado por paciente.
--
-- REGLAS DE NEGOCIO (obligatorias):
--   1. Solo incluir pacientes cuyo EstadoAfiliacion sea 'Activo'
--   2. Solo incluir atenciones que no hayan sido facturadas (Facturado = 0)
--   3. El resultado final debe estar ordenado de mayor a menor según el valor total
-- =============================================================================

USE AuditMed;
GO

SELECT 
    p.Documento AS Documento,
    p.Nombre AS Nombre,
    ISNULL(SUM(a.Valor), 0) AS TotalValorAdeudado,
    COUNT(a.IdAtencion) AS CantidadAtencionesPendientes,
    CASE 
        WHEN SUM(a.Valor) >= 600000 THEN 'Alta'
        WHEN SUM(a.Valor) >= 400000 THEN 'Media'
        ELSE 'Baja'
    END AS NivelDeuda
FROM 
    dbo.Pacientes p
INNER JOIN 
    dbo.Atenciones a ON p.IdPaciente = a.IdPaciente
WHERE 
    p.EstadoAfiliacion = 'Activo'
    AND a.Facturado = 0
GROUP BY 
    p.Documento, 
    p.Nombre
ORDER BY 
    TotalValorAdeudado DESC;
GO

-- =============================================================================
-- EXPLICACIÓN DE LA CONSULTA
-- =============================================================================
/*
CLAUSULA          | PROPÓSITO
------------------|----------------------------------------------------------
SELECT            | Selecciona las columnas requeridas: Documento, Nombre
                  | Agrega columna calculada: TotalValorAdeudado (SUM)
                  | Agrega columna auxiliar: CantidadAtencionesPendientes
                  | Agrega columna de clasificación: NivelDeuda

FROM Pacientes p  | Tabla principal de pacientes (alias 'p')

INNER JOIN        | Solo trae pacientes que tienen atenciones
Atenciones a      | Tabla de atenciones (alias 'a')
ON p.IdPaciente = | Condición de unión
a.IdPaciente

WHERE             | Filtros obligatorios:
  EstadoAfiliacion|   REGLA 1: Solo pacientes 'Activo'
  = 'Activo'      |
  AND Facturado   |   REGLA 2: Solo atenciones no facturadas (0)
  = 0             |

GROUP BY          | Agrupa por paciente para poder usar SUM y COUNT
  p.Documento,    |   (todas las columnas no agregadas deben estar aquí)
  p.Nombre        |

ORDER BY          | REGLA 3: Ordena de mayor a menor deuda
  TotalValorAdeudado DESC

FUNCIONES USADAS:
- ISNULL(expresion, valor) : Reemplaza NULL por un valor por defecto
- SUM(columna)             : Suma los valores de cada grupo
- COUNT(columna)           : Cuenta registros en cada grupo
- CASE WHEN                : Evaluación condicional para clasificar
*/

GO

-- =============================================================================
-- CONSULTAS AUXILIARES PARA VERIFICACIÓN
-- =============================================================================


-- Verificación 1: Pacientes excluidos por estado
PRINT '1. Pacientes EXCLUIDOS (no están activos):';
SELECT 
    p.Documento,
    p.Nombre,
    p.EstadoAfiliacion,
    SUM(a.Valor) AS ValorTotal
FROM 
    dbo.Pacientes p
INNER JOIN 
    dbo.Atenciones a ON p.IdPaciente = a.IdPaciente
WHERE 
    p.EstadoAfiliacion != 'Activo'
    AND a.Facturado = 0
GROUP BY 
    p.Documento, p.Nombre, p.EstadoAfiliacion;
    
PRINT '2. Atenciones EXCLUIDAS (ya facturadas):';
SELECT 
    p.Documento,
    p.Nombre,
    a.IdAtencion,
    a.FechaAtencion,
    a.Valor,
    'Ya facturada' AS MotivoExclusion
FROM 
    dbo.Pacientes p
INNER JOIN 
    dbo.Atenciones a ON p.IdPaciente = a.IdPaciente
WHERE 
    p.EstadoAfiliacion = 'Activo'
    AND a.Facturado = 1
ORDER BY 
    p.Documento, a.FechaAtencion;
PRINT '';

-- Verificación 3: Detalle por paciente activo
PRINT '3. Detalle completo de atenciones de pacientes activos:';
SELECT 
    p.Documento,
    p.Nombre,
    a.IdAtencion,
    CONVERT(VARCHAR, a.FechaAtencion, 103) AS Fecha,
    a.Facturado,
    a.Valor,
    CASE 
        WHEN a.Facturado = 1 THEN 'Excluida (facturada)'
        ELSE 'Incluida'
    END AS EstadoConsulta
FROM 
    dbo.Pacientes p
INNER JOIN 
    dbo.Atenciones a ON p.IdPaciente = a.IdPaciente
WHERE 
    p.EstadoAfiliacion = 'Activo'
ORDER BY 
    p.Documento, a.FechaAtencion;

-- Verificación 4: Total general
PRINT '4. Resumen general:';
SELECT 
    COUNT(DISTINCT p.IdPaciente) AS TotalPacientesActivosConDeuda,
    SUM(a.Valor) AS GranTotalDeuda,
    AVG(a.Valor) AS PromedioDeudaPorAtencion
FROM 
    dbo.Pacientes p
INNER JOIN 
    dbo.Atenciones a ON p.IdPaciente = a.IdPaciente
WHERE 
    p.EstadoAfiliacion = 'Activo'
    AND a.Facturado = 0;

GO