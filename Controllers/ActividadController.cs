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
    public class ActividadController : ControllerBase
    {
        private readonly IActividadService _actividadService;

        public ActividadController(IActividadService actividadService)
        {
            _actividadService = actividadService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActividadViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var actividades = await _actividadService.GetAll();
            return Ok(actividades);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActividadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var actividad = await _actividadService.GetById(id);
            if (actividad == null)
            {
                return NotFound(new { mensaje = $"No se encontró la actividad con ID {id}." });
            }
            return Ok(actividad);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActividadViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var actividades = await _actividadService.GetByTareaId(idTarea);
            return Ok(actividades);
        }

        [HttpGet("responsable/{idResponsable:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActividadViewModel>))]
        public async Task<IActionResult> GetByResponsableId(long idResponsable)
        {
            var actividades = await _actividadService.GetByResponsableId(idResponsable);
            return Ok(actividades);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActividadViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ActividadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaActividad = await _actividadService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaActividad.Id }, nuevaActividad);
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
        public async Task<IActionResult> Update(long id, [FromBody] ActividadViewModel model)
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
                var resultado = await _actividadService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la actividad con ID {id}." });
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
            var resultado = await _actividadService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la actividad con ID {id}." });
            }

            return NoContent();
        }
    }
}