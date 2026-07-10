using AuditMed.Api.Models.Dtos;
using AuditMed.Api.Services.AtencionService;
using Microsoft.AspNetCore.Mvc;

namespace AuditMed.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AtencionesController : ControllerBase
    {
        private readonly IAtencionService _service;

        public AtencionesController(IAtencionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Consultar una atención por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegistroAtencionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Crear nuevo registro de atención
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegistroAtencionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RegistroAtencionRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.IdAtencion }, created);
        }

        /// <summary>
        /// Actualizar registro de atención existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegistroAtencionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] RegistroAtencionRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        /// <summary>
        /// Eliminar registro de atención
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
