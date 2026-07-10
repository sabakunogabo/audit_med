using AuditMed.Api.Models.Dtos;
using AuditMed.Api.Services.AtencionService;
using Microsoft.AspNetCore.Mvc;

namespace AuditMed.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAtencionService _service;

        public AuditoriaController(IAtencionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene las atenciones que pasan las reglas de auditoría médica.
        /// </summary>
        [HttpGet("atenciones")]
        [ProducesResponseType(typeof(IEnumerable<RegistroAtencionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAtencionesAuditoria()
        {
            var result = await _service.GetAtencionesAuditoriaAsync();
            return Ok(result);
        }
    }
}