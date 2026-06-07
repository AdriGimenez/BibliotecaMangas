using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaMangas.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController(IAutoresRepository _autoresRepository) : ControllerBase
    {
        [HttpGet("getAllAutores")]
        public async Task<List<AutorDTO>> GetAllAutores()
        {
            return await _autoresRepository.GetAll();
        }

        [HttpGet("getAutor/{id}")]
        [ProducesResponseType(typeof(AutorDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AutorDTO>> GetAutorById(int id)
        {
            var autor = await _autoresRepository.GetById(id);

            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost("createAutor")]
        [ProducesResponseType(typeof(AutorDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AutorDTO>> CreateAutor([FromBody] AutorDTO autor)
        {
            if (autor == null || string.IsNullOrWhiteSpace(autor.Nombre))
            {
                return BadRequest("El nombre del autor es obligatorio.");
            }

            var creado = await _autoresRepository.Create(autor);

            if (!creado)
            {
                return BadRequest("No se puede crear el autor o ya existe.");
            }

            return Created("", autor);
        }

        [HttpPut("updateAutor/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAutor(int id,  [FromBody] AutorDTO autor)
        {
            if(autor == null || string.IsNullOrWhiteSpace(autor.Nombre))
            {
                return BadRequest("El nombre del autor es obligatorio.");
            }

            var actualizado = await _autoresRepository.Update(id, autor);

            if (!actualizado)
            {
                return NotFound("No se encontró el autor o ya existe otro autor con ese nombre");
            }
            return NoContent();
        }

        [HttpDelete("deleteAutor/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var eliminado = await _autoresRepository.Delete(id);

            if (!eliminado)
            {
                return NotFound("No se encontró el autor.");
            }
            return NoContent();
        }
    }
}
