using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class RegistroAvance
{
    public long Id { get; set; }

    public long? IdTarea { get; set; }

    public long? IdActividad { get; set; }

    public long IdUsuario { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal? Porcentaje { get; set; }

    public decimal HorasDedicadas { get; set; }

    public DateTimeOffset? FechaReporte { get; set; }

    public virtual Actividad? IdActividadNavigation { get; set; }

    public virtual Tarea? IdTareaNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
