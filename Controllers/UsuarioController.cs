using AcademiaFlowAPI.Dto;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var usuario = await _usuarioService.GetById(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });
            }
            return Ok(usuario);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var usuario = await _usuarioService.GetByEmail(email);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con email '{email}'." });
            }
            return Ok(usuario);
        }

        [HttpGet("documento/{numeroDocumento}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDocumento(string numeroDocumento)
        {
            var usuario = await _usuarioService.GetByDocumento(numeroDocumento);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con número de documento '{numeroDocumento}'." });
            }
            return Ok(usuario);
        }

        [HttpGet("institucion/{idInstitucion:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioViewModel>))]
        public async Task<IActionResult> GetByInstitucionId(long idInstitucion)
        {
            var usuarios = await _usuarioService.GetByInstitucionId(idInstitucion);
            return Ok(usuarios);
        }

        [HttpGet("unidad-academica/{idUnidadAcademica:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioViewModel>))]
        public async Task<IActionResult> GetByUnidadAcademicaId(long idUnidadAcademica)
        {
            var usuarios = await _usuarioService.GetByUnidadAcademicaId(idUnidadAcademica);
            return Ok(usuarios);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoUsuario = await _usuarioService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Id }, nuevoUsuario);
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
        public async Task<IActionResult> Update(long id, [FromBody] UsuarioViewModel model)
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
                var resultado = await _usuarioService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró el usuario con ID {id} para actualizar." });
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

            var resultado = await _usuarioService.CambiarEstadoActivo(id, dto.Activo);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });
            }

            return NoContent();
        }

        [HttpPost("{id:long}/ultimo-acceso")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarUltimoAcceso(long id)
        {
            var resultado = await _usuarioService.RegistrarUltimoAcceso(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _usuarioService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el usuario con ID {id}." });
            }

            return NoContent();
        }
    }


}