using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaMangas.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EditorialesController(IEditorialesRepository _editorialesRepository) : ControllerBase
    {
        [HttpGet("getAllEditoriales")]
        public async Task<List<EditorialDTO>> GetAllEditoriales()
        {
            return await _editorialesRepository.GetAll();
        }

        [HttpGet("getEditorial/{id}")]
        [ProducesResponseType(typeof(EditorialDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EditorialDTO>> GetEditorialById(int id)
        {
            var editorial = await _editorialesRepository.GetById(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return Ok(editorial);
        }

        [HttpPost("createEditorial")]
        [ProducesResponseType(typeof(EditorialDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<EditorialDTO>> CreateEditorial([FromBody] EditorialDTO editorial)
        {
            if (editorial == null ||
               string.IsNullOrWhiteSpace(editorial.Nombre) ||
               string.IsNullOrWhiteSpace(editorial.Pais))
            {
                return BadRequest("El nombre y el país de la editorial son obligatorios.");
            }
            var creado = await _editorialesRepository.Create(editorial);
            if (!creado)
            {
                return BadRequest("No se pudo crear la editorial o ya existe.");
            }
            return Created("", editorial);
        }

        [HttpPut("updateEditorial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateEditorial(int id,  [FromBody] EditorialDTO editorial)
        {
            if (editorial == null ||
                string.IsNullOrWhiteSpace(editorial.Nombre) ||
                string.IsNullOrWhiteSpace(editorial.Pais))
            {
                return BadRequest("El nombre y el país de la editorial son obligatorios.");
            }

            var actualizado = await _editorialesRepository.Update(id, editorial);

            if(!actualizado)
            {
                return NotFound("No se encontró la editorial o ya existe otra editorial con ese nombre y país.");
            }

            return NoContent();
        }

        [HttpDelete("deleteEditorial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEditorial(int id)
        {
            var eliminado = await _editorialesRepository.Delete(id);

            if (!eliminado)
            {
                return NotFound("No se encontró la editorial");
            }

            return NoContent();
        }

    }
}
