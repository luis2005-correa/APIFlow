using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Archivo
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public long? IdFase { get; set; }

    public long? IdTarea { get; set; }

    public string? Tipo { get; set; }

    public string NombreOriginal { get; set; } = null!;

    public string StorageKey { get; set; } = null!;

    public string? Extension { get; set; }

    public long? TamanoBytes { get; set; }

    public string? HashSha256 { get; set; }

    public short? Version { get; set; }

    public long? IdArchivoPadre { get; set; }

    public bool? EsVigente { get; set; }

    public string? Descripcion { get; set; }

    public long? SubidoPor { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual Archivo? IdArchivoPadreNavigation { get; set; }

    public virtual Fase? IdFaseNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Tarea? IdTareaNavigation { get; set; }

    public virtual ICollection<Archivo> InverseIdArchivoPadreNavigation { get; set; } = new List<Archivo>();

    public virtual Usuario? SubidoPorNavigation { get; set; }
}
