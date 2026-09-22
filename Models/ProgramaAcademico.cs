using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ProgramaAcademico
{
    public long Id { get; set; }

    public long IdUnidadAcademica { get; set; }

    public long? IdDisciplina { get; set; }

    public string Nombre { get; set; } = null!;

    public string? CodigoSnies { get; set; }

    public string? Nivel { get; set; }

    public bool? Activo { get; set; }

    public virtual Disciplina? IdDisciplinaNavigation { get; set; }

    public virtual UnidadAcademica IdUnidadAcademicaNavigation { get; set; } = null!;

    public virtual ICollection<Proyecto> IdProyectos { get; set; } = new List<Proyecto>();
}
