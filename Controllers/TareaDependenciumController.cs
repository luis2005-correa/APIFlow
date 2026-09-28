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
    public class TareaDependenciumController : ControllerBase
    {
        private readonly ITareaDependenciumService _tareaDependenciumService;

        public TareaDependenciumController(ITareaDependenciumService tareaDependenciumService)
        {
            _tareaDependenciumService = tareaDependenciumService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaDependenciumViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var dependencias = await _tareaDependenciumService.GetAll();
            return Ok(dependencias);
        }

        [HttpGet("{idTarea:long}/{idDependeDe:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TareaDependenciumViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long idTarea, long idDependeDe)
        {
            var dependencia = await _tareaDependenciumService.GetById((idTarea, idDependeDe));
            if (dependencia == null)
            {
                return NotFound(new { mensaje = $"No se encontró la dependencia entre la tarea {idTarea} y la tarea {idDependeDe}." });
            }
            return Ok(dependencia);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaDependenciumViewModel>))]
        public async Task<IActionResult> GetDependenciasByTareaId(long idTarea)
        {
            var dependencias = await _tareaDependenciumService.GetDependenciasByTareaId(idTarea);
            return Ok(dependencias);
        }

        [HttpGet("depende-de/{idDependeDe:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TareaDependenciumViewModel>))]
        public async Task<IActionResult> GetTareasQueDependenDe(long idDependeDe)
        {
            var dependencias = await _tareaDependenciumService.GetTareasQueDependenDe(idDependeDe);
            return Ok(dependencias);
        }

        [HttpGet("validar-circularidad/{idTarea:long}/{idDependeDe:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> ValidarDependenciaCircular(long idTarea, long idDependeDe)
        {
            var esCircular = await _tareaDependenciumService.ExisteDependenciaCircular(idTarea, idDependeDe);
            return Ok(new { idTarea, idDependeDe, esCircular });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TareaDependenciumViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TareaDependenciumViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaDependencia = await _tareaDependenciumService.Create(model);
                return CreatedAtAction(
                    nameof(GetById),
                    new { idTarea = nuevaDependencia.IdTarea, idDependeDe = nuevaDependencia.IdDependeDe },
                    nuevaDependencia
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

        [HttpPut("{idTarea:long}/{idDependeDe:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long idTarea, long idDependeDe, [FromBody] TareaDependenciumViewModel model)
        {
            if (idTarea != model.IdTarea || idDependeDe != model.IdDependeDe)
            {
                return BadRequest(new { mensaje = "Las claves primarias de la URL no coinciden con las del modelo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _tareaDependenciumService.Update((idTarea, idDependeDe), model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se encontró la dependencia entre la tarea {idTarea} y la tarea {idDependeDe} para actualizar." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{idTarea:long}/{idDependeDe:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long idTarea, long idDependeDe)
        {
            var resultado = await _tareaDependenciumService.Delete((idTarea, idDependeDe));
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe la dependencia entre la tarea {idTarea} y la tarea {idDependeDe}." });
            }

            return NoContent();
        }
    }
}