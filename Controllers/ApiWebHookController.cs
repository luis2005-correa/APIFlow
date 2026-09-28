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
    public class ApiWebhookController : ControllerBase
    {
        private readonly IApiWebhookService _apiWebhookService;

        public ApiWebhookController(IApiWebhookService apiWebhookService)
        {
            _apiWebhookService = apiWebhookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiWebhookViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var webhooks = await _apiWebhookService.GetAll();
            return Ok(webhooks);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiWebhookViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var webhook = await _apiWebhookService.GetById(id);
            if (webhook == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Webhook con ID {id}." });
            }
            return Ok(webhook);
        }

        [HttpGet("cliente/{idCliente:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiWebhookViewModel>))]
        public async Task<IActionResult> GetByClienteId(long idCliente)
        {
            var webhooks = await _apiWebhookService.GetByClienteId(idCliente);
            return Ok(webhooks);
        }

        [HttpGet("evento/{evento}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiWebhookViewModel>))]
        public async Task<IActionResult> GetByEvento(string evento)
        {
            var webhooks = await _apiWebhookService.GetByEvento(evento);
            return Ok(webhooks);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiWebhookViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ApiWebhookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoWebhook = await _apiWebhookService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoWebhook.Id }, nuevoWebhook);
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
        public async Task<IActionResult> Update(long id, [FromBody] ApiWebhookViewModel model)
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
                var resultado = await _apiWebhookService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el Webhook con ID {id}." });
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
            var resultado = await _apiWebhookService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el Webhook con ID {id}." });
            }

            return NoContent();
        }
    }
}