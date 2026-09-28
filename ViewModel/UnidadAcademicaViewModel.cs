using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class UnidadAcademicaViewModel
    {
        public long Id { get; set; }

        public long IdInstitucion { get; set; }

        public string NombreInstitucion { get; set; } = "";

        public long? IdPadre { get; set; }

        public string? NombreUnidadPadre { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Tipo { get; set; }

        public bool? Activo { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static UnidadAcademicaViewModel ToViewModel(
            UnidadAcademica ob,
            string nombreInstitucion = "",
            string? nombreUnidadPadre = null)
        {
            return new UnidadAcademicaViewModel()
            {
                Id = ob.Id,
                IdInstitucion = ob.IdInstitucion,
                NombreInstitucion = nombreInstitucion,
                IdPadre = ob.IdPadre,
                NombreUnidadPadre = nombreUnidadPadre,
                Nombre = ob.Nombre,
                Tipo = ob.Tipo,
                Activo = ob.Activo,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static UnidadAcademica ToUnidadAcademica(UnidadAcademicaViewModel ob)
        {
            return new UnidadAcademica()
            {
                Id = ob.Id,
                IdInstitucion = ob.IdInstitucion,
                IdPadre = ob.IdPadre,
                Nombre = ob.Nombre,
                Tipo = ob.Tipo,
                Activo = ob.Activo,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}