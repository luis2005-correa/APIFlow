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
    public class ProyectoEstadoHistorialController : ControllerBase
    {
        private readonly IProyectoEstadoHistorialService _proyectoEstadoHistorialService;

        public ProyectoEstadoHistorialController(IProyectoEstadoHistorialService proyectoEstadoHistorialService)
        {
            _proyectoEstadoHistorialService = proyectoEstadoHistorialService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoEstadoHistorialViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var historial = await _proyectoEstadoHistorialService.GetAll();
            return Ok(historial);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoEstadoHistorialViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var registro = await _proyectoEstadoHistorialService.GetById(id);
            if (registro == null)
            {
                return NotFound(new { mensaje = $"No se encontró el registro de historial con ID {id}." });
            }
            return Ok(registro);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoEstadoHistorialViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var historial = await _proyectoEstadoHistorialService.GetByProyectoId(idProyecto);
            return Ok(historial);
        }

        [HttpGet("proyecto/{idProyecto:long}/ultimo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoEstadoHistorialViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUltimoCambioByProyectoId(long idProyecto)
        {
            var ultimoCambio = await _proyectoEstadoHistorialService.GetUltimoCambioByProyectoId(idProyecto);
            if (ultimoCambio == null)
            {
                return NotFound(new { mensaje = $"No existe historial de cambios de estado para el proyecto con ID {idProyecto}." });
            }
            return Ok(ultimoCambio);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProyectoEstadoHistorialViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProyectoEstadoHistorialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoRegistro = await _proyectoEstadoHistorialService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoRegistro.Id }, nuevoRegistro);
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
        public async Task<IActionResult> Update(long id, [FromBody] ProyectoEstadoHistorialViewModel model)
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
                var resultado = await _proyectoEstadoHistorialService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el registro de historial con ID {id}." });
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
            var resultado = await _proyectoEstadoHistorialService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el registro de historial con ID {id}." });
            }

            return NoContent();
        }
    }
}