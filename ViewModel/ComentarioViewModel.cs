using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ComentarioViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public long? IdFase { get; set; }

        public string? NombreFase { get; set; }

        public long? IdTarea { get; set; }

        public string? NombreTarea { get; set; }

        public long? IdActividad { get; set; }

        public string? NombreActividad { get; set; }

        public long? IdComentarioPadre { get; set; }

        public long IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string Contenido { get; set; } = null!;

        public string? Menciones { get; set; }

        public DateTimeOffset? EditadoEn { get; set; }

        public bool? Eliminado { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public static ComentarioViewModel ToViewModel(
            Comentario ob,
            string nombreProyecto = "",
            string? nombreFase = null,
            string? nombreTarea = null,
            string? nombreActividad = null,
            string nombreUsuario = "")
        {
            return new ComentarioViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdFase = ob.IdFase,
                NombreFase = nombreFase,
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                IdActividad = ob.IdActividad,
                NombreActividad = nombreActividad,
                IdComentarioPadre = ob.IdComentarioPadre,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                Contenido = ob.Contenido,
                Menciones = ob.Menciones,
                EditadoEn = ob.EditadoEn,
                Eliminado = ob.Eliminado,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static Comentario ToComentario(ComentarioViewModel ob)
        {
            return new Comentario()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                IdFase = ob.IdFase,
                IdTarea = ob.IdTarea,
                IdActividad = ob.IdActividad,
                IdComentarioPadre = ob.IdComentarioPadre,
                IdUsuario = ob.IdUsuario,
                Contenido = ob.Contenido,
                Menciones = ob.Menciones,
                EditadoEn = ob.EditadoEn,
                Eliminado = ob.Eliminado,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}