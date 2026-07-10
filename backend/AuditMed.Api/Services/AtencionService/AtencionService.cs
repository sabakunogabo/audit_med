using AuditMed.Api.Data.Entities;
using AuditMed.Api.Models.Dtos;
using AuditMed.Api.Repositories.AtencionRepository;
namespace AuditMed.Api.Services.AtencionService
{
    public class AtencionService : IAtencionService
    {
        private readonly IAtencionRepository _repository;
        private readonly ILogger<AtencionService> _logger;
        public AtencionService(IAtencionRepository repository, ILogger<AtencionService> logger)
        {
            _repository = repository; 
            _logger = logger;
        }
        public async Task<RegistroAtencionResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }
        public async Task<RegistroAtencionResponseDto> CreateAsync(RegistroAtencionRequestDto dto)
        {
            var entity = new RegistroAtencion
            {
                DocumentoPaciente = dto.DocumentoPaciente,
                CodigoDiagnostico = dto.CodigoDiagnostico,
                FechaAtencion = dto.FechaAtencion,
                RequiereAuditoria = dto.RequiereAuditoria
            };
            var createdEntity = await _repository.AddAsync(entity);
            _logger.LogInformation("Atención creada exitosamente con ID {Id}", createdEntity.IdAtencion);
            return MapToDto(createdEntity);
        }
        public async Task<RegistroAtencionResponseDto?> UpdateAsync(int id, RegistroAtencionRequestDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null) return null;
            existingEntity.DocumentoPaciente = dto.DocumentoPaciente;
            existingEntity.CodigoDiagnostico = dto.CodigoDiagnostico;
            existingEntity.FechaAtencion = dto.FechaAtencion;
            existingEntity.RequiereAuditoria = dto.RequiereAuditoria;
            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            if (updatedEntity is null)
            {
                return null;
            }
            return MapToDto(updatedEntity);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
            public async Task<IEnumerable<RegistroAtencionResponseDto>> GetAtencionesAuditoriaAsync()
            {
                var registros = await _repository.GetAllAsync();
                var fechaLimite = DateTime.Now.AddDays(-30);
                var resultadoAuditoria = registros
                    .Where(r => !string.IsNullOrWhiteSpace(r.CodigoDiagnostico))
                    .Select(r => new RegistroAtencionResponseDto
                    {
                        IdAtencion = r.IdAtencion,
                        DocumentoPaciente = r.DocumentoPaciente,
                        CodigoDiagnostico = r.CodigoDiagnostico,
                        FechaAtencion = r.FechaAtencion,
                        RequiereAuditoria = r.FechaAtencion < fechaLimite || r.RequiereAuditoria
                    })
                    .OrderByDescending(r => r.FechaAtencion)
                    .ToList();
                return resultadoAuditoria;
            }
        private static RegistroAtencionResponseDto MapToDto(RegistroAtencion entity) => new()
        {
            IdAtencion = entity.IdAtencion,
            DocumentoPaciente = entity.DocumentoPaciente,
            CodigoDiagnostico = entity.CodigoDiagnostico,
            FechaAtencion = entity.FechaAtencion,
            RequiereAuditoria = entity.RequiereAuditoria
        };
    }
}