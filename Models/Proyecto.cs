using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Proyecto
{
    public long Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Justificacion { get; set; }

    public string? ObjetivoGeneral { get; set; }

    public string? Resumen { get; set; }

    public string? PalabrasClave { get; set; }

    public long IdInstitucion { get; set; }

    public long? IdUnidadAcademica { get; set; }

    public long IdLider { get; set; }

    public string? Estado { get; set; }

    public string? OrigenFinanciamiento { get; set; }

    public decimal? PresupuestoTotal { get; set; }

    public string? Moneda { get; set; }

    public DateOnly? FechaInicioPlan { get; set; }

    public DateOnly? FechaFinPlan { get; set; }

    public DateOnly? FechaInicioReal { get; set; }

    public DateOnly? FechaFinReal { get; set; }

    public decimal? PorcentajeAvance { get; set; }

    public string? JustificacionCancelacion { get; set; }

    public long? CreadoPor { get; set; }

    public long? ActualizadoPor { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public DateTimeOffset? EliminadoEn { get; set; }

    public virtual Usuario? ActualizadoPorNavigation { get; set; }

    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Usuario? CreadoPorNavigation { get; set; }

    public virtual ICollection<Cronograma> Cronogramas { get; set; } = new List<Cronograma>();

    public virtual ICollection<Fase> Fases { get; set; } = new List<Fase>();

    public virtual Institucion IdInstitucionNavigation { get; set; } = null!;

    public virtual Usuario IdLiderNavigation { get; set; } = null!;

    public virtual UnidadAcademica? IdUnidadAcademicaNavigation { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<ProyectoDisciplina> ProyectoDisciplinas { get; set; } = new List<ProyectoDisciplina>();

    public virtual ICollection<ProyectoEstadoHistorial> ProyectoEstadoHistorials { get; set; } = new List<ProyectoEstadoHistorial>();

    public virtual ICollection<ProyectoFinanciamiento> ProyectoFinanciamientos { get; set; } = new List<ProyectoFinanciamiento>();

    public virtual ICollection<ProyectoIntegrante> ProyectoIntegrantes { get; set; } = new List<ProyectoIntegrante>();

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();

    public virtual ICollection<Verificacion> Verificacions { get; set; } = new List<Verificacion>();

    public virtual ICollection<ProgramaAcademico> IdProgramas { get; set; } = new List<ProgramaAcademico>();
}
