
# Base de Datos - AuditMed

> [!info] Fase 1 del Proyecto - Infraestructura Portable
> Modelo de datos desplegado automáticamente con Docker y Flyway.

## Despliegue Automático

Este proyecto utiliza **Docker Compose** para levantar un contenedor de SQL Server 2022 y **Flyway** para ejecutar las migraciones de forma automática y versionada.

```bash
cd database
docker compose up -d
```

### ¿Por qué Docker + Flyway?
- **Portabilidad:** Funciona igual en Windows, Mac o Linux.
- **Reproducibilidad:** Cualquier persona tiene exactamente la misma versión de la BD.
- **Versionado:** Los cambios en la BD se rastrean con Git (`V1__`, `V2__`).
- **Cero dependencias locales:** No requiere instalar SQL Server ni herramientas de cliente.

## Diagrama Entidad-Relación

```mermaid
erDiagram
    PACIENTES ||--o{ ATENCIONES : "tiene"
    
    PACIENTES {
        int IdPaciente PK "IDENTITY"
        varchar_150 Nombre "NOT NULL"
        varchar_20 Documento "NOT NULL, UNIQUE"
        varchar_20 EstadoAfiliacion "NOT NULL, CHECK"
    }
    
    ATENCIONES {
        int IdAtencion PK "IDENTITY"
        int IdPaciente FK "NOT NULL, CASCADE"
        datetime FechaAtencion "DEFAULT GETDATE"
        bit Facturado "DEFAULT 0"
        decimal_18_2 Valor "CHECK >= 0"
    }
    
    REGISTROATENCIONES {
        int IdAtencion PK "IDENTITY"
        varchar_20 DocumentoPaciente "NOT NULL"
        varchar_10 CodigoDiagnostico "NULLABLE"
        datetime FechaAtencion "NOT NULL"
        bit RequiereAuditoria "DEFAULT 0"
    }
```

## Relaciones con Otros Componentes

> [!note] Trazabilidad hacia fases posteriores

| Tabla BD | Clase Backend (Fase 2) | Servicio Frontend (Fase 3) |
|----------|------------------------|----------------------------|
| `RegistroAtenciones` | `RegistroAtencion.cs` | `AtencionService` |
| `RegistroAtenciones` | `AtencionRepository` | `atencion-table.component` |

## Migraciones Flyway

| Archivo | Descripción |
|---------|-------------|
| `V1__creacion_base_datos.sql` | Crea la base de datos `AuditMed` si no existe. |
| `V2__creacion_tablas.sql` | Crea las tablas `Paciente`, `Atencion` y `RegistroAtencion` con sus restricciones e índices. |
| `V3__seed_pacientes.sql` | Inserta 8 pacientes de prueba (incluye activos, inactivos y suspendidos). |
| `V4__seed_atenciones.sql` | Inserta 16 atenciones de prueba (algunas facturadas y otras no). |
| `V5__seed_registro_atenciones.sql` | Inserta 9 registros de atención con fechas relativas a hoy (para validar auditoria de >30 días). |
| `03-consulta-facturacion.sql` | *No es migración*. Consulta `SELECT` para validación manual del |

## Volver a
- [[README|Inicio]]