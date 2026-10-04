namespace AcademiaFlowAPI.Dto
{
    public class VerificarEmailDto
    {
        public string Email { get; set; } = null!;
    }

    public class RestablecerPasswordDto
    {
        public string Email { get; set; } = null!;
        public string NuevaPassword { get; set; } = null!;
    }
}
