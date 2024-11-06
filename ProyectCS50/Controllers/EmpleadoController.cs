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
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IMapper _mapper;
 
        public EmpleadoController(IEmpleadoRepository empleadoRepo, ILogger<EmpleadoController> logger,
            IMapper mapper)
        {
            _empleadoRepo = empleadoRepo;
            _logger = logger;
            _mapper = mapper;
            
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetEmpleados()
        {
            try
            {
                _logger.LogInformation("Obteniendo los empleados");

                var empleados = await _empleadoRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<EmpleadoDto>>(empleados));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los empleados: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los empleados.");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmpleadoDto>> GetEmpleado(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Numero de empleado no válido: {id}");
                return BadRequest("Numero de empleado no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo empleado con numero de empleado: {id}");

                var empleado = await _empleadoRepo.GetById(id);

                if (empleado == null)
                {
                    _logger.LogWarning($"No se encontró ningún empleado con numero de empleado: {id}");
                    return NotFound("Empleado no encontrado.");
                }

                return Ok(_mapper.Map<EmpleadoDto>(empleado));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleado con numero de empleado {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener el empleado.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmpleadoDto>> PostEmpleado(EmpleadoCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un empleado nulo en la solicitud.");
                return BadRequest("El empleado no puede ser nulo.");
            }

            try
            {
                _logger.LogInformation($"Creando un nuevo empleado con nombre: {createDto.PrimerNombre}");


                // Verificar la validez del modelo
                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de empleado no es válido.");
                    return BadRequest(ModelState);
                }

                // Crear el nuevo empleado
                var newEmpleado = _mapper.Map<Empleado>(createDto);

                await _empleadoRepo.CreateAsync(newEmpleado);

                _logger.LogInformation($"Nuevo empleado '{createDto.PrimerNombre}' creado con numero de empleado: " +
                    $"{newEmpleado.NumeroEmpleado}");
                return CreatedAtAction(nameof(GetEmpleados), new { id = newEmpleado.NumeroEmpleado }, newEmpleado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo empleado.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEmpleado(int id, EmpleadoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.NumeroEmpleado)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el numero del empleado no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando empleado con numero de empleado: {id}");

                var existingEmpleado = await _empleadoRepo.GetById(id);
                if (existingEmpleado == null)
                {
                    _logger.LogInformation($"No se encontró nungún empleado con numero de empleado: {id}");
                    return NotFound("El empleado no existe.");
                }


                _mapper.Map(updateDto, existingEmpleado);

                await _empleadoRepo.SaveChangesAsync();

                _logger.LogInformation($"Empleado con numero de empleado {id} actualizado correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _empleadoRepo.ExistsAsync(s => s.NumeroEmpleado == id))
                {
                    _logger.LogWarning($"No se encontró ningún empleado con numero de empleado: {id}");
                    return NotFound("El empleado no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el empleado " +
                        $"con numero de empleado: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar el empleado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar el empleado.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando empleado con numero de empleado: {id}");

                var empleado = await _empleadoRepo.GetById(id);
                if (empleado == null)
                {
                    _logger.LogInformation($"Eliminando empleado con numero de empleado: {id}");
                    return NotFound("Empleado no encontrado.");
                }

                await _empleadoRepo.DeleteAsync(empleado);



                _logger.LogInformation($"Empleado con numero de empleado {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el empleado con numero de empleado {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar el empleado.");
            }
        }
    }
}
