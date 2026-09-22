using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class AsignacionRecurso
{
    public long Id { get; set; }

    public long IdRecurso { get; set; }

    public long? IdFase { get; set; }

    public long? IdTarea { get; set; }

    public decimal Cantidad { get; set; }

    public string? Observacion { get; set; }

    public virtual Fase? IdFaseNavigation { get; set; }

    public virtual Recurso IdRecursoNavigation { get; set; } = null!;

    public virtual Tarea? IdTareaNavigation { get; set; }
}
