using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ProyectoFinanciamiento
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public long IdEntidad { get; set; }

    public decimal Monto { get; set; }

    public string? Moneda { get; set; }

    public string? NumeroConvenio { get; set; }

    public DateOnly? FechaAprobacion { get; set; }

    public string? Observaciones { get; set; }

    public virtual EntidadFinanciadora IdEntidadNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
