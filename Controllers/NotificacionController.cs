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
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var notificaciones = await _notificacionService.GetAll();
            return Ok(notificaciones);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificacionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var notificacion = await _notificacionService.GetById(id);
            if (notificacion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la notificación con ID {id}." });
            }
            return Ok(notificacion);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var notificaciones = await _notificacionService.GetByUsuarioId(idUsuario);
            return Ok(notificaciones);
        }

        [HttpGet("usuario/{idUsuario:long}/no-leidas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionViewModel>))]
        public async Task<IActionResult> GetNoLeidasByUsuarioId(long idUsuario)
        {
            var notificaciones = await _notificacionService.GetNoLeidasByUsuarioId(idUsuario);
            return Ok(notificaciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotificacionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] NotificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaNotificacion = await _notificacionService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevaNotificacion.Id }, nuevaNotificacion);
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
        public async Task<IActionResult> Update(long id, [FromBody] NotificacionViewModel model)
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
                var resultado = await _notificacionService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe la notificación con ID {id}." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id:long}/marcar-leida")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarcarComoLeida(long id)
        {
            var resultado = await _notificacionService.MarcarComoLeida(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la notificación con ID {id}." });
            }

            return NoContent();
        }

        [HttpPatch("usuario/{idUsuario:long}/marcar-todas-leidas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarcarTodasComoLeidas(long idUsuario)
        {
            var resultado = await _notificacionService.MarcarTodasComoLeidas(idUsuario);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontraron notificaciones pendientes por leer para el usuario con ID {idUsuario}." });
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _notificacionService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la notificación con ID {id}." });
            }

            return NoContent();
        }
    }
}