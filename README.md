
# AuditMed

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-17-DD0031?logo=angular)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoftsqlserver)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker)

> Sistema de auditoría médica para gestión y control de atenciones facturables.

## Estado del Proyecto

| Fase | Componente | Estado |
|------|------------|--------|
| 1 | Base de Datos (Docker + Flyway) | Completada |
| 2 | Backend API | Pendiente |
| 3 | Frontend Angular | Pendiente |
| 4 | Code Review | Pendiente |

## Inicio Rápido (Base de Datos)

> [!IMPORTANT]
> **Requisito único:** Tener Docker instalado y ejecutándose.

La base de datos se levanta automáticamente con SQL Server 2022 y las migraciones se ejecutan con Flyway.

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/AuditMed.git
cd AuditMed

# 2. Levantar la infraestructura de BD
cd database
docker compose up -d

# 3. se puede verificar que los contenedores están corriendo
docker compose ps
```


### Datos de Conexión
- **Server:** `localhost,1433`
- **User:** `sa`
- **Password:** `YourStrong!Passw0rd` (ver `database/.env`)
- **Database:** `AuditMed`

## Detener el Entorno

```bash
cd database
docker compose down
```

## Estructura

```
AuditMed/
├── docs/                    # Documentación Obsidian
├── database/                # Infraestructura de BD
│   ├── docker-compose.yml   # Orquestación SQL Server + Flyway
│   ├── Dockerfile.flyway    # Imagen custom para migraciones
│   └── scripts/             # Migraciones y consultas SQL
├── backend/                 # API .NET (pendiente)
└── frontend/                # Angular (pendiente)
```

## Documentación

- [[docs/base-de-datos|Base de Datos]] - Arquitectura y diseño del modelo relacional