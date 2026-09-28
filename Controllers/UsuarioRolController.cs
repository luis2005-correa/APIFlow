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
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolService _usuarioRolService;

        public UsuarioRolController(IUsuarioRolService usuarioRolService)
        {
            _usuarioRolService = usuarioRolService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioRolViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var rolesUsuarios = await _usuarioRolService.GetAll();
            return Ok(rolesUsuarios);
        }

        [HttpGet("{idUsuario:long}/{idRol:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioRolViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long idUsuario, long idRol)
        {
            var usuarioRol = await _usuarioRolService.GetById((idUsuario, idRol));
            if (usuarioRol == null)
            {
                return NotFound(new { mensaje = $"No se encontró la relación del usuario {idUsuario} con el rol {idRol}." });
            }
            return Ok(usuarioRol);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioRolViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var roles = await _usuarioRolService.GetByUsuarioId(idUsuario);
            return Ok(roles);
        }

        [HttpGet("rol/{idRol:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioRolViewModel>))]
        public async Task<IActionResult> GetByRolId(long idRol)
        {
            var usuarios = await _usuarioRolService.GetByRolId(idRol);
            return Ok(usuarios);
        }

        [HttpGet("check/{idUsuario:long}/{idRol:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> UsuarioTieneRol(long idUsuario, long idRol)
        {
            var tieneRol = await _usuarioRolService.UsuarioTieneRol(idUsuario, idRol);
            return Ok(new { idUsuario, idRol, tieneRol });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioRolViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UsuarioRolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoUsuarioRol = await _usuarioRolService.Create(model);
                return CreatedAtAction(
                    nameof(GetById),
                    new { idUsuario = nuevoUsuarioRol.IdUsuario, idRol = nuevoUsuarioRol.IdRol },
                    nuevoUsuarioRol
                );
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

        [HttpPost("asignar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { mensaje = "Los datos para la asignación son requeridos." });
            }

            var resultado = await _usuarioRolService.AsignarRol(dto.IdUsuario, dto.IdRol, dto.AsignadoPor);
            if (!resultado)
            {
                return BadRequest(new { mensaje = "El usuario ya posee este rol o no se pudo realizar la asignación." });
            }

            return Ok(new { mensaje = "Rol asignado correctamente." });
        }

        [HttpPut("{idUsuario:long}/{idRol:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long idUsuario, long idRol, [FromBody] UsuarioRolViewModel model)
        {
            if (idUsuario != model.IdUsuario || idRol != model.IdRol)
            {
                return BadRequest(new { mensaje = "La clave compuesta de la URL no coincide con el modelo enviado." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _usuarioRolService.Update((idUsuario, idRol), model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = "No se encontró el registro para actualizar." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{idUsuario:long}/{idRol:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long idUsuario, long idRol)
        {
            var resultado = await _usuarioRolService.Delete((idUsuario, idRol));
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la asignación del rol {idRol} al usuario {idUsuario}." });
            }

            return NoContent();
        }
    }


}