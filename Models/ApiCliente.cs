using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class ApiCliente
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public Guid ClientId { get; set; }

    public string SecretHash { get; set; } = null!;

    public string? Scopes { get; set; }

    public int? RateLimitHora { get; set; }

    public bool? Activo { get; set; }

    public DateTimeOffset? ExpiraEn { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public virtual ICollection<ApiLog> ApiLogs { get; set; } = new List<ApiLog>();

    public virtual ICollection<ApiWebhook> ApiWebhooks { get; set; } = new List<ApiWebhook>();
}
