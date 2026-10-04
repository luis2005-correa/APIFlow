using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class TareaViewModel
    {
        public long Id { get; set; }

        public long IdFase { get; set; }

        public string? NombreFase { get; set; }

        public long IdProyecto { get; set; }

        public string? NombreProyecto { get; set; }

        public string Descripcion { get; set; } = null!;

        public short? Orden { get; set; }

        public string? Prioridad { get; set; }

        public string? Estado { get; set; }

        public long? IdResponsable { get; set; }

        public string? NombreResponsable { get; set; }

        public DateOnly? FechaInicioPlan { get; set; }

        public DateOnly? FechaFinPlan { get; set; }

        public DateOnly? FechaInicioReal { get; set; }

        public DateOnly? FechaFinReal { get; set; }

        public decimal? HorasEstimadas { get; set; }

        public decimal? PorcentajeAvance { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static TareaViewModel ToViewModel(
            Tarea ob,
            string nombreFase = "",
            string nombreProyecto = "",
            string? nombreResponsable = null)
        {
            return new TareaViewModel()
            {
                Id = ob.Id,
                IdFase = ob.IdFase,
                NombreFase = nombreFase,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Prioridad = ob.Prioridad,
                Estado = ob.Estado,
                IdResponsable = ob.IdResponsable,
                NombreResponsable = nombreResponsable,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                HorasEstimadas = ob.HorasEstimadas,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static Tarea ToTarea(TareaViewModel ob)
        {
            return new Tarea()
            {
                Id = ob.Id,
                IdFase = ob.IdFase,
                IdProyecto = ob.IdProyecto,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Prioridad = ob.Prioridad,
                Estado = ob.Estado,
                IdResponsable = ob.IdResponsable,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                HorasEstimadas = ob.HorasEstimadas,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}