using System.ComponentModel.DataAnnotations;

namespace AuditMed.Api.Models.Dtos
{
    public class RegistroAtencionRequestDto
    {
        [Required(ErrorMessage = "El documento del paciente es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El documento debe tener entre 5 y 20 caracteres.")]
        public string DocumentoPaciente { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "El código de diagnóstico no puede exceder 10 caracteres.")]
        public string? CodigoDiagnostico { get; set; }

        [Required(ErrorMessage = "La fecha de atención es obligatoria.")]
        public DateTime FechaAtencion { get; set; }

        public bool RequiereAuditoria { get; set; } = false;
    }
}