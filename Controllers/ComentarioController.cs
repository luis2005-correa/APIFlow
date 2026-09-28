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
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;

        public ComentarioController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ComentarioViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var comentarios = await _comentarioService.GetAll();
            return Ok(comentarios);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ComentarioViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var comentario = await _comentarioService.GetById(id);
            if (comentario == null)
            {
                return NotFound(new { mensaje = $"No se encontró el comentario con ID {id}." });
            }
            return Ok(comentario);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ComentarioViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var comentarios = await _comentarioService.GetByProyectoId(idProyecto);
            return Ok(comentarios);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ComentarioViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var comentarios = await _comentarioService.GetByTareaId(idTarea);
            return Ok(comentarios);
        }

        [HttpGet("actividad/{idActividad:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ComentarioViewModel>))]
        public async Task<IActionResult> GetByActividadId(long idActividad)
        {
            var comentarios = await _comentarioService.GetByActividadId(idActividad);
            return Ok(comentarios);
        }

        [HttpGet("{idComentarioPadre:long}/respuestas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ComentarioViewModel>))]
        public async Task<IActionResult> GetRespuestas(long idComentarioPadre)
        {
            var respuestas = await _comentarioService.GetRespuestas(idComentarioPadre);
            return Ok(respuestas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ComentarioViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ComentarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoComentario = await _comentarioService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoComentario.Id }, nuevoComentario);
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
        public async Task<IActionResult> Update(long id, [FromBody] ComentarioViewModel model)
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
                var resultado = await _comentarioService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el comentario con ID {id} o ha sido eliminado." });
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
            var resultado = await _comentarioService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el comentario con ID {id}." });
            }

            return NoContent();
        }
    }
}