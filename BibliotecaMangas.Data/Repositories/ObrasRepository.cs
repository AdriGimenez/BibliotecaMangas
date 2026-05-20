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
    public class ObrasRepository(BibliotecaContext _context) : IObrasRepository
    {
        public async Task<List<ObraDTO>> GetAll()
        {
            return await _context.Obras
                .Select(o => new ObraDTO
                {
                    Titulo = o.Titulo,
                    AutorId = o.AutorId,
                    EditorialId = o.EditorialId
                }).ToListAsync();
        }
        public Task<ObraDTO?> GetById(int id) => throw new NotImplementedException();
        public Task<bool> Save(ObraDTO obra) => throw new NotImplementedException();
        public Task<bool> Update(int id, ObraDTO obra) => throw new NotImplementedException();
        public Task<bool> Delete(int id) => throw new NotImplementedException();

    }
}
