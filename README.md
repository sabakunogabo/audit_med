
# 🏥 AuditMed

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-17-DD0031?logo=angular)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoftsqlserver)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker)
![Architecture](https://img.shields.io/badge/Architecture-Clean_SOLID-00B4D8)

> Sistema de auditoría médica para gestión y control de atenciones facturables. Construido con arquitectura limpia, patrón repository y despliegue portátil.

## 📋 Estado del Proyecto

| Fase | Componente | Estado |
|------|------------|--------|
| 1 | Base de Datos (Docker + Flyway) | Completada |
| 2 | Backend API (.NET 8 + SOLID) | Completada |
| 3 | Frontend Angular | Pendiente |
| 4 | Code Review | Pendiente |

## Inicio Rápido

> [!IMPORTANT]
> **Requisitos:** Tener Docker y el SDK de .NET 8 instalados.

### 1. Levantar Base de Datos (Docker)
La base de datos se levanta en un contenedor aislado con SQL Server 2022 y las migraciones se ejecutan automáticamente con Flyway.

```bash
# Clonar repositorio
git clone https://github.com/tu-usuario/AuditMed.git
cd AuditMed

# Levantar infraestructura de BD
cd database
docker compose up -d
cd ..
```

### 2. Levantar Backend API (.NET)
Una vez el contenedor de SQL Server esté corriendo, iniciamos la API.

```bash
cd backend/AuditMed.Api
dotnet restore
dotnet run
```
La API estará disponible en `https://localhost:5001` (o el puerto asignado).
Puedes probar la documentación interactiva en: `https://localhost:5001/swagger`

## Detener el Entorno

```bash
# Detener Backend: Ctrl + C en la terminal donde corrió dotnet

# Detener Base de Datos
cd database
docker compose down
```

## Estructura del Proyecto

```
AuditMed/
├── docs/                              # Documentación técnica (Obsidian)
│   ├── base-de-datos.md
│   └── backend-api.md
│
├── database/                          # Infraestructura de BD (Fase 1)
│   ├── docker-compose.yml
│   ├── Dockerfile.flyway
│   └── scripts/
│       ├── V1__creacion_tablas.sql
│       ├── V2__datos_prueba.sql
│       └── 03-consulta-facturacion.sql
│
├── backend/                           # API .NET 8 (Fase 2)
│   └── AuditMed.Api/
│       ├── Controllers/               # Endpoints REST
│       ├── Data/                      # Configuración EF Core
│       ├── Interfaces/                # Contratos (DIP)
│       ├── Models/                    # Entidades y DTOs
│       ├── Repositories/              # Acceso a datos (Repository Pattern)
│       └── Services/                  # Lógica de negocio (LINQ puro)
│
└── frontend/                          # Interfaz de usuario (Fase 3 - Pendiente)
```

## Endpoints de la API (Fase 2)

### CRUD Base
| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/atenciones/{id}` | Obtener atención por ID |
| `POST` | `/api/atenciones` | Crear nueva atención |
| `PUT` | `/api/atenciones/{id}` | Actualizar atención |
| `DELETE` | `/api/atenciones/{id}` | Eliminar atención |

### Lógica de Negocio (Auditoría)
| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/auditoria/atenciones` | Obtener atenciones evaluadas por reglas de auditoría |

> **Reglas aplicadas en `/api/auditoria/atenciones`:**
> 1. Filtra diagnósticos nulos o vacíos.
> 2. Marca `RequiereAuditoria = true` si la fecha es > 30 días.
> 3. Ordena por fecha descendente.

## Arquitectura Backend

El backend fue diseñado aplicando **Principios SOLID** y el **Patrón Repository** para garantizar la escalabilidad y testeabilidad del código:

- **Controllers:** Exponen HTTP y delegan la lógica.
- **Services:** Contienen reglas de negocio puras (no conocen Entity Framework). Usan LINQ en memoria.
- **Repositories:** Abstraen el acceso a SQL Server usando EF Core.
- **Interfaces:** Permiten inyección de dependencias y facilidad para crear Mocks en tests.

## 📖 Documentación

Para detalles técnicos profundos, abrir la carpeta `docs/` :
- [[docs/base-de-datos|Base de Datos]] - Docker, Flyway y Modelo Relacional
- [[docs/backend-api|Backend API]] - Arquitectura, SOLID y Flujo de Datos

---

*Proyecto desarrollado como prueba técnica.*