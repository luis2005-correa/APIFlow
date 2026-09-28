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
    public class ProgramaAcademicoController : ControllerBase
    {
        private readonly IProgramaAcademicoService _programaAcademicoService;

        public ProgramaAcademicoController(IProgramaAcademicoService programaAcademicoService)
        {
            _programaAcademicoService = programaAcademicoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProgramaAcademicoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var programas = await _programaAcademicoService.GetAll();
            return Ok(programas);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramaAcademicoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var programa = await _programaAcademicoService.GetById(id);
            if (programa == null)
            {
                return NotFound(new { mensaje = $"No se encontró el programa académico con ID {id}." });
            }
            return Ok(programa);
        }

        [HttpGet("activos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProgramaAcademicoViewModel>))]
        public async Task<IActionResult> GetActivos()
        {
            var programas = await _programaAcademicoService.GetActivos();
            return Ok(programas);
        }

        [HttpGet("unidad-academica/{idUnidadAcademica:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProgramaAcademicoViewModel>))]
        public async Task<IActionResult> GetByUnidadAcademicaId(long idUnidadAcademica)
        {
            var programas = await _programaAcademicoService.GetByUnidadAcademicaId(idUnidadAcademica);
            return Ok(programas);
        }

        [HttpGet("snies/{codigoSnies}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramaAcademicoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCodigoSnies(string codigoSnies)
        {
            var programa = await _programaAcademicoService.GetByCodigoSnies(codigoSnies);
            if (programa == null)
            {
                return NotFound(new { mensaje = $"No se encontró el programa académico con código SNIES '{codigoSnies}'." });
            }
            return Ok(programa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgramaAcademicoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProgramaAcademicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoPrograma = await _programaAcademicoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoPrograma.Id }, nuevoPrograma);
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
        public async Task<IActionResult> Update(long id, [FromBody] ProgramaAcademicoViewModel model)
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
                var resultado = await _programaAcademicoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el programa académico con ID {id}." });
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
            var resultado = await _programaAcademicoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el programa académico con ID {id}." });
            }

            return NoContent();
        }
    }
}