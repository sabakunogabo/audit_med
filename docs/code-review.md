# Code Review - Analisis y Refactorizacion

Al revisar este metodo, entiendo perfectamente por que esta generando caidas aleatorias en produccion. A continuacion explico los problemas que encontré y cómo los solucionaría manteniendo las cosas simples.

## Codigo original con errores

```csharp
public decimal CalcularTotalFactura(Paciente paciente, List<Atencion> atencionesDelMes)
{
    decimal total = 0;
    if (paciente.Estado == "Inactivo")
        return total;
    foreach (var atencion in atencionesDelMes)
    {
        if (atencion.Valor > 0)
            total = total + atencion.Valor;
    }
    if (paciente.Edad > 60)
        total = total * 0.9m;
    return total;
}
```

## Analisis de problemas

**1. La causa de la caida en produccion (NullReferenceException)**
El error principal está en que el método asume que siempre le van a llegar datos válidos. Si por alguna razón la base de datos devuelve un paciente nulo, o la lista de atenciones es nula, el codigo explota en la primera linea al intentar leer `paciente.Estado` o al entrar al `foreach`.

**2. Textos "quemados" en el codigo (Magic Strings)**
Tener el string `"Inactivo"` escrito asi a mano es peligroso. Si en otra parte del sistema alguien lo escribe con minuscula ("inactivo") o con un espacio, la validacion falla y el sistema cobra a un paciente que no deberia cobrarse.

**3. Numeros "magicos" y logica oculta**
Tener el `60` y el `0.9m` asi sin explicacion hace que el codigo sea dificil de leer. Ademas, si maana el negocio cambia el descuento al 15% o la edad a 65 años, tenemos que venir a buscar dentro de la logica del metodo para cambiarlo, lo cual es propenso a errores.

**4. Detalle de sintaxis**
El uso de `total = total + atencion.Valor` funciona, pero es mas limpio y estandar usar el operador `+=` para sumar.

---

## Codigo refactorizado

Para resolver esto, mi enfoque como junior seria agregar validaciones basicas defensivas al inicio, crear un enum para los estados del paciente para evitar errores de tipeo, y extraer esos numeros magicos a constantes para que sean faciles de modificar en el futuro.

```csharp
/// <summary>
/// Estados validos para un paciente.
/// </summary>
public enum EstadoPaciente
{
    Activo,
    Inactivo,
    Suspendido
}

public class CalculadoraFactura
{
    // Constantes para que si el negocio cambia las reglas, solo se modifique aqui.
    private const int EdadDescuentoAdultoMayor = 60;
    private const decimal PorcentajeDescuentoAdultoMayor = 0.9m;

    /// <summary>
    /// Calcula el total de la factura aplicando descuentos segun reglas de negocio.
    /// </summary>
    public decimal CalcularTotalFactura(Paciente paciente, List<Atencion> atencionesDelMes)
    {
        // 1. Validacion defensiva: Evitamos el NullReferenceException
        if (paciente == null || atencionesDelMes == null)
        {
            return 0m;
        }

        // 2. Regla de negocio usando el enum en vez de un string suelto
        if (paciente.Estado == EstadoPaciente.Inactivo)
        {
            return 0m;
        }

        decimal total = 0;

        // 3. Calculo del subtotal usando el operador +=
        foreach (var atencion in atencionesDelMes)
        {
            if (atencion.Valor > 0)
            {
                total += atencion.Valor;
            }
        }

        // 4. Descuento usando las constantes declaradas arriba
        if (paciente.Edad > EdadDescuentoAdultoMayor)
        {
            total *= PorcentajeDescuentoAdultoMayor;
        }

        return total;
    }
}
```

## Resumen de mis cambios

- **Agregue validaciones de nulos:** Un simple `if` al inicio soluciona la caida en produccion. Si no hay datos, devolvemos cero en lugar de dejar que la aplicacion explote.
- **Reemplace el string por un Enum:** Al usar `EstadoPaciente.Inactivo`, el compilador nos avisara si escribimos algo mal, eliminando errores silenciosos.
- **Extraiga numeros a constantes:** Puse el 60 y el 0.9m en variables `const` arriba del todo. Ahora el codigo se lee solo y si cambia el descuento, se modifica en un solo lugar seguro.
- **Mejore la sintaxis:** Cambie el `total = total + ...` por `total += ...` que es la convencion estandar en C#.

## Volver a
- [[README|Inicio]]