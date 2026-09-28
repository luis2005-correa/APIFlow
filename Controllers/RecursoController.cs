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
    public class RecursoController : ControllerBase
    {
        private readonly IRecursoService _recursoService;

        public RecursoController(IRecursoService recursoService)
        {
            _recursoService = recursoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RecursoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var recursos = await _recursoService.GetAll();
            return Ok(recursos);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecursoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var recurso = await _recursoService.GetById(id);
            if (recurso == null)
            {
                return NotFound(new { mensaje = $"No se encontró el recurso con ID {id}." });
            }
            return Ok(recurso);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RecursoViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var recursos = await _recursoService.GetByProyectoId(idProyecto);
            return Ok(recursos);
        }

        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RecursoViewModel>))]
        public async Task<IActionResult> GetByTipo(string tipo)
        {
            var recursos = await _recursoService.GetByTipo(tipo);
            return Ok(recursos);
        }

        [HttpGet("proyecto/{idProyecto:long}/costo-total")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        public async Task<IActionResult> GetCostoTotalByProyectoId(long idProyecto)
        {
            var costoTotal = await _recursoService.GetCostoTotalByProyectoId(idProyecto);
            return Ok(costoTotal);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecursoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RecursoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoRecurso = await _recursoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoRecurso.Id }, nuevoRecurso);
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
        public async Task<IActionResult> Update(long id, [FromBody] RecursoViewModel model)
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
                var resultado = await _recursoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el recurso con ID {id}." });
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
            var resultado = await _recursoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el recurso con ID {id}." });
            }

            return NoContent();
        }
    }
}