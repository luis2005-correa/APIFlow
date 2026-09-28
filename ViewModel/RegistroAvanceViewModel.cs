using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class RegistroAvanceViewModel
    {
        public long Id { get; set; }

        public long? IdTarea { get; set; }

        public string? NombreTarea { get; set; }

        public long? IdActividad { get; set; }

        public string? NombreActividad { get; set; }

        public long IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string Descripcion { get; set; } = null!;

        public decimal? Porcentaje { get; set; }

        public decimal HorasDedicadas { get; set; }

        public DateTimeOffset? FechaReporte { get; set; }

        public static RegistroAvanceViewModel ToViewModel(
            RegistroAvance ob,
            string? nombreTarea = null,
            string? nombreActividad = null,
            string nombreUsuario = "")
        {
            return new RegistroAvanceViewModel()
            {
                Id = ob.Id,
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                IdActividad = ob.IdActividad,
                NombreActividad = nombreActividad,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                Descripcion = ob.Descripcion,
                Porcentaje = ob.Porcentaje,
                HorasDedicadas = ob.HorasDedicadas,
                FechaReporte = ob.FechaReporte
            };
        }

        public static RegistroAvance ToRegistroAvance(RegistroAvanceViewModel ob)
        {
            return new RegistroAvance()
            {
                Id = ob.Id,
                IdTarea = ob.IdTarea,
                IdActividad = ob.IdActividad,
                IdUsuario = ob.IdUsuario,
                Descripcion = ob.Descripcion,
                Porcentaje = ob.Porcentaje,
                HorasDedicadas = ob.HorasDedicadas,
                FechaReporte = ob.FechaReporte
            };
        }
    }
}