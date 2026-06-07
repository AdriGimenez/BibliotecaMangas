using System;
using System.Collections.Generic;

namespace BibliotecaMangas.Data.EF;

public partial class Tomos
{
    public int TomoId { get; set; }

    public int? ObraId { get; set; }

    public int NumeroTomo { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Obras? Obra { get; set; }
}
