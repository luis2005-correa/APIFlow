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
    public class ProyectoDisciplinaController : ControllerBase
    {
        private readonly IProyectoDisciplinaService _proyectoDisciplinaService;

        public ProyectoDisciplinaController(IProyectoDisciplinaService proyectoDisciplinaService)
        {
            _proyectoDisciplinaService = proyectoDisciplinaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoDisciplinaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var relaciones = await _proyectoDisciplinaService.GetAll();
            return Ok(relaciones);
        }

        [HttpGet("{idProyecto:long}/{idDisciplina:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoDisciplinaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long idProyecto, long idDisciplina)
        {
            var relacion = await _proyectoDisciplinaService.GetById(idProyecto, idDisciplina);
            if (relacion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la relación entre el proyecto {idProyecto} y la disciplina {idDisciplina}." });
            }
            return Ok(relacion);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoDisciplinaViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var relaciones = await _proyectoDisciplinaService.GetByProyectoId(idProyecto);
            return Ok(relaciones);
        }

        [HttpGet("disciplina/{idDisciplina:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoDisciplinaViewModel>))]
        public async Task<IActionResult> GetByDisciplinaId(long idDisciplina)
        {
            var relaciones = await _proyectoDisciplinaService.GetByDisciplinaId(idDisciplina);
            return Ok(relaciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProyectoDisciplinaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProyectoDisciplinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaRelacion = await _proyectoDisciplinaService.Create(model);
                return CreatedAtAction(
                    nameof(GetById),
                    new { idProyecto = nuevaRelacion.IdProyecto, idDisciplina = nuevaRelacion.IdDisciplina },
                    nuevaRelacion
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{idProyecto:long}/{idDisciplina:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long idProyecto, long idDisciplina, [FromBody] ProyectoDisciplinaViewModel model)
        {
            if (idProyecto != model.IdProyecto || idDisciplina != model.IdDisciplina)
            {
                return BadRequest(new { mensaje = "Los IDs de la URL no coinciden con los IDs del modelo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _proyectoDisciplinaService.Update(idProyecto, idDisciplina, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No existe la relación entre el proyecto {idProyecto} y la disciplina {idDisciplina}." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{idProyecto:long}/{idDisciplina:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long idProyecto, long idDisciplina)
        {
            var resultado = await _proyectoDisciplinaService.Delete(idProyecto, idDisciplina);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la relación entre el proyecto {idProyecto} y la disciplina {idDisciplina}." });
            }

            return NoContent();
        }
    }
}