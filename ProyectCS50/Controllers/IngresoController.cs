using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Models.Dto;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly I_IngresoRepository _ingresoRepo;
        private readonly ILogger<IngresoController> _logger;
        private readonly IMapper _mapper;


        public IngresoController(
            I_IngresoRepository ingresoRepo,
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
        public async Task<ActionResult<IEnumerable<IngresoDto>>> GetIngresos()
        {
            try
            {
                _logger.LogInformation("Obteniendo los ingresos");

                var ingresos = await _ingresoRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<IngresoDto>>(ingresos));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los ingresos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los ingresos.");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IngresoDto>> GetIngreso(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de ingreso no válido: {id}");
                return BadRequest("ID de ingreso no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo ingreso con ID: {id}");

                var ingreso = await _ingresoRepo.GetById(id);

                if (ingreso == null)
                {
                    _logger.LogWarning($"No se encontró ningún ingreso con ID: {id}");
                    return NotFound("Ingreso no encontrado.");
                }

                return Ok(_mapper.Map<IngresoDto>(ingreso));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ingreso con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener el ingreso.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IngresoDto>> PostIngreso(IngresoCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un ingreso nulo en la solicitud.");
                return BadRequest("El ingreso no puede ser nulo.");
            }

            try
            {
               
                // Verificar la validez del modelo
                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de ingreso no es válido.");
                    return BadRequest(ModelState);
                }

                var newIngreso = _mapper.Map<Ingreso>(createDto);

                await _ingresoRepo.CreateAsync(newIngreso);

                _logger.LogInformation($"Nuevo ingreso creado con ID: " +
                    $"{newIngreso.IdIngreso}");
                return CreatedAtAction(nameof(GetIngresos),
                    new { id = newIngreso.IdIngreso }, newIngreso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo ingreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo ingreso.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutIngreso(int id,
            IngresoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdIngreso)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de ingreso no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando ingreso con ID: {id}");

                var existingIngreso = await _ingresoRepo.GetById(id);
                if (existingIngreso == null)
                {
                    _logger.LogInformation($"No se encontró ningún ingreso con ID: {id}");
                    return NotFound("El ingreso no existe.");
                }

           
                _mapper.Map(updateDto, existingIngreso);

                await _ingresoRepo.SaveChangesAsync();

                _logger.LogInformation($"Ingreso con ID {id} actualizado correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _ingresoRepo.ExistsAsync(a => a.IdIngreso == id))
                {
                    _logger.LogWarning($"No se encontró ningún ingreso con ID: {id}");
                    return NotFound("El ingreso no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el ingreso " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar el ingreso.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el ingreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar el ingreso.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando ingreso con ID: {id}");

                var ingreso = await _ingresoRepo.GetById(id);
                if (ingreso == null)
                {
                    _logger.LogInformation($"Eliminando ingreso con ID: {id}");
                    return NotFound("Ingreso no encontrado.");
                }

                await _ingresoRepo.DeleteAsync(ingreso);


                _logger.LogInformation($"Ingreso con ID {id} eliminada correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el ingreso con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar el ingreso.");
            }
        }
    }
}
