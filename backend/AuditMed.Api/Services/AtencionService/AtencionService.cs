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
            _repository = repository; // Inyección de la abstracción, no de DbContext
            _logger = logger;
        }

        public async Task<RegistroAtencionResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<RegistroAtencionResponseDto> CreateAsync(RegistroAtencionRequestDto dto)
        {
            // El servicio solo construye la entidad y le dice al repo que la guarde
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

            // Modificamos la entidad desacoplada
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

        /// <summary>
        /// REQUERIMIENTO DE LA PRUEBA: Lógica de auditoría usando LINQ en C#
        /// </summary>
        public async Task<IEnumerable<RegistroAtencionResponseDto>> GetAtencionesAuditoriaAsync()
        {
            // 1. El servicio le pide TODOS los datos al repositorio (podríamos optimizar esto después con un método específico en el repo, pero para cumplir el enunciado "usando LINQ en C#", traemos a memoria)
            var registros = await _repository.GetAllAsync();

            // 2. LÓGICA DE NEGOCIO PURA EN MEMORIA (LINQ to Objects)
            var fechaLimite = DateTime.Now.AddDays(-30);

            var resultadoAuditoria = registros
                .Where(r => !string.IsNullOrWhiteSpace(r.CodigoDiagnostico)) // Regla 1: Filtrar nulos/vacíos
                .Select(r => new RegistroAtencionResponseDto
                {
                    IdAtencion = r.IdAtencion,
                    DocumentoPaciente = r.DocumentoPaciente,
                    CodigoDiagnostico = r.CodigoDiagnostico,
                    FechaAtencion = r.FechaAtencion,
                    RequiereAuditoria = r.FechaAtencion < fechaLimite    // Regla 2: Marcar si > 30 días
                })
                .OrderByDescending(r => r.FechaAtencion)                  // Regla 3: Más reciente primero
                .ToList();

            return resultadoAuditoria;
        }

        // Mapper manual
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