using Microsoft.AspNetCore.Mvc;
using AcademiaFlowAPI.Services.Interfaces;
using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiClientController : ControllerBase
    {
        private readonly IApiClienteservice _apiClienteService;

        public ApiClientController(IApiClienteservice apiClienteService)
        {
            _apiClienteService = apiClienteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiClienteViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _apiClienteService.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiClienteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cliente = await _apiClienteService.GetById(id);
            if (cliente == null)
            {
                return NotFound(new { mensaje = $"No se encontró el cliente API con ID {id}." });
            }
            return Ok(cliente);
        }

        [HttpGet("client-id/{clientId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiClienteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var cliente = await _apiClienteService.GetByClientId(clientId);
            if (cliente == null)
            {
                return NotFound(new { mensaje = $"No se encontró el cliente API con ClientId {clientId}." });
            }
            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiClienteViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ApiClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoCliente = await _apiClienteService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.Id }, nuevoCliente);
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
        public async Task<IActionResult> Update(long id, [FromBody] ApiClienteViewModel model)
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
                var resultado = await _apiClienteService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el cliente API con ID {id}." });
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
            var resultado = await _apiClienteService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el cliente API con ID {id}." });
            }

            return NoContent();
        }
    }
}