using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuditMed.Api.Data.Entities;

[Index("EstadoAfiliacion", Name = "IX_Pacientes_EstadoAfiliacion")]
[Index("Documento", Name = "UQ_Paciente_Documento", IsUnique = true)]
public partial class Paciente
{
    [Key]
    public int IdPaciente { get; set; }

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [StringLength(150)]
    public string Apellido { get; set; } = null!;

    [StringLength(20)]
    public string Documento { get; set; } = null!;

    [StringLength(20)]
    public string EstadoAfiliacion { get; set; } = null!;

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<Atencion> Atencion { get; set; } = new List<Atencion>();
}
