using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Verificacion
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public long? IdFase { get; set; }

    public long? IdTarea { get; set; }

    public string? Tipo { get; set; }

    public long? AsignadoPor { get; set; }

    public string? Resultado { get; set; }

    public string? Observaciones { get; set; }

    public DateTimeOffset? FechaAsignacion { get; set; }

    public DateOnly? FechaLimite { get; set; }

    public DateTimeOffset? FechaVerificacion { get; set; }

    public virtual Usuario? AsignadoPorNavigation { get; set; }

    public virtual Fase? IdFaseNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Tarea? IdTareaNavigation { get; set; }
}
