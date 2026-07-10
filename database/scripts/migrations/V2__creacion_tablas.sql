
-- =============================================================================
-- AUDITMED - Migración V2: Creación de Tablas
-- =============================================================================

USE AuditMed;
GO

-- TABLA: PACIENTES
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Pacientes' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Pacientes
    (
        IdPaciente       INT IDENTITY(1,1)   NOT NULL,
        Nombre           NVARCHAR(150)       NOT NULL,
        Apellido         NVARCHAR(150)       NOT NULL,
        Documento        NVARCHAR(20)        NOT NULL,
        EstadoAfiliacion NVARCHAR(20)        NOT NULL,
        
        CONSTRAINT PK_Pacientes PRIMARY KEY CLUSTERED (IdPaciente),
        CONSTRAINT UQ_Pacientes_Documento UNIQUE NONCLUSTERED (Documento),
        CONSTRAINT CK_Pacientes_EstadoAfiliacion 
            CHECK (EstadoAfiliacion IN ('Activo', 'Inactivo', 'Suspendido'))
    );
END
GO

-- TABLA: ATENCIONES
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Atenciones' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Atenciones
    (
        IdAtencion    INT IDENTITY(1,1)   NOT NULL,
        IdPaciente    INT                 NOT NULL,
        FechaAtencion DATETIME            NOT NULL CONSTRAINT DF_Atenciones_Fecha DEFAULT GETDATE(),
        Facturado     BIT                 NOT NULL CONSTRAINT DF_Atenciones_Facturado DEFAULT 0,
        Valor         DECIMAL(18,2)       NOT NULL CONSTRAINT DF_Atenciones_Valor DEFAULT 0,
        
        CONSTRAINT PK_Atenciones PRIMARY KEY CLUSTERED (IdAtencion),
        CONSTRAINT FK_Atenciones_Pacientes 
            FOREIGN KEY (IdPaciente) REFERENCES dbo.Pacientes(IdPaciente) ON DELETE CASCADE,
        CONSTRAINT CK_Atenciones_Valor CHECK (Valor >= 0)
    );
END
GO

-- TABLA: REGISTROATENCIONES
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RegistroAtenciones' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.RegistroAtenciones
    (
        IdAtencion        INT IDENTITY(1,1)   NOT NULL,
        DocumentoPaciente NVARCHAR(20)        NOT NULL,
        CodigoDiagnostico NVARCHAR(10)        NULL,
        FechaAtencion     DATETIME            NOT NULL CONSTRAINT DF_RegistroAtenciones_Fecha DEFAULT GETDATE(),
        RequiereAuditoria BIT                 NOT NULL CONSTRAINT DF_RegistroAtenciones_Auditoria DEFAULT 0,
        
        CONSTRAINT PK_RegistroAtenciones PRIMARY KEY CLUSTERED (IdAtencion),
        CONSTRAINT CK_RegistroAtenciones_Documento CHECK (LEN(DocumentoPaciente) > 0)
    );
END
GO

-- ÍNDICES
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pacientes_EstadoAfiliacion' AND object_id = OBJECT_ID('dbo.Pacientes'))
    CREATE NONCLUSTERED INDEX IX_Pacientes_EstadoAfiliacion ON dbo.Pacientes(EstadoAfiliacion);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Atenciones_IdPaciente' AND object_id = OBJECT_ID('dbo.Atenciones'))
    CREATE NONCLUSTERED INDEX IX_Atenciones_IdPaciente ON dbo.Atenciones(IdPaciente);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Atenciones_Facturado' AND object_id = OBJECT_ID('dbo.Atenciones'))
    CREATE NONCLUSTERED INDEX IX_Atenciones_Facturado ON dbo.Atenciones(Facturado) INCLUDE (IdPaciente, Valor);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RegistroAtenciones_FechaAtencion' AND object_id = OBJECT_ID('dbo.RegistroAtenciones'))
    CREATE NONCLUSTERED INDEX IX_RegistroAtenciones_FechaAtencion ON dbo.RegistroAtenciones(FechaAtencion);
GO