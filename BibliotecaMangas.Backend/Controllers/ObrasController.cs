using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaMangas.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController(IObrasRepository _obrasRepository) : ControllerBase
    {
        [HttpGet("getAllObras")]
        public async Task<List<ObraDTO>> GetAllObras()
        {
            return await _obrasRepository.GetAll();
        }

        [HttpGet("getObras")]
        [ProducesResponseType(typeof(ObraDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ObraDTO>> GetObraById(int id)
        {
            var obra = await _obrasRepository.GetById(id);

            if (obra == null)
            {
                return NotFound();
            }
            return Ok(obra);
        }

        [HttpGet("getObraDetalle/{id}")]
        [ProducesResponseType(typeof(ObraDetalleDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ObraDetalleDTO>> GetObraDetalleById(int id)
        {
            var obraDetalle = await _obrasRepository.GetDetalleById(id);

            if (obraDetalle == null)
            {
                return NotFound();
            }
            return Ok(obraDetalle);
        }
    }
}
