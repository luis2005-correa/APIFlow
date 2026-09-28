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
    public class AsignacionTareaController : ControllerBase
    {
        private readonly IAsignacionTareaService _asignacionTareaService;

        public AsignacionTareaController(IAsignacionTareaService asignacionTareaService)
        {
            _asignacionTareaService = asignacionTareaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionTareaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var asignaciones = await _asignacionTareaService.GetAll();
            return Ok(asignaciones);
        }

        [HttpGet("tarea/{idTarea:long}/usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AsignacionTareaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long idTarea, long idUsuario)
        {
            var asignacion = await _asignacionTareaService.GetById((idTarea, idUsuario));
            if (asignacion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la asignación para la Tarea {idTarea} y Usuario {idUsuario}." });
            }
            return Ok(asignacion);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionTareaViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var asignaciones = await _asignacionTareaService.GetByTareaId(idTarea);
            return Ok(asignaciones);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AsignacionTareaViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var asignaciones = await _asignacionTareaService.GetByUsuarioId(idUsuario);
            return Ok(asignaciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AsignacionTareaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AsignacionTareaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaAsignacion = await _asignacionTareaService.Create(model);
                return CreatedAtAction(
                    nameof(GetById),
                    new { idTarea = nuevaAsignacion.IdTarea, idUsuario = nuevaAsignacion.IdUsuario },
                    nuevaAsignacion
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("tarea/{idTarea:long}/usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long idTarea, long idUsuario, [FromBody] AsignacionTareaViewModel model)
        {
            if (idTarea != model.IdTarea || idUsuario != model.IdUsuario)
            {
                return BadRequest(new { mensaje = "Los parámetros de la URL no coinciden con las claves compuestas del modelo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _asignacionTareaService.Update((idTarea, idUsuario), model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la asignación para Tarea {idTarea} y Usuario {idUsuario}." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("tarea/{idTarea:long}/usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long idTarea, long idUsuario)
        {
            var resultado = await _asignacionTareaService.Delete((idTarea, idUsuario));
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la asignación para Tarea {idTarea} y Usuario {idUsuario}." });
            }

            return NoContent();
        }
    }
}