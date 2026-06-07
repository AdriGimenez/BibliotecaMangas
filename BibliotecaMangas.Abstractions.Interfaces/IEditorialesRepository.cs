using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Models;

namespace BibliotecaMangas.Abstractions.Interfaces
{
    public interface IEditorialesRepository
    {
        Task<List<EditorialDTO>> GetAll();
        Task<EditorialDTO?> GetById(int id);
        Task<bool> Create(EditorialDTO editorial);
        Task<bool> Update(int id, EditorialDTO editorial);
        Task<bool> Delete(int id);
    }
}
