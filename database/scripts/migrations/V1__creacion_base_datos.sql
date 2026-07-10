
-- =============================================================================
-- AUDITMED - Migración V1: Creación de base de datos
-- =============================================================================

-- CREAR BASE DE DATOS
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AuditMed')
BEGIN
    CREATE DATABASE AuditMed;
END
GO
