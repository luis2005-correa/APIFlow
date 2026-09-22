using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ApiLog
{
    public long Id { get; set; }

    public long? IdCliente { get; set; }

    public long? IdUsuario { get; set; }

    public string Metodo { get; set; } = null!;

    public string Ruta { get; set; } = null!;

    public short StatusCode { get; set; }

    public int? DuracionMs { get; set; }

    public string? Ip { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual ApiCliente? IdClienteNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
