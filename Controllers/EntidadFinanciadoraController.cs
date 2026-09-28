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
    public class EntidadFinanciadoraController : ControllerBase
    {
        private readonly IEntidadFinanciadoraService _entidadFinanciadoraService;

        public EntidadFinanciadoraController(IEntidadFinanciadoraService entidadFinanciadoraService)
        {
            _entidadFinanciadoraService = entidadFinanciadoraService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntidadFinanciadoraViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var entidades = await _entidadFinanciadoraService.GetAll();
            return Ok(entidades);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntidadFinanciadoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var entidad = await _entidadFinanciadoraService.GetById(id);
            if (entidad == null)
            {
                return NotFound(new { mensaje = $"No se encontró la entidad financiadora con ID {id}." });
            }
            return Ok(entidad);
        }

        [HttpGet("activas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntidadFinanciadoraViewModel>))]
        public async Task<IActionResult> GetActivas()
        {
            var entidades = await _entidadFinanciadoraService.GetActivas();
            return Ok(entidades);
        }

        [HttpGet("nit/{nit}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntidadFinanciadoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNit(string nit)
        {
            var entidad = await _entidadFinanciadoraService.GetByNit(nit);
            if (entidad == null)
            {
                return NotFound(new { mensaje = $"No se encontró la entidad financiadora con NIT {nit}." });
            }
            return Ok(entidad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EntidadFinanciadoraViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EntidadFinanciadoraViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaEntidad = await _entidadFinanciadoraService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaEntidad.Id }, nuevaEntidad);
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
        public async Task<IActionResult> Update(long id, [FromBody] EntidadFinanciadoraViewModel model)
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
                var resultado = await _entidadFinanciadoraService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la entidad financiadora con ID {id}." });
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
            var resultado = await _entidadFinanciadoraService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la entidad financiadora con ID {id}." });
            }

            return NoContent();
        }
    }
}