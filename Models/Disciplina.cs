using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Disciplina
{
    public long Id { get; set; }

    public long? IdPadre { get; set; }

    public string? Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual Disciplina? IdPadreNavigation { get; set; }

    public virtual ICollection<Disciplina> InverseIdPadreNavigation { get; set; } = new List<Disciplina>();

    public virtual ICollection<ProgramaAcademico> ProgramaAcademicos { get; set; } = new List<ProgramaAcademico>();

    public virtual ICollection<ProyectoDisciplina> ProyectoDisciplinas { get; set; } = new List<ProyectoDisciplina>();

    public virtual ICollection<ProyectoIntegrante> ProyectoIntegrantes { get; set; } = new List<ProyectoIntegrante>();
}
