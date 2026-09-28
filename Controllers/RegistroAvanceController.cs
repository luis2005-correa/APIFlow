using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroAvanceController : ControllerBase
    {
        private readonly IRegistroAvanceService _registroAvanceService;

        public RegistroAvanceController(IRegistroAvanceService registroAvanceService)
        {
            _registroAvanceService = registroAvanceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RegistroAvanceViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var avances = await _registroAvanceService.GetAll();
            return Ok(avances);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistroAvanceViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var avance = await _registroAvanceService.GetById(id);
            if (avance == null)
            {
                return NotFound(new { mensaje = $"No se encontró el registro de avance con ID {id}." });
            }
            return Ok(avance);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RegistroAvanceViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var avances = await _registroAvanceService.GetByTareaId(idTarea);
            return Ok(avances);
        }

        [HttpGet("actividad/{idActividad:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RegistroAvanceViewModel>))]
        public async Task<IActionResult> GetByActividadId(long idActividad)
        {
            var avances = await _registroAvanceService.GetByActividadId(idActividad);
            return Ok(avances);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RegistroAvanceViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var avances = await _registroAvanceService.GetByUsuarioId(idUsuario);
            return Ok(avances);
        }

        [HttpGet("tarea/{idTarea:long}/horas-totales")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        public async Task<IActionResult> GetHorasTotalesByTareaId(long idTarea)
        {
            var horasTotales = await _registroAvanceService.GetHorasTotalesByTareaId(idTarea);
            return Ok(horasTotales);
        }

        [HttpGet("actividad/{idActividad:long}/horas-totales")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        public async Task<IActionResult> GetHorasTotalesByActividadId(long idActividad)
        {
            var horasTotales = await _registroAvanceService.GetHorasTotalesByActividadId(idActividad);
            return Ok(horasTotales);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegistroAvanceViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RegistroAvanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoAvance = await _registroAvanceService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoAvance.Id }, nuevoAvance);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] RegistroAvanceViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del modelo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _registroAvanceService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el registro de avance con ID {id}." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _registroAvanceService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el registro de avance con ID {id}." });
            }

            return NoContent();
        }
    }
}