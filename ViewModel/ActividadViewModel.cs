 using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ActividadViewModel
    {
        public long Id { get; set; }

        public long IdTarea { get; set; }

        public string NombreTarea { get; set; } = "";

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public short? Orden { get; set; }

        public string? Estado { get; set; }

        public long? IdResponsable { get; set; }

        public string NombreResponsable { get; set; } = "";

        public DateOnly? FechaInicioPlan { get; set; }

        public DateOnly? FechaFinPlan { get; set; }

        public decimal? HorasEstimadas { get; set; }

        public decimal? HorasReales { get; set; }

        public decimal? PorcentajeAvance { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public DateTimeOffset? FechaActualizacion { get; set; }

        public static ActividadViewModel ToViewModel(Actividad ob, string nombreTarea = "", string nombreResponsable = "")
        {
            return new ActividadViewModel()
            {
                Id = ob.Id,
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Estado = ob.Estado,
                IdResponsable = ob.IdResponsable,
                NombreResponsable = nombreResponsable,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                HorasEstimadas = ob.HorasEstimadas,
                HorasReales = ob.HorasReales,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }

        public static Actividad ToActividad(ActividadViewModel ob)
        {
            return new Actividad()
            {
                Id = ob.Id,
                IdTarea = ob.IdTarea,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Orden = ob.Orden,
                Estado = ob.Estado,
                IdResponsable = ob.IdResponsable,
                FechaInicioPlan = ob.FechaInicioPlan,
                FechaFinPlan = ob.FechaFinPlan,
                HorasEstimadas = ob.HorasEstimadas,
                HorasReales = ob.HorasReales,
                PorcentajeAvance = ob.PorcentajeAvance,
                FechaCreacion = ob.FechaCreacion,
                FechaActualizacion = ob.FechaActualizacion
            };
        }
    }
}