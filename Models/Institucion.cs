using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Institucion
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Siglas { get; set; }

    public string? Nit { get; set; }

    public string? Naturaleza { get; set; }

    public string? Pais { get; set; }

    public string? Ciudad { get; set; }

    public string? SitioWeb { get; set; }

    public bool? Activo { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<UnidadAcademica> UnidadAcademicas { get; set; } = new List<UnidadAcademica>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
