using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaMangas.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TomosController(ITomosRepository _tomosRepository) : ControllerBase
    {
        [HttpGet("getAllTomos")]
        public async Task<List<TomoDTO>> GetAllTomos()
        {
            return await _tomosRepository.GetAll();
        }

        [HttpGet("getTomo/{id}")]
        [ProducesResponseType(typeof(TomoDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TomoDTO>> GetTomoById(int id)
        {
            var tomo = await _tomosRepository.GetById(id);

            if (tomo == null)
            {
                return NotFound();
            }
            return Ok(tomo);
        }

        [HttpPost("createTomo")]
        [ProducesResponseType(typeof(TomoDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TomoDTO>> CreateTomo([FromBody] TomoDTO tomo)
        {
            if (tomo == null ||
                tomo.ObraId == null ||
                tomo.NumeroTomo == null ||
                tomo.NumeroTomo <= 0)
            {
                return BadRequest("La obra y el número de tomo son obligatorios.");
            }

            var creado = await _tomosRepository.Create(tomo);

            if (!creado)
            {
                return BadRequest("No se pudo crear el tomo. Verificá que la obra exista y que el tomo no esté repetido.");
            }
            return Created("", tomo);
        }

        [HttpPut("updateTomo/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTomo(int id,  [FromBody] TomoDTO tomo)
        {
            if (tomo == null ||
                tomo.ObraId == null ||
                tomo.NumeroTomo == null ||
                tomo.NumeroTomo <= 0)
            {
                return BadRequest("La obra y el número de tomo son obligatorios.");
            }

            var actualizado = await _tomosRepository.Update(id, tomo);

            if (!actualizado)
            {
                return NotFound("No se encontró el tomo, la obra no existe o el tomo ya está cargado.");
            }

            return NoContent();
        }

        [HttpDelete("deleteTomo/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTomo(int id)
        {
            var eliminado = await _tomosRepository.Delete(id);

            if(!eliminado)
            {
                return NotFound("No se encontró el tomo.");
            }

            return NoContent();
        }

    }
}
