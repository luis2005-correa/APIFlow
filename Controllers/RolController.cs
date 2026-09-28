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
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RolViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var rol = await _rolService.GetById(id);
            if (rol == null)
            {
                return NotFound(new { mensaje = $"No se encontró el rol con ID {id}." });
            }
            return Ok(rol);
        }

        [HttpGet("nombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNombre(string nombre)
        {
            var rol = await _rolService.GetByNombre(nombre);
            if (rol == null)
            {
                return NotFound(new { mensaje = $"No se encontró el rol con el nombre '{nombre}'." });
            }
            return Ok(rol);
        }

        [HttpGet("sistema")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RolViewModel>))]
        public async Task<IActionResult> GetRolesSistema()
        {
            var roles = await _rolService.GetRolesSistema();
            return Ok(roles);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RolViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoRol = await _rolService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoRol.Id }, nuevoRol);
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
        public async Task<IActionResult> Update(long id, [FromBody] RolViewModel model)
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
                var resultado = await _rolService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el rol con ID {id}." });
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var resultado = await _rolService.Delete(id);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No existe el rol con ID {id}." });
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
    }
}