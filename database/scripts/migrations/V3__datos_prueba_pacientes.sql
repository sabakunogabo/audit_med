-- =============================================================================
-- AUDITMED - Migración V3: Datos de Prueba (Seed Pacientes)
-- =============================================================================

USE AuditMed;
GO

-- Limpieza e identidad (solo desarrollo)
ALTER TABLE dbo.Atencion NOCHECK CONSTRAINT ALL;
ALTER TABLE dbo.RegistroAtencion NOCHECK CONSTRAINT ALL;

DELETE FROM dbo.RegistroAtencion;
DELETE FROM dbo.Atencion;
DELETE FROM dbo.Paciente;

DBCC CHECKIDENT ('dbo.Paciente', RESEED, 1);
DBCC CHECKIDENT ('dbo.Atencion', RESEED, 1);
DBCC CHECKIDENT ('dbo.RegistroAtencion', RESEED, 1);

ALTER TABLE dbo.Atencion CHECK CONSTRAINT ALL;
ALTER TABLE dbo.RegistroAtencion CHECK CONSTRAINT ALL;
GO


INSERT INTO dbo.Paciente
(
    Nombre,
    Apellido,
    Documento,
    EstadoAfiliacion
)
VALUES
    ('Juan', 'Pérez García', '12345678', 'Activo'),
    ('María', 'López Martínez', '23456789', 'Activo'),
    ('Carlos', 'Rodríguez Sánchez', '34567890', 'Inactivo'),
    ('Ana', 'Fernández Torres', '45678901', 'Activo'),
    ('Pedro', 'Gómez Ruiz', '56789012', 'Suspendido'),
    ('Laura', 'Martínez Díaz', '67890123', 'Activo'),
    ('Diego', 'Hernández Vega', '78901234', 'Activo'),
    ('Sofía', 'Morales Castillo', '89012345', 'Inactivo');
GO