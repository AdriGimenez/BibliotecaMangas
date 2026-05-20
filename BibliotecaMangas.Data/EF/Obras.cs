using System;
using System.Collections.Generic;

namespace BibliotecaMangas.Data.EF;

public partial class Obras
{
    public int ObraId { get; set; }

    public string Titulo { get; set; } = null!;

    public int? AutorId { get; set; }

    public int? EditorialId { get; set; }

    public virtual Autores? Autor { get; set; }

    public virtual Editoriales? Editorial { get; set; }

    public virtual ICollection<Tomos> Tomos { get; set; } = new List<Tomos>();
}
