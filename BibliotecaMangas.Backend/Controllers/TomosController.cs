using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
