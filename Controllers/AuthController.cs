using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Dto;
using AcademiaFlowAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GestionesAcademicasDbContext _context;

        public AuthController(GestionesAcademicasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Autentica un usuario con email o documento y contraseña.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { mensaje = "Correo o documento y contraseña son requeridos." });

            var identificador = dto.Email.Trim().ToLower();

            var usuario = await _context.Usuarios
                .Include(u => u.IdInstitucionNavigation)
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == identificador ||
                    (u.NumeroDocumento != null && u.NumeroDocumento.ToLower() == identificador));

            if (usuario == null)
                return Unauthorized(new { mensaje = "Correo, documento o contraseña incorrectos." });

            if (usuario.Activo == false)
                return Unauthorized(new { mensaje = "Tu cuenta está desactivada. Contacta al administrador." });

            bool passwordValida;
            try
            {
                passwordValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            }
            catch
            {
                passwordValida = dto.Password == usuario.PasswordHash;
            }

            if (!passwordValida)
                return Unauthorized(new { mensaje = "Correo, documento o contraseña incorrectos." });

            usuario.UltimoAcceso = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new LoginResponseDto
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Cargo = usuario.Cargo,
                Activo = usuario.Activo ?? true
            });
        }

        /// <summary>
        /// Registra un nuevo usuario con contraseña hasheada.
        /// </summary>
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { mensaje = "Correo y contraseña son requeridos." });

            if (string.IsNullOrWhiteSpace(dto.Nombres) || string.IsNullOrWhiteSpace(dto.Apellidos))
                return BadRequest(new { mensaje = "Nombres y apellidos son requeridos." });

            var emailExiste = await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == dto.Email.Trim().ToLower());

            if (emailExiste)
                return BadRequest(new { mensaje = "Ya existe una cuenta registrada con ese correo electrónico." });

            if (!string.IsNullOrWhiteSpace(dto.NumeroDocumento))
            {
                var docExiste = await _context.Usuarios
                    .AnyAsync(u => u.NumeroDocumento == dto.NumeroDocumento.Trim());
                if (docExiste)
                    return BadRequest(new { mensaje = "Ya existe una cuenta con ese número de documento." });
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var nuevoUsuario = new Models.Usuario
            {
                Nombres = dto.Nombres.Trim(),
                Apellidos = dto.Apellidos.Trim(),
                NombreCompleto = $"{dto.Nombres.Trim()} {dto.Apellidos.Trim()}",
                Email = dto.Email.Trim(),
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento?.Trim(),
                Telefono = dto.Telefono?.Trim(),
                Cargo = dto.Cargo?.Trim(),
                PasswordHash = passwordHash,
                Activo = true,
                EmailConfirmado = false,
                FechaCreacion = DateTimeOffset.UtcNow
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Login), new LoginResponseDto
            {
                Id = nuevoUsuario.Id,
                NombreCompleto = nuevoUsuario.NombreCompleto,
                Email = nuevoUsuario.Email,
                Cargo = nuevoUsuario.Cargo,
                Activo = true
            });
        }

        /// <summary>
        /// Verifica si un email o documento existe en el sistema (paso 1 de recuperar contraseña).
        /// </summary>
        [HttpPost("verificar-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerificarEmail([FromBody] VerificarEmailDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { mensaje = "El correo o documento es requerido." });

            var identificador = dto.Email.Trim().ToLower();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    (u.Email.ToLower() == identificador ||
                    (u.NumeroDocumento != null && u.NumeroDocumento.ToLower() == identificador)) &&
                    u.Activo != false);

            if (usuario == null)
                return NotFound(new { mensaje = "No encontramos ninguna cuenta activa con ese correo o documento." });

            return Ok(new { mensaje = "Usuario verificado. Puedes establecer tu nueva contraseña.", email = usuario.Email });
        }

        /// <summary>
        /// Restablece la contraseña dado un email/documento y la nueva contraseña.
        /// </summary>
        [HttpPost("restablecer-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RestablecerPassword([FromBody] RestablecerPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.NuevaPassword))
                return BadRequest(new { mensaje = "Correo o documento y nueva contraseña son requeridos." });

            if (dto.NuevaPassword.Length < 6)
                return BadRequest(new { mensaje = "La contraseña debe tener al menos 6 caracteres." });

            var identificador = dto.Email.Trim().ToLower();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    (u.Email.ToLower() == identificador ||
                    (u.NumeroDocumento != null && u.NumeroDocumento.ToLower() == identificador)) &&
                    u.Activo != false);

            if (usuario == null)
                return NotFound(new { mensaje = "No se encontró una cuenta activa con ese correo o documento." });

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NuevaPassword);
            usuario.FechaActualizacion = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Contraseña actualizada exitosamente. Ya puedes iniciar sesión." });
        }
    }
}