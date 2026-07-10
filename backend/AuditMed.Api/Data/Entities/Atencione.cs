using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuditMed.Api.Data.Entities;

[Index("Facturado", Name = "IX_Atenciones_Facturado")]
[Index("IdPaciente", Name = "IX_Atenciones_IdPaciente")]
public partial class Atencione
{
    [Key]
    public int IdAtencion { get; set; }

    public int IdPaciente { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaAtencion { get; set; }

    public bool Facturado { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Valor { get; set; }

    [ForeignKey("IdPaciente")]
    [InverseProperty("Atenciones")]
    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
