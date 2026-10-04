using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class VerificacionViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string? NombreProyecto { get; set; }

        public long? IdFase { get; set; }

        public string? NombreFase { get; set; }

        public long? IdTarea { get; set; }

        public string? DescripcionTarea { get; set; }

        public string? Tipo { get; set; }

        public long? AsignadoPor { get; set; }

        public string? NombreAsignadoPor { get; set; }

        public string? Resultado { get; set; }

        public string? Observaciones { get; set; }

        public DateTimeOffset? FechaAsignacion { get; set; }

        public DateOnly? FechaLimite { get; set; }

        public DateTimeOffset? FechaVerificacion { get; set; }

        public static VerificacionViewModel ToViewModel(
            Verificacion ob,
            string nombreProyecto = "",
            string? nombreFase = null,
            string? descripcionTarea = null,
            string? nombreAsignadoPor = null)
        {
            return new VerificacionViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdFase = ob.IdFase,
                NombreFase = nombreFase,
                IdTarea = ob.IdTarea,
                DescripcionTarea = descripcionTarea,
                Tipo = ob.Tipo,
                AsignadoPor = ob.AsignadoPor,
                NombreAsignadoPor = nombreAsignadoPor,
                Resultado = ob.Resultado,
                Observaciones = ob.Observaciones,
                FechaAsignacion = ob.FechaAsignacion,
                FechaLimite = ob.FechaLimite,
                FechaVerificacion = ob.FechaVerificacion
            };
        }

        public static Verificacion ToVerificacion(VerificacionViewModel ob)
        {
            return new Verificacion()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                IdFase = ob.IdFase,
                IdTarea = ob.IdTarea,
                Tipo = ob.Tipo,
                AsignadoPor = ob.AsignadoPor,
                Resultado = ob.Resultado,
                Observaciones = ob.Observaciones,
                FechaAsignacion = ob.FechaAsignacion,
                FechaLimite = ob.FechaLimite,
                FechaVerificacion = ob.FechaVerificacion
            };
        }
    }
}