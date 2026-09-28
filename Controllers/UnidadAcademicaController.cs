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
    public class UnidadAcademicaController : ControllerBase
    {
        private readonly IUnidadAcademicaService _unidadAcademicaService;

        public UnidadAcademicaController(IUnidadAcademicaService unidadAcademicaService)
        {
            _unidadAcademicaService = unidadAcademicaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UnidadAcademicaViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var unidades = await _unidadAcademicaService.GetAll();
            return Ok(unidades);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UnidadAcademicaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var unidad = await _unidadAcademicaService.GetById(id);
            if (unidad == null)
            {
                return NotFound(new { mensaje = $"No se encontró la unidad académica con ID {id}." });
            }
            return Ok(unidad);
        }

        [HttpGet("institucion/{idInstitucion:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UnidadAcademicaViewModel>))]
        public async Task<IActionResult> GetByInstitucionId(long idInstitucion)
        {
            var unidades = await _unidadAcademicaService.GetByInstitucionId(idInstitucion);
            return Ok(unidades);
        }

        [HttpGet("{idPadre:long}/hijos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UnidadAcademicaViewModel>))]
        public async Task<IActionResult> GetHijos(long idPadre)
        {
            var unidades = await _unidadAcademicaService.GetHijos(idPadre);
            return Ok(unidades);
        }

        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UnidadAcademicaViewModel>))]
        public async Task<IActionResult> GetByTipo(string tipo)
        {
            var unidades = await _unidadAcademicaService.GetByTipo(tipo);
            return Ok(unidades);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UnidadAcademicaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UnidadAcademicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaUnidad = await _unidadAcademicaService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaUnidad.Id }, nuevaUnidad);
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
        public async Task<IActionResult> Update(long id, [FromBody] UnidadAcademicaViewModel model)
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
                var resultado = await _unidadAcademicaService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró la unidad académica con ID {id} para actualizar." });
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

        [HttpPatch("{id:long}/estado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CambiarEstadoActivo(long id, [FromBody] CambiarEstadoActivoDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { mensaje = "El estado 'activo' es requerido." });
            }

            var resultado = await _unidadAcademicaService.CambiarEstadoActivo(id, dto.Activo);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la unidad académica con ID {id}." });
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _unidadAcademicaService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la unidad académica con ID {id}." });
            }

            return NoContent();
        }
    }


}