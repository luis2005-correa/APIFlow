using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class FaseViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string? NombreProyecto { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public short? Orden { get; set; }

        public decimal? Peso { get; set; }

        public DateOnly? FechaInicioPlan { get; set; }

        public DateOnly? FechaFinPlan { get; set; }

        public DateOnly? FechaInicioReal { get; set; }

        public DateOnly? FechaFinReal { get; set; }

        public decimal? PorcentajeAvance { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static FaseViewModel ToViewModel(Fase ob, string nombreProyecto = "")
        {
            return new FaseViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Peso = ob.Peso,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static Fase ToFase(FaseViewModel ob)
        {
            return new Fase()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Peso = ob.Peso,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}