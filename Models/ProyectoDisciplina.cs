using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ProyectoDisciplina
{
    public long IdProyecto { get; set; }

    public long IdDisciplina { get; set; }

    public bool? Principal { get; set; }

    public virtual Disciplina IdDisciplinaNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
