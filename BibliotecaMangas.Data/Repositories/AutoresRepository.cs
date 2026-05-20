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
    public class AutoresRepository(BibliotecaContext _context) : IAutoresRepository
    {
        public async Task<List<AutorDTO>> GetAll()
        {
            return await _context.Autores
                .Select(a => new AutorDTO
                {
                    Nombre = a.Nombre
                }).ToListAsync();
        }

        public Task<AutorDTO?> GetById(int id) => throw new NotImplementedException();
        public Task<bool> Save(AutorDTO autor) => throw new NotImplementedException();
        public Task<bool> Update(int id, AutorDTO autor) => throw new NotImplementedException();
        public Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
