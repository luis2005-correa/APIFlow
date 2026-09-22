using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Actividad
{
    public long Id { get; set; }

    public long IdTarea { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public short? Orden { get; set; }

    public string? Estado { get; set; }

    public long? IdResponsable { get; set; }

    public DateOnly? FechaInicioPlan { get; set; }

    public DateOnly? FechaFinPlan { get; set; }

    public decimal? HorasEstimadas { get; set; }

    public decimal? HorasReales { get; set; }

    public decimal? PorcentajeAvance { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Usuario? IdResponsableNavigation { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;

    public virtual ICollection<RegistroAvance> RegistroAvances { get; set; } = new List<RegistroAvance>();
}
