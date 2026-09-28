using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;
using AcademiaFlowAPI.Dto;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;

        public ProyectoController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var proyectos = await _proyectoService.GetAll();
            return Ok(proyectos);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var proyecto = await _proyectoService.GetById(id);
            if (proyecto == null)
            {
                return NotFound(new { mensaje = $"No se encontró el proyecto con ID {id}." });
            }
            return Ok(proyecto);
        }

        [HttpGet("codigo/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProyectoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var proyecto = await _proyectoService.GetByCodigo(codigo);
            if (proyecto == null)
            {
                return NotFound(new { mensaje = $"No se encontró el proyecto con código '{codigo}'." });
            }
            return Ok(proyecto);
        }

        [HttpGet("estado/{estado}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoViewModel>))]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            var proyectos = await _proyectoService.GetByEstado(estado);
            return Ok(proyectos);
        }

        [HttpGet("lider/{idLider:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoViewModel>))]
        public async Task<IActionResult> GetByLiderId(long idLider)
        {
            var proyectos = await _proyectoService.GetByLiderId(idLider);
            return Ok(proyectos);
        }

        [HttpGet("institucion/{idInstitucion:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProyectoViewModel>))]
        public async Task<IActionResult> GetByInstitucionId(long idInstitucion)
        {
            var proyectos = await _proyectoService.GetByInstitucionId(idInstitucion);
            return Ok(proyectos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProyectoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProyectoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoProyecto = await _proyectoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoProyecto.Id }, nuevoProyecto);
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
        public async Task<IActionResult> Update(long id, [FromBody] ProyectoViewModel model)
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
                var resultado = await _proyectoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el proyecto con ID {id}." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id:long}/cambiar-estado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CambiarEstado(long id, [FromBody] CambiarEstadoProyectoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NuevoEstado))
            {
                return BadRequest(new { mensaje = "El nuevo estado es requerido." });
            }

            try
            {
                var resultado = await _proyectoService.CambiarEstado(id, dto.NuevoEstado, dto.Justificacion, dto.UsuarioId);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No existe el proyecto con ID {id}." });
                }

                return Ok(new { mensaje = "Estado actualizado exitosamente." });
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
            var resultado = await _proyectoService.SoftDelete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el proyecto con ID {id}." });
            }

            return NoContent();
        }
    }


}