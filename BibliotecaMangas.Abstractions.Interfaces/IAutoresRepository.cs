using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Models;

namespace BibliotecaMangas.Abstractions.Interfaces
{
    public interface IAutoresRepository
    {
        Task<List<AutorDTO>> GetAll();
        Task<AutorDTO?> GetById(int id);
        Task<bool> Create(AutorDTO autor);
        Task<bool> Update(int id, AutorDTO autor);
        Task<bool> Delete(int id);
    }
}
