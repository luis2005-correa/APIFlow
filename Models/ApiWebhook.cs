using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ApiWebhook
{
    public long Id { get; set; }

    public long IdCliente { get; set; }

    public string Evento { get; set; } = null!;

    public string Secreto { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ApiCliente IdClienteNavigation { get; set; } = null!;
}
