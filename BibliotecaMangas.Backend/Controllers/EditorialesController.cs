using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BibliotecaMangas.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EditorialesController(IEditorialesRepository _editorialesRepository) : ControllerBase
    {
        [HttpGet("getAllAutores")]
        public async Task<List<EditorialDTO>> GetAllEditoriales()
        {
            return await _editorialesRepository.GetAll();
        }
    }
}
