# Frontend - Angular 21 + Material

  

> [!info] Fase 3 del Proyecto

> Interfaz de usuario moderna aplicando Reactive Forms, Smart/Dumb Components y Angular Material.

  

## Arquitectura de Componentes

  

```mermaid

graph TD

APP[app.component<br/>SMART - Orquestador] --> TABLE[atencion-table<br/>DUMB - Presentación]

APP --> FORM[atencion-form<br/>DUMB - Presentación]

APP --> DIALOG[confirm-dialog<br/>Modal]

TABLE -->|@Output editar| APP

TABLE -->|@Output eliminar| APP

FORM -->|@Output guardar| APP

APP -->|@Input atencionSeleccionada| FORM

APP -->|@Input atenciones| TABLE

```

  

## Buenas Prácticas Aplicadas

  

| Práctica | Implementación |

|----------|----------------|

| **Standalone Components** | No se usan `NgModules`, imports directos en cada componente. |

| **Smart/Dumb Components** | `app.component` maneja el estado. La tabla y formulario solo reciben Inputs y emiten Outputs. |

| **Reactive Forms** | Validación estricta y reactiva del campo Documento. |

| **Control Flow (`@if`)** | Sintaxis moderna de Angular 17 en lugar de `*ngIf`. |

| **Manejo de Estado Centralizado** | El array `atenciones` vive solo en `app.component` y fluye hacia abajo. |

  

## Regla Visual Implementada

  

> [!important] Requerimiento de la Prueba

> Si `RequiereAuditoria` es `true`, la fila se resalta.

  

- Se inyecta la clase CSS `.fila-auditoria` dinámicamente usando `[ngClass]="getRowClass(row)"` en la tabla.

- El SCSS aplica `background-color: #ffebee` (rojo claro de Material Design) a toda la fila.

- Se usa un `mat-chip` de color `warn` para reforzar la señal visual.

  

## Trazabilidad Endpoints

  

| Acción UI | Método HTTP | Endpoint Backend | Servicio Angular |

|-----------|-------------|------------------|------------------|

| Cargar tabla | GET | `/api/auditoria/atenciones` | `getAuditoria()` |

| Crear registro | POST | `/api/atenciones` | `crear(dto)` |

| Editar registro | PUT | `/api/atenciones/{id}` | `actualizar(id, dto)` |

| Eliminar registro | DELETE | `/api/atenciones/{id}` | `eliminar(id)` |

  

## Volver a

- [[README|Inicio]]

- [[backend-api|Fase 2: Backend API]]