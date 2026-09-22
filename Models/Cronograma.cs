using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Cronograma
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public short? Version { get; set; }

    public string? Formato { get; set; }

    public string? Datos { get; set; }

    public string? Observaciones { get; set; }

    public long? SubidoPor { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Usuario? SubidoPorNavigation { get; set; }
}
