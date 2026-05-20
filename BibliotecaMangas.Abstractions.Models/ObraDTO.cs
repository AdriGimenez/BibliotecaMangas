using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMangas.Abstractions.Models
{
    public class ObraDTO
    {
        public string Titulo { get; set; } = null!;
        public int? AutorId { get; set; }
        public int? EditorialId { get; set; }
    }
}