using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class UsuarioRol
{
    public long IdUsuario { get; set; }

    public long IdRol { get; set; }

    public DateTimeOffset? AsignadoEn { get; set; }

    public long? AsignadoPor { get; set; }

    public virtual Usuario? AsignadoPorNavigation { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
