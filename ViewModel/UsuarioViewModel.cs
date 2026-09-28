using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class UsuarioViewModel
    {
        public long Id { get; set; }

        public long? IdInstitucion { get; set; }

        public string? NombreInstitucion { get; set; }

        public long? IdUnidadAcademica { get; set; }

        public string? NombreUnidadAcademica { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool? EmailConfirmado { get; set; }

        public string? Telefono { get; set; }

        public string? Cargo { get; set; }

        public bool? Activo { get; set; }

        public DateTimeOffset? UltimoAcceso { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static UsuarioViewModel ToViewModel(
            Usuario ob,
            string? nombreInstitucion = null,
            string? nombreUnidadAcademica = null)
        {
            return new UsuarioViewModel()
            {
                Id = ob.Id,
                IdInstitucion = ob.IdInstitucion,
                NombreInstitucion = nombreInstitucion,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                NombreUnidadAcademica = nombreUnidadAcademica,
                TipoDocumento = ob.TipoDocumento,
                NumeroDocumento = ob.NumeroDocumento,
                Nombres = ob.Nombres,
                Apellidos = ob.Apellidos,
                NombreCompleto = ob.NombreCompleto,
                Email = ob.Email,
                EmailConfirmado = ob.EmailConfirmado,
                Telefono = ob.Telefono,
                Cargo = ob.Cargo,
                Activo = ob.Activo,
                UltimoAcceso = ob.UltimoAcceso,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static Usuario ToUsuario(UsuarioViewModel ob)
        {
            return new Usuario()
            {
                Id = ob.Id,
                IdInstitucion = ob.IdInstitucion,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                TipoDocumento = ob.TipoDocumento,
                NumeroDocumento = ob.NumeroDocumento,
                Nombres = ob.Nombres,
                Apellidos = ob.Apellidos,
                NombreCompleto = ob.NombreCompleto,
                Email = ob.Email,
                EmailConfirmado = ob.EmailConfirmado,
                Telefono = ob.Telefono,
                Cargo = ob.Cargo,
                Activo = ob.Activo,
                UltimoAcceso = ob.UltimoAcceso,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}