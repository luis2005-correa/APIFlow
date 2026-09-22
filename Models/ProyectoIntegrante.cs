using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ProyectoIntegrante
{
    public long Id { get; set; }

    public long IdProyecto { get; set; }

    public long IdUsuario { get; set; }

    public string Rol { get; set; } = null!;

    public long? IdDisciplina { get; set; }

    public short? DedicacionHoras { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public DateOnly? FechaRetiro { get; set; }

    public bool? Activo { get; set; }

    public virtual Disciplina? IdDisciplinaNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
