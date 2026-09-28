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
    public class AsignacionRecursoController : ControllerBase
    {
        private readonly IAsignacionRecursoService _asignacionRecursoService;

        public AsignacionRecursoController(IAsignacionRecursoService asignacionRecursoService)
        {
            _asignacionRecursoService = asignacionRecursoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionRecursoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var asignaciones = await _asignacionRecursoService.GetAll();
            return Ok(asignaciones);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AsignacionRecursoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var asignacion = await _asignacionRecursoService.GetById(id);
            if (asignacion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la asignación de recurso con ID {id}." });
            }
            return Ok(asignacion);
        }

        [HttpGet("recurso/{idRecurso:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionRecursoViewModel>))]
        public async Task<IActionResult> GetByRecursoId(long idRecurso)
        {
            var asignaciones = await _asignacionRecursoService.GetByRecursoId(idRecurso);
            return Ok(asignaciones);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionRecursoViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var asignaciones = await _asignacionRecursoService.GetByTareaId(idTarea);
            return Ok(asignaciones);
        }

        [HttpGet("fase/{idFase:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionRecursoViewModel>))]
        public async Task<IActionResult> GetByFaseId(long idFase)
        {
            var asignaciones = await _asignacionRecursoService.GetByFaseId(idFase);
            return Ok(asignaciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AsignacionRecursoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AsignacionRecursoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaAsignacion = await _asignacionRecursoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaAsignacion.Id }, nuevaAsignacion);
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
        public async Task<IActionResult> Update(long id, [FromBody] AsignacionRecursoViewModel model)
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
                var resultado = await _asignacionRecursoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la asignación con ID {id}." });
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
            var resultado = await _asignacionRecursoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la asignación de recurso con ID {id}." });
            }

            return NoContent();
        }
    }
}