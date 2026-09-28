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
    public class ProyectoIntegranteController : ControllerBase
    {
        private readonly IProyectoIntegranteService _proyectoIntegranteService;

        public ProyectoIntegranteController(IProyectoIntegranteService proyectoIntegranteService)
        {
            _proyectoIntegranteService = proyectoIntegranteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoIntegranteViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var integrantes = await _proyectoIntegranteService.GetAll();
            return Ok(integrantes);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoIntegranteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var integrante = await _proyectoIntegranteService.GetById(id);
            if (integrante == null)
            {
                return NotFound(new { mensaje = $"No se encontró el integrante del proyecto con ID {id}." });
            }
            return Ok(integrante);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoIntegranteViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var integrantes = await _proyectoIntegranteService.GetByProyectoId(idProyecto);
            return Ok(integrantes);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoIntegranteViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var proyectos = await _proyectoIntegranteService.GetByUsuarioId(idUsuario);
            return Ok(proyectos);
        }

        [HttpGet("proyecto/{idProyecto:long}/activos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoIntegranteViewModel>))]
        public async Task<IActionResult> GetActivosByProyectoId(long idProyecto)
        {
            var integrantes = await _proyectoIntegranteService.GetActivosByProyectoId(idProyecto);
            return Ok(integrantes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProyectoIntegranteViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProyectoIntegranteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoIntegrante = await _proyectoIntegranteService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoIntegrante.Id }, nuevoIntegrante);
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
        public async Task<IActionResult> Update(long id, [FromBody] ProyectoIntegranteViewModel model)
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
                var resultado = await _proyectoIntegranteService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el integrante del proyecto con ID {id}." });
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
            var resultado = await _proyectoIntegranteService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el integrante del proyecto con ID {id}." });
            }

            return NoContent();
        }
    }
}