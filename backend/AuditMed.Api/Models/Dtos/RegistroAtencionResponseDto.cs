namespace AuditMed.Api.Models.Dtos
{
    public class RegistroAtencionResponseDto
    {
        public int IdAtencion { get; set; }
        public string DocumentoPaciente { get; set; } = string.Empty;
        public string? CodigoDiagnostico { get; set; }
        public DateTime FechaAtencion { get; set; }
        public bool RequiereAuditoria { get; set; }
    }
}