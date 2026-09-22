using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Fase
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public short? Orden { get; set; }

    public decimal? Peso { get; set; }

    public DateOnly? FechaInicioPlan { get; set; }

    public DateOnly? FechaFinPlan { get; set; }

    public DateOnly? FechaInicioReal { get; set; }

    public DateOnly? FechaFinReal { get; set; }

    public decimal? PorcentajeAvance { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

    public virtual ICollection<AsignacionRecurso> AsignacionRecursos { get; set; } = new List<AsignacionRecurso>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();

    public virtual ICollection<Verificacion> Verificacions { get; set; } = new List<Verificacion>();
}
