using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Rol
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Sistema { get; set; }

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();

    public virtual ICollection<Permiso> IdPermisos { get; set; } = new List<Permiso>();
}
