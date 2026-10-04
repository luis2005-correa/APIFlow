namespace AcademiaFlowAPI.Dto
{
    public class RegistroDto
    {
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Cargo { get; set; }
    }
}
