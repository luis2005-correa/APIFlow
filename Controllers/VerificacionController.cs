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
    public class VerificacionController : ControllerBase
    {
        private readonly IVerificacionService _verificacionService;

        public VerificacionController(IVerificacionService verificacionService)
        {
            _verificacionService = verificacionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VerificacionViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var verificaciones = await _verificacionService.GetAll();
            return Ok(verificaciones);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VerificacionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var verificacion = await _verificacionService.GetById(id);
            if (verificacion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la verificación con ID {id}." });
            }
            return Ok(verificacion);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VerificacionViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var verificaciones = await _verificacionService.GetByProyectoId(idProyecto);
            return Ok(verificaciones);
        }

        [HttpGet("fase/{idFase:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VerificacionViewModel>))]
        public async Task<IActionResult> GetByFaseId(long idFase)
        {
            var verificaciones = await _verificacionService.GetByFaseId(idFase);
            return Ok(verificaciones);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VerificacionViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var verificaciones = await _verificacionService.GetByTareaId(idTarea);
            return Ok(verificaciones);
        }

        [HttpGet("asignado-por/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VerificacionViewModel>))]
        public async Task<IActionResult> GetByAsignadoPorId(long idUsuario)
        {
            var verificaciones = await _verificacionService.GetByAsignadoPorId(idUsuario);
            return Ok(verificaciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VerificacionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] VerificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaVerificacion = await _verificacionService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaVerificacion.Id }, nuevaVerificacion);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
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
        public async Task<IActionResult> Update(long id, [FromBody] VerificacionViewModel model)
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
                var resultado = await _verificacionService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró la verificación con ID {id} para actualizar." });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id:long}/resultado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarResultado(long id, [FromBody] RegistrarResultadoDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Resultado))
            {
                return BadRequest(new { mensaje = "El campo 'resultado' es obligatorio." });
            }

            var resultado = await _verificacionService.RegistrarResultado(id, dto.Resultado, dto.Observaciones);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la verificación con ID {id}." });
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _verificacionService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la verificación con ID {id}." });
            }

            return NoContent();
        }
    }


}