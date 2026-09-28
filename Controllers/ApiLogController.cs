using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademiaFlowAPI.Services.Interfaces;
using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiLogController : ControllerBase
    {
        private readonly IApiLogService _apiLogService;

        public ApiLogController(IApiLogService apiLogService)
        {
            _apiLogService = apiLogService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiLogViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _apiLogService.GetAll();
            return Ok(logs);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiLogViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var log = await _apiLogService.GetById(id);
            if (log == null)
            {
                return NotFound(new { mensaje = $"No se encontró el registro de log con ID {id}." });
            }
            return Ok(log);
        }

        [HttpGet("cliente/{idCliente:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiLogViewModel>))]
        public async Task<IActionResult> GetByClienteId(long idCliente)
        {
            var logs = await _apiLogService.GetByClienteId(idCliente);
            return Ok(logs);
        }

        [HttpGet("usuario/{idUsuario:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiLogViewModel>))]
        public async Task<IActionResult> GetByUsuarioId(long idUsuario)
        {
            var logs = await _apiLogService.GetByUsuarioId(idUsuario);
            return Ok(logs);
        }

        [HttpGet("status/{statusCode:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApiLogViewModel>))]
        public async Task<IActionResult> GetByStatusCode(short statusCode)
        {
            var logs = await _apiLogService.GetByStatusCode(statusCode);
            return Ok(logs);
        }
    }
}