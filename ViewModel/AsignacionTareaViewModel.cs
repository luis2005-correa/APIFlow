using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class AsignacionTareaViewModel
    {
        public long IdTarea { get; set; }

        public string NombreTarea { get; set; } = "";

        public long IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public decimal? HorasAsignadas { get; set; }

        public DateTimeOffset? AsignadoEn { get; set; }

        public long? AsignadoPor { get; set; }

        public string? NombreUsuarioAsigno { get; set; }

        public static AsignacionTareaViewModel ToViewModel(
            AsignacionTarea ob,
            string nombreTarea = "",
            string nombreUsuario = "",
            string? nombreUsuarioAsigno = null)
        {
            return new AsignacionTareaViewModel()
            {
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                HorasAsignadas = ob.HorasAsignadas,
                AsignadoEn = ob.AsignadoEn,
                AsignadoPor = ob.AsignadoPor,
                NombreUsuarioAsigno = nombreUsuarioAsigno
            };
        }

        public static AsignacionTarea ToAsignacionTarea(AsignacionTareaViewModel ob)
        {
            return new AsignacionTarea()
            {
                IdTarea = ob.IdTarea,
                IdUsuario = ob.IdUsuario,
                HorasAsignadas = ob.HorasAsignadas,
                AsignadoEn = ob.AsignadoEn,
                AsignadoPor = ob.AsignadoPor
            };
        }
    }
}