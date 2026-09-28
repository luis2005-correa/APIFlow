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
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _permisoService;

        public PermisoController(IPermisoService permisoService)
        {
            _permisoService = permisoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var permisos = await _permisoService.GetAll();
            return Ok(permisos);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermisoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var permiso = await _permisoService.GetById(id);
            if (permiso == null)
            {
                return NotFound(new { mensaje = $"No se encontró el permiso con ID {id}." });
            }
            return Ok(permiso);
        }

        [HttpGet("clave/{clave}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermisoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByClave(string clave)
        {
            var permiso = await _permisoService.GetByClave(clave);
            if (permiso == null)
            {
                return NotFound(new { mensaje = $"No se encontró el permiso con clave '{clave}'." });
            }
            return Ok(permiso);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PermisoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PermisoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoPermiso = await _permisoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoPermiso.Id }, nuevoPermiso);
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
        public async Task<IActionResult> Update(long id, [FromBody] PermisoViewModel model)
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
                var resultado = await _permisoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el permiso con ID {id}." });
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
            var resultado = await _permisoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el permiso con ID {id}." });
            }

            return NoContent();
        }
    }
}