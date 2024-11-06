using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models.Dto;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleDeduccionController : ControllerBase
    {
        private readonly IDetalleDeduccionRepository _deduccionRepo;
        private readonly ILogger<DetalleDeduccionController> _logger;
        private readonly IMapper _mapper;

        public DetalleDeduccionController(
            IDetalleDeduccionRepository deduccionRepo,
            ILogger<DetalleDeduccionController> logger,
            IMapper mapper)
        {
            _deduccionRepo = deduccionRepo;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DetalleDeduccionDto>>> GetDetallesDeducciones()
        {
            try
            {
                _logger.LogInformation("Obteniendo los detalles deducciones");

                var deducciones = await _deduccionRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<DetalleDeduccionDto>>(deducciones));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los detalles deducciones: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los detalles deducciones.");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetalleDeduccionDto>> GetDetalleDeduccion(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de Detalle Detalle deducción no válido: {id}");
                return BadRequest("ID de DetalleDetalle deducción no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo Detalle deducción con ID: {id}");

                var deduccion = await _deduccionRepo.GetById(id);

                if (deduccion == null)
                {
                    _logger.LogWarning($"No se encontró ningun Detalle deduccion con ID: {id}");
                    return NotFound("Detalle Deducción no encontrada.");
                }

                return Ok(_mapper.Map<DetalleDeduccionDto>(deduccion));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener Detalle deducción con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener Detalle deducción.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetalleDeduccionDto>> PostDetalleDeduccion(DetalleDeduccionCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un Detalle deducción nula en la solicitud.");
                return BadRequest("El Detalle deducción no puede ser nula.");
            }

            try
            {

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de Detalle deducción no es válido.");
                    return BadRequest(ModelState);
                }
                var newDeduccion = _mapper.Map<DetalleDeduccione>(createDto);


                await _deduccionRepo.CreateAsync(newDeduccion);

                _logger.LogInformation($"Nuevo Detalle deducción creada con ID: " +
                    $"{newDeduccion.IdDetalleDeducciones}");
                return CreatedAtAction(nameof(GetDetalleDeduccion),
                    new { id = newDeduccion.IdDetalleDeducciones }, newDeduccion);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo Detalle deducción: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo Detalle deducción.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDetalleDeduccion(int id,
            DetalleDeduccionUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdDetalleDeducciones)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de Detalle deducción no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando Detalle deducción con ID: {id}");

                var existingDeduccion = await _deduccionRepo.GetById(id);
                if (existingDeduccion == null)
                {
                    _logger.LogInformation($"No se encontró ningun Detalle deducción con ID: {id}");
                    return NotFound("Detalle deducción no existe.");
                }

                _mapper.Map(updateDto, existingDeduccion);

                await _deduccionRepo.SaveChangesAsync();

                _logger.LogInformation($"Detalle Deducción con ID {id} actualizada correctamente.");

                return NoContent();


            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _deduccionRepo.ExistsAsync(a => a.IdDetalleDeducciones == id))
                {
                    _logger.LogWarning($"No se encontró ningun Detalle deducción con ID: {id}");
                    return NotFound("El Detalle deducción no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el Detalle deducción " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar el Detalle deducción.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Detalle deducción: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar el Detalle deducción.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDetalleDeduccion(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando Detalle deducción con ID: {id}");

                var deduccion = await _deduccionRepo.GetById(id);
                if (deduccion == null)
                {
                    _logger.LogInformation($"Eliminando Detalle deducción con ID: {id}");
                    return NotFound("Detalle deducción no encontrada.");
                }

                await _deduccionRepo.DeleteAsync(deduccion);

                _logger.LogInformation($"Detalle Deducción con ID {id} eliminada correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar Detalle deducción con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar Detalle deducción.");
            }
        }

    }
}
