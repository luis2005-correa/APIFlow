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
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;

        public ArchivoController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArchivoViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var archivos = await _archivoService.GetAll();
            return Ok(archivos);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArchivoViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var archivo = await _archivoService.GetById(id);
            if (archivo == null)
            {
                return NotFound(new { mensaje = $"No se encontró el archivo con ID {id}." });
            }
            return Ok(archivo);
        }

        [HttpGet("proyecto/{idProyecto:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArchivoViewModel>))]
        public async Task<IActionResult> GetByProyectoId(long idProyecto)
        {
            var archivos = await _archivoService.GetByProyectoId(idProyecto);
            return Ok(archivos);
        }

        [HttpGet("proyecto/{idProyecto:long}/vigentes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArchivoViewModel>))]
        public async Task<IActionResult> GetVigentesByProyectoId(long idProyecto)
        {
            var archivos = await _archivoService.GetVigentesByProyectoId(idProyecto);
            return Ok(archivos);
        }

        [HttpGet("tarea/{idTarea:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArchivoViewModel>))]
        public async Task<IActionResult> GetByTareaId(long idTarea)
        {
            var archivos = await _archivoService.GetByTareaId(idTarea);
            return Ok(archivos);
        }

        [HttpGet("fase/{idFase:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArchivoViewModel>))]
        public async Task<IActionResult> GetByFaseId(long idFase)
        {
            var archivos = await _archivoService.GetByFaseId(idFase);
            return Ok(archivos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ArchivoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ArchivoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoArchivo = await _archivoService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = nuevoArchivo.Id }, nuevoArchivo);
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
        public async Task<IActionResult> Update(long id, [FromBody] ArchivoViewModel model)
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
                var resultado = await _archivoService.Update(id, model);
                if (!resultado)
                {
                    return NotFound(new { mensaje = $"No se pudo actualizar. No existe el archivo con ID {id}." });
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
            var resultado = await _archivoService.Delete(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = $"No existe el archivo con ID {id}." });
            }

            return NoContent();
        }
    }
}