using AuditMed.Api.Models.Dtos;

namespace AuditMed.Api.Services.AtencionService
{
    public interface IAtencionService
    {
        Task<RegistroAtencionResponseDto?> GetByIdAsync(int id);
        Task<RegistroAtencionResponseDto> CreateAsync(RegistroAtencionRequestDto dto);
        Task<RegistroAtencionResponseDto?> UpdateAsync(int id, RegistroAtencionRequestDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RegistroAtencionResponseDto>> GetAtencionesAuditoriaAsync();
    }
}