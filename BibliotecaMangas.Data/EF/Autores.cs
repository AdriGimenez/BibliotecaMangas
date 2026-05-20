using System;
using System.Collections.Generic;

namespace BibliotecaMangas.Data.EF;

public partial class Autores
{
    public int AutorId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Obras> Obras { get; set; } = new List<Obras>();
}
