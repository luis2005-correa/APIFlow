using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Notificacion
{
    public long Id { get; set; }

    public long IdUsuario { get; set; }

    public long? IdProyecto { get; set; }

    public string? Tipo { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public string? Enlace { get; set; }

    public bool? Leida { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
