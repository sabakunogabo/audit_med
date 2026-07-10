# AuditMed

  

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)

![Angular](https://img.shields.io/badge/Angular-17-DD0031?logo=angular)

![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoftsqlserver)

![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker)

![Architecture](https://img.shields.io/badge/Architecture-Clean_SOLID-00B4D8)

  

> Sistema de auditoría médica para gestión y control de atenciones facturables. Construido con arquitectura limpia, patrón repository, diseño desacoplado en Angular y despliegue de BD portátil con Docker.

  

## Estado del Proyecto

  

| Fase | Componente | Estado |

|------|------------|--------|

| 1 | Base de Datos (Docker + Flyway) | Completada |

| 2 | Backend API (.NET 8 + SOLID) | Completada |

| 3 | Frontend Angular (Material ) | Completada |

| 4 | Code Review y Refactorización | Completada |

  

---

  

## Inicio Rápido (Levantar el sistema)

  

> [!IMPORTANT]

> **Requisitos previos:** Tener [Docker](https://www.docker.com/) y el [SDK .NET 8](https://dotnet.microsoft.com/download) instalados.

  

Abre una terminal y ejecuta los siguientes comandos:

  

```bash

# 1. Clonar el repositorio

git clone https://github.com/tu-usuario/AuditMed.git

cd AuditMed

  

# 2. Levantar Base de Datos (SQL Server en Docker + Migraciones Flyway)

cd database

docker compose up -d

cd ..

  

# 3. Levantar Backend API (.NET)

cd backend/AuditMed.Api

dotnet run

# (Dejar corriendo en una terminal)

  

# 4. Levantar Frontend (Angular) - En una NUEVA terminal

cd frontend

npm install

ng serve

```

  

**Abrir en el navegador:** `http://localhost:4200`

  

---

  

## Estructura del Proyecto (Monorepo)

  

```

AuditMed/

├── docs/ # Documentación técnica (Diseñada para Obsidian)

│ ├── base-de-datos.md # Fase 1: Modelo relacional y Docker

│ ├── backend-api.md # Fase 2: Arquitectura y SOLID

│ ├── frontend-angular.md # Fase 3: Smart/Dumb components

│ └── code-review.md # Fase 4: Análisis y patrón Strategy

│

├── database/ # Infraestructura como Código (Fase 1)

│ ├── docker-compose.yml # Orquesta SQL Server y Flyway

│ ├── Dockerfile.flyway # Imagen para migraciones

│ └── scripts/ # Migraciones versionadas (V1__, V2__)

│

├── backend/AuditMed.Api/ # API RESTful (Fase 2)

│ ├── Controllers/ # Endpoints (CRUD + Auditoría)

│ ├── Interfaces/ # Contratos (Inversión de dependencias)

│ ├── Repositories/ # Acceso a Datos (Entity Framework Core)

│ ├── Services/ # Lógica de Negocio (LINQ puro en memoria)

│ └── Models/ # Entidades y DTOs

│

└── frontend/ # SPA Interfaz de usuario (Fase 3)

└── src/app/

├── components/ # Componentes "Tontos" (Solo UI)

├── services/ # Capa HTTP (HttpClient)

└── app.component.ts # Componente "Inteligente" (Orquestador)

```

  

---

  

## Puntos Técnicos Evaluados (Resumen de Implementación)

  

### Parte 1: Lógica SQL

- Consulta pura con `INNER JOIN`, filtrado por estado activo y facturado (`= 0`).

- Agrupación (`GROUP BY`) y ordenamiento descendente por valor total adeudado.

- Desplegado de forma portable usando **Docker** y versionado con **Flyway**.

  

### Parte 2: Backend y Reglas de Negocio

- **Patrón Repository:** Aislamiento total de `DbContext`. El servicio no conoce EF Core.

- **Lógica de Auditoría en C#:** Se obtienen los datos y se procesan en memoria usando LINQ to Objects:

1. Se descartan diagnósticos nulos o vacíos (`!string.IsNullOrWhiteSpace`).

2. Se marca `RequiereAuditoria = true` si la fecha es menor a `DateTime.Now.AddDays(-30)`.

3. Se ordena por fecha descendente.

  

### Parte 3: Frontend y Reglas Visuales

- **Smart/Dumb Components:** El estado (`atenciones`, `cargando`) vive en `app.component`. La tabla y formulario solo reciben `@Input` y emiten `@Output`.

- **Regla Visual Obligatoria:** Si `requiereAuditoria` es `true`, se inyecta la clase CSS `.fila-auditoria`, pintando toda la fila de rojo claro (`#ffebee`) para alertar al auditor.

- **Reactive Forms:** Validación robusta del campo Documento (Obligatorio, min 5 caracteres).

  

### Parte 4: Code Review (Análisis de Código Heredado)

- **Problema Identificado:** `NullReferenceException` en producción por falta de validaciones y uso de "Magic Strings" (`"Inactivo"`).

- **Solución Propuesta:**

- Uso de `ArgumentNullException.ThrowIfNull`.

- Reemplazo de strings por `Enums`.

- Implementación del **Patrón Strategy** para inyectar descuentos dinámicamente, cumpliendo el Principio Open/Closed (SOLID).

  

---

  

## Documentación Técnica (Obsidian)

  

La carpeta `docs/` contiene la documentación detallada de la arquitectura.

Si abres la carpeta del repositorio como un **Vault en Obsidian**, podrás ver los grafos de dependencias, la traza entre BD, Backend y Frontend, y navegar mediante los siguientes enlaces:

  

| Fase | Documento | Descripción |

|------|-----------|-------------|

| 1 | [[base-de-datos\|Base de Datos]] | Diagrama Entidad-Relación, justificación de Docker/Flyway y trazabilidad hacia el Backend. |

| 2 | [[backend-api\|Backend API]] | Flujo de datos, implementación del Patrón Repository y cumplimiento de principios SOLID. |

| 3 | [[frontend-angular\|Frontend Angular]] | Arquitectura Smart/Dumb Components, reglas visuales CSS y trazabilidad de endpoints. |

| 4 | [[code-review\|Code Review]] | Análisis de causas raíz del código heredado y refactorización con Patrón Strategy. |

  

---

  

## Detener el Entorno

  

```bash

# Detener contenedores de BD

cd database && docker compose down

```

  

---

*Prueba técnica desarrollada aplicando estándares enterprise.*

```