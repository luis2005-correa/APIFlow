namespace AcademiaFlowAPI.Dto
{
    public class CambiarEstadoProyectoDto
    {
        public string NuevoEstado { get; set; } = string.Empty;
        public string? Justificacion { get; set; }
        public long? UsuarioId { get; set; }
    }
}
