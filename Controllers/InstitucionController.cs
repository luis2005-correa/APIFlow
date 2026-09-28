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
    public class InstitucionController : ControllerBase
    {
        private readonly IInstitucionService _institucionService;

        public InstitucionController(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InstitucionViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var instituciones = await _institucionService.GetAll();
            return Ok(instituciones);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstitucionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var institucion = await _institucionService.GetById(id);
            if (institucion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la institución con ID {id}." });
            }
            return Ok(institucion);
        }

        [HttpGet("activas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InstitucionViewModel>))]
        public async Task<IActionResult> GetActivas()
        {
            var instituciones = await _institucionService.GetActivas();
            return Ok(instituciones);
        }

        [HttpGet("nit/{nit}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstitucionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNit(string nit)
        {
            var institucion = await _institucionService.GetByNit(nit);
            if (institucion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la institución con NIT {nit}." });
            }
            return Ok(institucion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InstitucionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] InstitucionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaInstitucion = await _institucionService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaInstitucion.Id }, nuevaInstitucion);
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
        public async Task<IActionResult> Update(long id, [FromBody] InstitucionViewModel model)
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
                var resultado = await _institucionService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la institución con ID {id}." });
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
            var resultado = await _institucionService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la institución con ID {id}." });
            }

            return NoContent();
        }
    }
}