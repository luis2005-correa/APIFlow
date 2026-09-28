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
    public class CronogramaController : ControllerBase
    {
        private readonly ICronogramaService _cronogramaService;

        public CronogramaController(ICronogramaService cronogramaService)
        {
            _cronogramaService = cronogramaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CronogramaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var cronogramas = await _cronogramaService.GetAll();
            return Ok(cronogramas);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CronogramaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cronograma = await _cronogramaService.GetById(id);
            if (cronograma == null)
            {
                return NotFound(new { mensaje = $"No se encontró el cronograma con ID {id}." });
            }
            return Ok(cronograma);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CronogramaViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var cronogramas = await _cronogramaService.GetByProyectoId(idProyecto);
            return Ok(cronogramas);
        }

        [HttpGet("proyecto/{idProyecto:long}/ultima-version")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CronogramaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUltimaVersionByProyectoId(long idProyecto)
        {
            var cronograma = await _cronogramaService.GetUltimaVersionByProyectoId(idProyecto);
            if (cronograma == null)
            {
                return NotFound(new { mensaje = $"No existe un cronograma para el proyecto con ID {idProyecto}." });
            }
            return Ok(cronograma);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CronogramaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CronogramaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoCronograma = await _cronogramaService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoCronograma.Id }, nuevoCronograma);
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
        public async Task<IActionResult> Update(long id, [FromBody] CronogramaViewModel model)
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
                var resultado = await _cronogramaService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el cronograma con ID {id}." });
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
            var resultado = await _cronogramaService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el cronograma con ID {id}." });
            }

            return NoContent();
        }
    }
}