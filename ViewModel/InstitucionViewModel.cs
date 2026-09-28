using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class InstitucionViewModel
    {
        public long Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Siglas { get; set; }

        public string? Nit { get; set; }

        public string? Naturaleza { get; set; }

        public string? Pais { get; set; }

        public string? Ciudad { get; set; }

        public string? SitioWeb { get; set; }

        public bool? Activo { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static InstitucionViewModel ToViewModel(Institucion ob)
        {
            return new InstitucionViewModel()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Siglas = ob.Siglas,
                Nit = ob.Nit,
                Naturaleza = ob.Naturaleza,
                Pais = ob.Pais,
                Ciudad = ob.Ciudad,
                SitioWeb = ob.SitioWeb,
                Activo = ob.Activo,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static Institucion ToInstitucion(InstitucionViewModel ob)
        {
            return new Institucion()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Siglas = ob.Siglas,
                Nit = ob.Nit,
                Naturaleza = ob.Naturaleza,
                Pais = ob.Pais,
                Ciudad = ob.Ciudad,
                SitioWeb = ob.SitioWeb,
                Activo = ob.Activo,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}