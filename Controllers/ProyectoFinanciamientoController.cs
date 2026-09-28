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
    public class ProyectoFinanciamientoController : ControllerBase
    {
        private readonly IProyectoFinanciamientoService _proyectoFinanciamientoService;

        public ProyectoFinanciamientoController(IProyectoFinanciamientoService proyectoFinanciamientoService)
        {
            _proyectoFinanciamientoService = proyectoFinanciamientoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoFinanciamientoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var financiamientos = await _proyectoFinanciamientoService.GetAll();
            return Ok(financiamientos);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoFinanciamientoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var financiamiento = await _proyectoFinanciamientoService.GetById(id);
            if (financiamiento == null)
            {
                return NotFound(new { mensaje = $"No se encontró el registro de financiamiento con ID {id}." });
            }
            return Ok(financiamiento);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoFinanciamientoViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var financiamientos = await _proyectoFinanciamientoService.GetByProyectoId(idProyecto);
            return Ok(financiamientos);
        }

        [HttpGet("entidad/{idEntidad:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoFinanciamientoViewModel>))]
        public async Task<IActionResult> GetByEntidadId(long idEntidad)
        {
            var financiamientos = await _proyectoFinanciamientoService.GetByEntidadId(idEntidad);
            return Ok(financiamientos);
        }

        [HttpGet("proyecto/{idProyecto:long}/monto-total")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        public async Task<IActionResult> GetMontoTotalByProyectoId(long idProyecto)
        {
            var montoTotal = await _proyectoFinanciamientoService.GetMontoTotalByProyectoId(idProyecto);
            return Ok(montoTotal);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProyectoFinanciamientoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProyectoFinanciamientoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoFinanciamiento = await _proyectoFinanciamientoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoFinanciamiento.Id }, nuevoFinanciamiento);
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
        public async Task<IActionResult> Update(long id, [FromBody] ProyectoFinanciamientoViewModel model)
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
                var resultado = await _proyectoFinanciamientoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el registro de financiamiento con ID {id}." });
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
            var resultado = await _proyectoFinanciamientoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el registro de financiamiento con ID {id}." });
            }

            return NoContent();
        }
    }
}