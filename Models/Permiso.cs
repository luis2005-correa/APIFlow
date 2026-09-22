using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Permiso
{
    public long Id { get; set; }

    public string Clave { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
