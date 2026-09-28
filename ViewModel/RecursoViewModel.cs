using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class RecursoViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public string Tipo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? UnidadMedida { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? CostoUnitario { get; set; }

        public decimal? CostoTotal { get; set; }

        public static RecursoViewModel ToViewModel(Recurso ob, string nombreProyecto = "")
        {
            return new RecursoViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                Tipo = ob.Tipo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                UnidadMedida = ob.UnidadMedida,
                Cantidad = ob.Cantidad,
                CostoUnitario = ob.CostoUnitario,
                CostoTotal = ob.CostoTotal
            };
        }

        public static Recurso ToRecurso(RecursoViewModel ob)
        {
            return new Recurso()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                Tipo = ob.Tipo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                UnidadMedida = ob.UnidadMedida,
                Cantidad = ob.Cantidad,
                CostoUnitario = ob.CostoUnitario,
                CostoTotal = ob.CostoTotal
            };
        }
    }
}