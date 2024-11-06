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
    public class DetalleIngresoController : ControllerBase
    {
        private readonly IDetalleIngresoRepository _ingresoRepo;
        private readonly ILogger<IngresoController> _logger;
        private readonly IMapper _mapper;


        public DetalleIngresoController(
            IDetalleIngresoRepository ingresoRepo,
            ILogger<IngresoController> logger,
            IMapper mapper)
        {
            _ingresoRepo = ingresoRepo;
            _logger = logger;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DetalleIngresoDto>>> GetDetalleIngresos()
        {
            try
            {
                _logger.LogInformation("Obteniendo Detalle ingresos");

                var ingresos = await _ingresoRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<DetalleIngresoDto>>(ingresos));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener Detalle ingresos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los Detalle ingresos.");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetalleIngresoDto>> GetDetalleIngreso(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de Detalle ingreso no válido: {id}");
                return BadRequest("ID de Detalle ingreso no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo Detalle ingreso con ID: {id}");

                var ingreso = await _ingresoRepo.GetById(id);

                if (ingreso == null)
                {
                    _logger.LogWarning($"No se encontró ningún Detalle ingreso con ID: {id}");
                    return NotFound("Detalle Ingreso no encontrado.");
                }

                return Ok(_mapper.Map<DetalleIngresoDto>(ingreso));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener Detalle ingreso con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener el Detalle ingreso.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetalleIngresoDto>> PostDetalleIngreso(DetalleIngresoCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un Detalle ingreso nulo en la solicitud.");
                return BadRequest("El Detalle ingreso no puede ser nulo.");
            }

            try
            {

                // Verificar la validez del modelo
                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de ingreso no es válido.");
                    return BadRequest(ModelState);
                }

                var newIngreso = _mapper.Map<DetalleIngreso>(createDto);

                await _ingresoRepo.CreateAsync(newIngreso);

                _logger.LogInformation($"Nuevo Detalle ingreso creado con ID: " +
                    $"{newIngreso.IdDetalleIngresos}");
                return CreatedAtAction(nameof(GetDetalleIngresos),
                    new { id = newIngreso.IdDetalleIngresos }, newIngreso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo Detalle ingreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo Detalle ingreso.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDetalleIngreso(int id,
            DetalleIngresoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdDetalleIngresos)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de Detalle ingreso no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando Detalle ingreso con ID: {id}");

                var existingIngreso = await _ingresoRepo.GetById(id);
                if (existingIngreso == null)
                {
                    _logger.LogInformation($"No se encontró ningún Detalle ingreso con ID: {id}");
                    return NotFound("El Detalle ingreso no existe.");
                }


                _mapper.Map(updateDto, existingIngreso);

                await _ingresoRepo.SaveChangesAsync();

                _logger.LogInformation($"Detalle Ingreso con ID {id} actualizado correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _ingresoRepo.ExistsAsync(a => a.IdDetalleIngresos == id))
                {
                    _logger.LogWarning($"No se encontró ningún Detalle ingreso con ID: {id}");
                    return NotFound("El Detalle ingreso no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el Detalle ingreso " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar el Detalle ingreso.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Detalle ingreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar el Detalle ingreso.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDetalleIngreso(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando Detalle ingreso con ID: {id}");

                var ingreso = await _ingresoRepo.GetById(id);
                if (ingreso == null)
                {
                    _logger.LogInformation($"Eliminando Detalle ingreso con ID: {id}");
                    return NotFound("Detalle Ingreso no encontrado.");
                }

                await _ingresoRepo.DeleteAsync(ingreso);


                _logger.LogInformation($"Detalle Ingreso con ID {id} eliminada correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el Detalle ingreso con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar el Detalle ingreso.");
            }
        }
    }
}
