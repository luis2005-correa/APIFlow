using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProyectoFinanciamientoViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public long IdEntidad { get; set; }

        public string NombreEntidad { get; set; } = "";

        public decimal Monto { get; set; }

        public string? Moneda { get; set; }

        public string? NumeroConvenio { get; set; }

        public DateOnly? FechaAprobacion { get; set; }

        public string? Observaciones { get; set; }

        public static ProyectoFinanciamientoViewModel ToViewModel(
            ProyectoFinanciamiento ob,
            string nombreProyecto = "",
            string nombreEntidad = "")
        {
            return new ProyectoFinanciamientoViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdEntidad = ob.IdEntidad,
                NombreEntidad = nombreEntidad,
                Monto = ob.Monto,
                Moneda = ob.Moneda,
                NumeroConvenio = ob.NumeroConvenio,
                FechaAprobacion = ob.FechaAprobacion,
                Observaciones = ob.Observaciones
            };
        }

        public static ProyectoFinanciamiento ToProyectoFinanciamiento(ProyectoFinanciamientoViewModel ob)
        {
            return new ProyectoFinanciamiento()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                IdEntidad = ob.IdEntidad,
                Monto = ob.Monto,
                Moneda = ob.Moneda,
                NumeroConvenio = ob.NumeroConvenio,
                FechaAprobacion = ob.FechaAprobacion,
                Observaciones = ob.Observaciones
            };
        }
    }
}