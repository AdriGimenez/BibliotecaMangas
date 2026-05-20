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
    public class TomosRepository(BibliotecaContext _context) : ITomosRepository
    {
        public async Task<List<TomoDTO>> GetAll()
        {
            return await _context.Tomos
                .Select(t => new TomoDTO
                {
                    ObraId = t.ObraId,
                    NumeroTomo = t.NumeroTomo
                }).ToListAsync();
        }

        public Task<TomoDTO?> GetById(int id) => throw new NotImplementedException();
        public Task<bool> Save(TomoDTO tomo) => throw new NotImplementedException();
        public Task<bool> Update(int id, TomoDTO tomo) => throw new NotImplementedException();
        public Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
