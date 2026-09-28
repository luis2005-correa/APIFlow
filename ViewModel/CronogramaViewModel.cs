using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class CronogramaViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public string Nombre { get; set; } = null!;

        public short? Version { get; set; }

        public string? Formato { get; set; }

        public string? Datos { get; set; }

        public string? Observaciones { get; set; }

        public long? SubidoPor { get; set; }

        public string? NombreUsuarioSubio { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public static CronogramaViewModel ToViewModel(
            Cronograma ob,
            string nombreProyecto = "",
            string? nombreUsuarioSubio = null)
        {
            return new CronogramaViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                Nombre = ob.Nombre,
                Version = ob.Version,
                Formato = ob.Formato,
                Datos = ob.Datos,
                Observaciones = ob.Observaciones,
                SubidoPor = ob.SubidoPor,
                NombreUsuarioSubio = nombreUsuarioSubio,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static Cronograma ToCronograma(CronogramaViewModel ob)
        {
            return new Cronograma()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                Nombre = ob.Nombre,
                Version = ob.Version,
                Formato = ob.Formato,
                Datos = ob.Datos,
                Observaciones = ob.Observaciones,
                SubidoPor = ob.SubidoPor,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}