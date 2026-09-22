using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class UnidadAcademica
{
    public long Id { get; set; }

    public long IdInstitucion { get; set; }

    public long? IdPadre { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Tipo { get; set; }

    public bool? Activo { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual Institucion IdInstitucionNavigation { get; set; } = null!;

    public virtual UnidadAcademica? IdPadreNavigation { get; set; }

    public virtual ICollection<UnidadAcademica> InverseIdPadreNavigation { get; set; } = new List<UnidadAcademica>();

    public virtual ICollection<ProgramaAcademico> ProgramaAcademicos { get; set; } = new List<ProgramaAcademico>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
