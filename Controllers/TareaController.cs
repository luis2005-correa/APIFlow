using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;
using AcademiaFlowAPI.Dto;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _tareaService.GetAll();
            return Ok(tareas);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TareaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null)
            {
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}." });
            }
            return Ok(tarea);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var tareas = await _tareaService.GetByProyectoId(idProyecto);
            return Ok(tareas);
        }

        [HttpGet("fase/{idFase:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaViewModel>))]
        public async Task<IActionResult> GetByFaseId(long idFase)
        {
            var tareas = await _tareaService.GetByFaseId(idFase);
            return Ok(tareas);
        }

        [HttpGet("responsable/{idResponsable:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaViewModel>))]
        public async Task<IActionResult> GetByResponsableId(long idResponsable)
        {
            var tareas = await _tareaService.GetByResponsableId(idResponsable);
            return Ok(tareas);
        }

        [HttpGet("estado/{estado}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaViewModel>))]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            var tareas = await _tareaService.GetByEstado(estado);
            return Ok(tareas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TareaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TareaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaTarea = await _tareaService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaTarea.Id }, nuevaTarea);
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
        public async Task<IActionResult> Update(long id, [FromBody] TareaViewModel model)
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
                var resultado = await _tareaService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró la tarea con ID {id} para actualizar." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id:long}/estado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CambiarEstado(long id, [FromBody] CambiarEstadoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto?.NuevoEstado))
            {
                return BadRequest(new { mensaje = "El nuevo estado es requerido." });
            }

            var resultado = await _tareaService.CambiarEstado(id, dto.NuevoEstado);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}." });
            }

            return NoContent();
        }

        [HttpPatch("{id:long}/avance")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarAvance(long id, [FromBody] ActualizarAvanceDto dto)
        {
            if (dto == null || dto.PorcentajeAvance < 0 || dto.PorcentajeAvance > 100)
            {
                return BadRequest(new { mensaje = "El porcentaje de avance debe estar entre 0 y 100." });
            }

            var resultado = await _tareaService.ActualizarAvance(id, dto.PorcentajeAvance);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}." });
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _tareaService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la tarea con ID {id}." });
            }

            return NoContent();
        }
    }



}