using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProyectoEstadoHistorialViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public string? EstadoAnterior { get; set; }

        public string EstadoNuevo { get; set; } = null!;

        public string? Justificacion { get; set; }

        public string? Observaciones { get; set; }

        public long? CambiadoPor { get; set; }

        public string? NombreCambiadoPor { get; set; }

        public long? IdResponsableVerificacion { get; set; }

        public string? NombreResponsableVerificacion { get; set; }

        public DateTimeOffset? FechaCambio { get; set; }

        public static ProyectoEstadoHistorialViewModel ToViewModel(
            ProyectoEstadoHistorial ob,
            string nombreProyecto = "",
            string? nombreCambiadoPor = null,
            string? nombreResponsableVerificacion = null)
        {
            return new ProyectoEstadoHistorialViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                EstadoAnterior = ob.EstadoAnterior,
                EstadoNuevo = ob.EstadoNuevo,
                Justificacion = ob.Justificacion,
                Observaciones = ob.Observaciones,
                CambiadoPor = ob.CambiadoPor,
                NombreCambiadoPor = nombreCambiadoPor,
                IdResponsableVerificacion = ob.IdResponsableVerificacion,
                NombreResponsableVerificacion = nombreResponsableVerificacion,
                FechaCambio = ob.FechaCambio
            };
        }

        public static ProyectoEstadoHistorial ToProyectoEstadoHistorial(ProyectoEstadoHistorialViewModel ob)
        {
            return new ProyectoEstadoHistorial()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                EstadoAnterior = ob.EstadoAnterior,
                EstadoNuevo = ob.EstadoNuevo,
                Justificacion = ob.Justificacion,
                Observaciones = ob.Observaciones,
                CambiadoPor = ob.CambiadoPor,
                IdResponsableVerificacion = ob.IdResponsableVerificacion,
                FechaCambio = ob.FechaCambio
            };
        }
    }
}