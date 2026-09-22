using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Recurso
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public string Tipo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? CostoUnitario { get; set; }

    public decimal? CostoTotal { get; set; }

    public virtual ICollection<AsignacionRecurso> AsignacionRecursos { get; set; } = new List<AsignacionRecurso>();

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
