using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuditMed.Api.Data.Entities;

[Index("FechaAtencion", Name = "IX_RegistroAtenciones_FechaAtencion")]
public partial class RegistroAtencion
{
    [Key]
    public int IdAtencion { get; set; }

    [StringLength(20)]
    public string DocumentoPaciente { get; set; } = null!;

    [StringLength(10)]
    public string? CodigoDiagnostico { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaAtencion { get; set; }

    public bool RequiereAuditoria { get; set; }
}
