using System;
using System.Collections.Generic;

namespace BibliotecaMangas.Data.EF;

public partial class Editoriales
{
    public int EditorialId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public virtual ICollection<Obras> Obras { get; set; } = new List<Obras>();
}
