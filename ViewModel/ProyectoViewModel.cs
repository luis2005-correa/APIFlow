using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProyectoViewModel
    {
        public long Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? Justificacion { get; set; }

        public string? ObjetivoGeneral { get; set; }

        public string? Resumen { get; set; }

        public string? PalabrasClave { get; set; }

        public long IdInstitucion { get; set; }

        public string NombreInstitucion { get; set; } = "";

        public long? IdUnidadAcademica { get; set; }

        public string? NombreUnidadAcademica { get; set; }

        public long IdLider { get; set; }

        public string NombreLider { get; set; } = "";

        public string? Estado { get; set; }

        public string? OrigenFinanciamiento { get; set; }

        public decimal? PresupuestoTotal { get; set; }

        public string? Moneda { get; set; }

        public DateOnly? FechaInicioPlan { get; set; }

        public DateOnly? FechaFinPlan { get; set; }

        public DateOnly? FechaInicioReal { get; set; }

        public DateOnly? FechaFinReal { get; set; }

        public decimal? PorcentajeAvance { get; set; }

        public string? JustificacionCancelacion { get; set; }

        public long? CreadoPor { get; set; }

        public string? NombreCreadoPor { get; set; }

        public long? ActualizadoPor { get; set; }

        public string? NombreActualizadoPor { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public DateTimeOffset? EliminadoEn { get; set; }

        public static ProyectoViewModel ToViewModel(
            Proyecto ob,
            string nombreInstitucion = "",
            string? nombreUnidadAcademica = null,
            string nombreLider = "",
            string? nombreCreadoPor = null,
            string? nombreActualizadoPor = null)
        {
            return new ProyectoViewModel()
            {
                Id = ob.Id,
                Codigo = ob.Codigo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Justificacion = ob.Justificacion,
                ObjetivoGeneral = ob.ObjetivoGeneral,
                Resumen = ob.Resumen,
                PalabrasClave = ob.PalabrasClave,
                IdInstitucion = ob.IdInstitucion,
                NombreInstitucion = nombreInstitucion,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                NombreUnidadAcademica = nombreUnidadAcademica,
                IdLider = ob.IdLider,
                NombreLider = nombreLider,
                Estado = ob.Estado,
                OrigenFinanciamiento = ob.OrigenFinanciamiento,
                PresupuestoTotal = ob.PresupuestoTotal,
                Moneda = ob.Moneda,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                PorcentajeAvance = ob.PorcentajeAvance,
                JustificacionCancelacion = ob.JustificacionCancelacion,
                CreadoPor = ob.CreadoPor,
                NombreCreadoPor = nombreCreadoPor,
                ActualizadoPor = ob.ActualizadoPor,
                NombreActualizadoPor = nombreActualizadoPor,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion,
                EliminadoEn = ob.EliminadoEn
            };
        }

        public static Proyecto ToProyecto(ProyectoViewModel ob)
        {
            return new Proyecto()
            {
                Id = ob.Id,
                Codigo = ob.Codigo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Justificacion = ob.Justificacion,
                ObjetivoGeneral = ob.ObjetivoGeneral,
                Resumen = ob.Resumen,
                PalabrasClave = ob.PalabrasClave,
                IdInstitucion = ob.IdInstitucion,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                IdLider = ob.IdLider,
                Estado = ob.Estado,
                OrigenFinanciamiento = ob.OrigenFinanciamiento,
                PresupuestoTotal = ob.PresupuestoTotal,
                Moneda = ob.Moneda,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                FechaInicioReal = ob.FechaInicioReal,
                FechaFinReal = ob.FechaFinReal,
                PorcentajeAvance = ob.PorcentajeAvance,
                JustificacionCancelacion = ob.JustificacionCancelacion,
                CreadoPor = ob.CreadoPor,
                ActualizadoPor = ob.ActualizadoPor,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion,
                EliminadoEn = ob.EliminadoEn
            };
        }
    }
}