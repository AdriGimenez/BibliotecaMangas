using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Models;

namespace BibliotecaMangas.Abstractions.Interfaces
{
    public interface IObrasRepository
    {
        Task<List<ObraDTO>> GetAll();
        Task<ObraDTO?> GetById(int id);
        Task<bool> Save(ObraDTO obra);
        Task<bool> Update(int id, ObraDTO obra);
        Task<bool> Delete(int id);
    }
}
