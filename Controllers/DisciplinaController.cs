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
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DisciplinaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var disciplinas = await _disciplinaService.GetAll();
            return Ok(disciplinas);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisciplinaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var disciplina = await _disciplinaService.GetById(id);
            if (disciplina == null)
            {
                return NotFound(new { mensaje = $"No se encontró la disciplina con ID {id}." });
            }
            return Ok(disciplina);
        }

        [HttpGet("activas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DisciplinaViewModel>))]
        public async Task<IActionResult> GetActivas()
        {
            var disciplinas = await _disciplinaService.GetActivas();
            return Ok(disciplinas);
        }

        [HttpGet("padre/{idPadre:long}/subdisciplinas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DisciplinaViewModel>))]
        public async Task<IActionResult> GetSubdisciplinas(long idPadre)
        {
            var subdisciplinas = await _disciplinaService.GetSubdisciplinas(idPadre);
            return Ok(subdisciplinas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DisciplinaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] DisciplinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaDisciplina = await _disciplinaService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaDisciplina.Id }, nuevaDisciplina);
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
        public async Task<IActionResult> Update(long id, [FromBody] DisciplinaViewModel model)
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
                var resultado = await _disciplinaService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la disciplina con ID {id}." });
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
            var resultado = await _disciplinaService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la disciplina con ID {id}." });
            }

            return NoContent();
        }
    }
}