using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMangas.Abstractions.Models
{
    public class ObraDetalleDTO
    {
        public string Titulo { get; set; } = null!;
        public string? Autor { get; set; }
        public string? Editorial { get; set; }
        public List<int> Tomos { get; set; } = new();
    }
}
