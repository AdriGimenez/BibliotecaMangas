using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Models;

namespace BibliotecaMangas.Abstractions.Interfaces
{
    public interface ITomosRepository
    {
        Task<List<TomoDTO>> GetAll();
        Task<TomoDTO?> GetById(int id);
        Task<bool> Create(TomoDTO tomo);
        Task<bool> Update(int id, TomoDTO tomo);
        Task<bool> Delete(int id);
    }
}
