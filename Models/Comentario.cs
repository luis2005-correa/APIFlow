using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Comentario
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public long? IdFase { get; set; }

    public long? IdTarea { get; set; }

    public long? IdActividad { get; set; }

    public long? IdComentarioPadre { get; set; }

    public long IdUsuario { get; set; }

    public string Contenido { get; set; } = null!;

    public string? Menciones { get; set; }

    public DateTimeOffset? EditadoEn { get; set; }

    public bool? Eliminado { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual Actividad? IdActividadNavigation { get; set; }

    public virtual Comentario? IdComentarioPadreNavigation { get; set; }

    public virtual Fase? IdFaseNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Tarea? IdTareaNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Comentario> InverseIdComentarioPadreNavigation { get; set; } = new List<Comentario>();
}
