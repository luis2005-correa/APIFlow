using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Tarea
{
    public long Id { get; set; }

    public long IdFase { get; set; }

    public long IdProyecto { get; set; }

    public string Descripcion { get; set; } = null!;

    public short? Orden { get; set; }

    public string? Prioridad { get; set; }

    public string? Estado { get; set; }

    public long? IdResponsable { get; set; }

    public DateOnly? FechaInicioPlan { get; set; }

    public DateOnly? FechaFinPlan { get; set; }

    public DateOnly? FechaInicioReal { get; set; }

    public DateOnly? FechaFinReal { get; set; }

    public decimal? HorasEstimadas { get; set; }

    public decimal? PorcentajeAvance { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual ICollection<Actividad> Actividads { get; set; } = new List<Actividad>();

    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

    public virtual ICollection<AsignacionRecurso> AsignacionRecursos { get; set; } = new List<AsignacionRecurso>();

    public virtual ICollection<AsignacionTarea> AsignacionTareas { get; set; } = new List<AsignacionTarea>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Fase IdFaseNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Usuario? IdResponsableNavigation { get; set; }

    public virtual ICollection<RegistroAvance> RegistroAvances { get; set; } = new List<RegistroAvance>();

    public virtual ICollection<TareaDependencium> TareaDependenciumIdDependeDeNavigations { get; set; } = new List<TareaDependencium>();

    public virtual ICollection<TareaDependencium> TareaDependenciumIdTareaNavigations { get; set; } = new List<TareaDependencium>();

    public virtual ICollection<Verificacion> Verificacions { get; set; } = new List<Verificacion>();
}
