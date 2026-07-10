
-- =============================================================================
-- AUDITMED - Migración V5: Datos de Prueba (Seed registros de atencion)
-- =============================================================================

USE AuditMed;
GO

-- REGISTROS DE ATENCIÓN (PARA LA API)
-- Usando DATEADD para que las fechas sean relativas a HOY (siempre válido para prueba de >30 días)
INSERT INTO dbo.RegistroAtencion (DocumentoPaciente, CodigoDiagnostico, FechaAtencion, RequiereAuditoria) VALUES
    ('12345678', 'J01.1', DATEADD(DAY, -45, GETDATE()), 0),
    ('23456789', 'K21.0', DATEADD(DAY, -35, GETDATE()), 0),
    ('45678901', 'M54.5', DATEADD(DAY, -60, GETDATE()), 0),
    ('67890123', 'I10', DATEADD(DAY, -50, GETDATE()), 0),
    ('67890123', 'J06.9', DATEADD(DAY, -15, GETDATE()), 0),
    ('78901234', 'R50.9', DATEADD(DAY, -10, GETDATE()), 0),
    ('12345678', 'A09', DATEADD(DAY, -5, GETDATE()), 0),
    ('45678901', 'E11.9', DATEADD(DAY, -25, GETDATE()), 0),
    ('78901234', 'N39.0', DATEADD(DAY, -2, GETDATE()), 0);
GO