using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ProyectoEstadoHistorial
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public string? EstadoAnterior { get; set; }

    public string EstadoNuevo { get; set; } = null!;

    public string? Justificacion { get; set; }

    public string? Observaciones { get; set; }

    public long? CambiadoPor { get; set; }

    public long? IdResponsableVerificacion { get; set; }

    public DateTimeOffset? FechaCambio { get; set; }

    public virtual Usuario? CambiadoPorNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Usuario? IdResponsableVerificacionNavigation { get; set; }
}
