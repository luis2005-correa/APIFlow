using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ApiClienteViewModel
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

        public static ApiClienteViewModel ToViewModel(ApiCliente ob)
        {
            return new ApiClienteViewModel()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                ClientId = ob.ClientId,
                SecretHash = ob.SecretHash,
                Scopes = ob.Scopes,
                RateLimitHora = ob.RateLimitHora,
                Activo = ob.Activo,
                ExpiraEn = ob.ExpiraEn,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static ApiCliente ToApiClientes(ApiClienteViewModel ob)
        {
            return new ApiCliente()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                ClientId = ob.ClientId,
                SecretHash = ob.SecretHash,
                Scopes = ob.Scopes,
                RateLimitHora = ob.RateLimitHora,
                Activo = ob.Activo,
                ExpiraEn = ob.ExpiraEn,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}
