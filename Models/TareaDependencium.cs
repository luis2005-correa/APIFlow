using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class TareaDependencium
{
    public long IdTarea { get; set; }

    public long IdDependeDe { get; set; }

    public string? Tipo { get; set; }

    public short? DesfaseDias { get; set; }

    public virtual Tarea IdDependeDeNavigation { get; set; } = null!;

    public virtual Tarea IdTareaNavigation { get; set; } = null!;
}
