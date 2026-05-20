using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using BibliotecaMangas.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMangas.Data.Repositories
{
    public class EditorialesRepository(BibliotecaContext _context) : IEditorialesRepository
    {
        public async Task<List<EditorialDTO>> GetAll()
        {
            return await _context.Editoriales
                .Select(e => new EditorialDTO
                {
                    Nombre = e.Nombre,
                    Pais = e.Pais
                }).ToListAsync();
        }

        public Task<EditorialDTO?> GetById(int id) => throw new NotImplementedException();
        public Task<bool> Save(EditorialDTO editorial) => throw new NotImplementedException();
        public Task<bool> Update(int id, EditorialDTO editorial) => throw new NotImplementedException();
        public Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
