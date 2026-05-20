using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
    }
}
