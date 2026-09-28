using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class NotificacionViewModel
    {
        public long Id { get; set; }

        public long IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public long? IdProyecto { get; set; }

        public string? NombreProyecto { get; set; }

        public string? Tipo { get; set; }

        public string Titulo { get; set; } = null!;

        public string Mensaje { get; set; } = null!;

        public string? Enlace { get; set; }

        public bool? Leida { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public static NotificacionViewModel ToViewModel(
            Notificacion ob,
            string nombreUsuario = "",
            string? nombreProyecto = null)
        {
            return new NotificacionViewModel()
            {
                Id = ob.Id,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                Tipo = ob.Tipo,
                Titulo = ob.Titulo,
                Mensaje = ob.Mensaje,
                Enlace = ob.Enlace,
                Leida = ob.Leida,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static Notificacion ToNotificacion(NotificacionViewModel ob)
        {
            return new Notificacion()
            {
                Id = ob.Id,
                IdUsuario = ob.IdUsuario,
                IdProyecto = ob.IdProyecto,
                Tipo = ob.Tipo,
                Titulo = ob.Titulo,
                Mensaje = ob.Mensaje,
                Enlace = ob.Enlace,
                Leida = ob.Leida,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}