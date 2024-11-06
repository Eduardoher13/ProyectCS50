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
    public class NominaController : ControllerBase
    {
        private readonly IEmpleadoRepository _empleadoRepo;
        public readonly I_IngresoRepository _ingresoRepo;
        public readonly IDeduccionRepository _deduccionRepo;
        private readonly INominaRepository _nominaRepo;
        private readonly ILogger<NominaController> _logger;
        private readonly IMapper _mapper;


        public NominaController(IEmpleadoRepository empleadoRepo, I_IngresoRepository i_Ingreso, IDeduccionRepository deducRepo,
            INominaRepository nominaRepo,
            ILogger<NominaController> logger,
            IMapper mapper)
        {
            _nominaRepo = nominaRepo;
            _empleadoRepo = empleadoRepo;
            _deduccionRepo = deducRepo;
            _ingresoRepo = i_Ingreso;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<NominaDto>>> GetNominas()
        {
            try
            {
                _logger.LogInformation("Obteniendo las nominas");

                var nominas = await _nominaRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<NominaDto>>(nominas));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las nominas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener las nominas.");
            }
        }

        //[HttpGet("nominas-completas")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IEnumerable<NominaCompletaDto>>> GetNominasFinalTodosLosEmpleados()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Obteniendo las nominas de todos los empleados");

        //        var empleados = await _empleadoRepo.GetAllAsync();
        //        var ingresos = await _ingresoRepo.GetAllAsync();
        //        var deducciones = await _deduccionRepo.GetAllAsync();
        //        var nominas = await _nominaRepo.GetAllAsync();

        //        var resultado = from e in empleados
        //                        join i in ingresos on e.NumeroEmpleado equals i.NumeroEmpleado
        //                        join d in deducciones on e.NumeroEmpleado equals d.NumeroEmpleado
        //                        join n in nominas on e.NumeroEmpleado equals n.NumeroEmpleado
        //                        select new NominaCompletaDto
        //                        {
        //                            NumeroEmpleado = e.NumeroEmpleado,
        //                            NumeroCedula = e.NumeroCedula,
        //                            NumeroINSS = e.NumeroINSS,
        //                            NumeroRUC = e.NumeroRUC,
        //                            PrimerNombre = e.PrimerNombre,
        //                            SegundoNombre = e.SegundoNombre,
        //                            PrimerApellido = e.PrimerApellido,
        //                            SegundoApellido = e.SegundoApellido,
        //                            FechaNacimiento = e.FechaNacimiento,
        //                            Sexo = e.Sexo,
        //                            EstadoCivil = e.EstadoCivil,
        //                            Direccion = e.Direccion,
        //                            Telefono = e.Telefono,
        //                            Celular = e.Celular,
        //                            FechaContratacion = e.FechaContratacion,
        //                            FechaCierreContrato = e.FechaCierreContrato,
        //                            EstadoEmpleado = e.EstadoEmpleado,
        //                            YearsTrabajados = e.YearsTrabajados,
        //                            SalarioOrdinario = i.SalarioOrdinario,
        //                            Antiguedad = i.Antiguedad,
        //                            RiesgoLaboral = i.RiesgoLaboral,
        //                            Nocturnidad = i.Nocturnidad,
        //                            HorasExtras = i.HorasExtras,
        //                            ConceptoOtrosIngresos = i.ConceptoOtrosIngresos,
        //                            OtrosIngresos = i.OtrosIngresos,
        //                            TotalIngresos = i.TotalIngresos,
        //                            INSS = d.INSS,
        //                            IR = d.IR,
        //                            ConceptoOD = d.ConceptoOD,
        //                            MontoOtrasDeducciones = d.MontoOtrasDeducciones,
        //                            TotalDeducciones = d.TotalDeducciones,
        //                            SalarioNeto = n.SalarioNeto,
        //                            FechaNomina = n.FechaNomina
        //                        };

        //        return Ok(resultado.ToList());


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error al obtener las nominas: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error interno del servidor al obtener las nominas.");
        //    }
        //}


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NominaDto>> GetNomina(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de nomina no válido: {id}");
                return BadRequest("ID de nomina no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo nomina con ID: {id}");

                var nomina = await _nominaRepo.GetById(id);

                if (nomina == null)
                {
                    _logger.LogWarning($"No se encontró ninguna nomina con ID: {id}");
                    return NotFound("Nomina no encontrada.");
                }

                return Ok(_mapper.Map<NominaDto>(nomina));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener nomina con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener la nomina.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NominaDto>> PostNomina(NominaCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió una nomina nula en la solicitud.");
                return BadRequest("La nomina no puede ser nula.");
            }

            try
            {
                _logger.LogInformation($"Creando una nueva nomina para el empleado " +
                    $"con ID: {createDto.NumeroEmpleado}");


                var empleadoExist = await _empleadoRepo.ExistsAsync(
                    s => s.NumeroEmpleado == createDto.NumeroEmpleado);

                if (!empleadoExist)
                {
                    _logger.LogWarning($"El empleado con numero de empleado '{createDto.NumeroEmpleado}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de nomina no es válido.");
                    return BadRequest(ModelState);
                }

                var newNomina = _mapper.Map<Nomina>(createDto);

                await _nominaRepo.CreateAsync(newNomina);

                _logger.LogInformation($"Nueva nomina creado con ID: " +
                    $"{newNomina.IdNomina}");
                return CreatedAtAction(nameof(GetNominas),
                    new { id = newNomina.IdNomina }, newNomina);

              

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear una nueva nomina: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear una nueva nomina.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutNomina(int id,
            NominaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdNomina)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de nomina no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando nomina con ID: {id}");

                var existingNomina = await _nominaRepo.GetById(id);
                if (existingNomina == null)
                {
                    _logger.LogInformation($"No se encontró ninguna nomina con ID: {id}");
                    return NotFound("La nomina no existe.");
                }


                var empleadoExists = await _empleadoRepo.ExistsAsync(
                    s => s.NumeroEmpleado == updateDto.NumeroEmpleado);

                if (!empleadoExists)
                {
                    _logger.LogWarning($"El empleado con numero de empleado '{updateDto.NumeroEmpleado}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }


                _mapper.Map(updateDto, existingNomina);

                await _nominaRepo.SaveChangesAsync();

                _logger.LogInformation($"Nomina con ID {id} actualizada correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _nominaRepo.ExistsAsync(a => a.IdNomina == id))
                {
                    _logger.LogWarning($"No se encontró ninguna nomina con ID: {id}");
                    return NotFound("La nomina no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar la nomina " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar la nomina.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la nomina: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar la nomina.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNomina(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando nomina con ID: {id}");

                var nomina = await _nominaRepo.GetById(id);
                if (nomina == null)
                {
                    _logger.LogInformation($"Eliminando nomina con ID: {id}");
                    return NotFound("Nomina no encontrada.");
                }

                await _nominaRepo.DeleteAsync(nomina);

                _logger.LogInformation($"Nomina con ID {id} eliminada correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el nomina con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar la nomina.");
            }
        }
    }
}
