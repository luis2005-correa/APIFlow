using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class EntidadFinanciadora
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Tipo { get; set; }

    public string? Nit { get; set; }

    public string? Pais { get; set; }

    public string? Contacto { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<ProyectoFinanciamiento> ProyectoFinanciamientos { get; set; } = new List<ProyectoFinanciamiento>();
}
