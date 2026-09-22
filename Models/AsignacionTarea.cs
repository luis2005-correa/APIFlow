using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class AsignacionTarea
{
    public long IdTarea { get; set; }

    public long IdUsuario { get; set; }

    public decimal? HorasAsignadas { get; set; }

    public DateTimeOffset? AsignadoEn { get; set; }

    public long? AsignadoPor { get; set; }

    public virtual Usuario? AsignadoPorNavigation { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
