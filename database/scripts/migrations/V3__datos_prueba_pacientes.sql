-- =============================================================================
-- AUDITMED - Migración V3: Datos de Prueba (Seed Pacientes)
-- =============================================================================

USE AuditMed;
GO

-- Limpieza e identidad (solo desarrollo)
ALTER TABLE dbo.Atenciones NOCHECK CONSTRAINT ALL;
ALTER TABLE dbo.RegistroAtenciones NOCHECK CONSTRAINT ALL;

DELETE FROM dbo.RegistroAtenciones;
DELETE FROM dbo.Atenciones;
DELETE FROM dbo.Pacientes;

DBCC CHECKIDENT ('dbo.Pacientes', RESEED, 1);
DBCC CHECKIDENT ('dbo.Atenciones', RESEED, 1);
DBCC CHECKIDENT ('dbo.RegistroAtenciones', RESEED, 1);

ALTER TABLE dbo.Atenciones CHECK CONSTRAINT ALL;
ALTER TABLE dbo.RegistroAtenciones CHECK CONSTRAINT ALL;
GO


INSERT INTO dbo.Pacientes
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