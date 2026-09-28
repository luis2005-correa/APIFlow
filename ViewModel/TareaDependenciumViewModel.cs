using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class TareaDependenciumViewModel
    {
        public long IdTarea { get; set; }

        public string DescripcionTarea { get; set; } = "";

        public long IdDependeDe { get; set; }

        public string DescripcionDependeDe { get; set; } = "";

        public string? Tipo { get; set; }

        public short? DesfaseDias { get; set; }

        public static TareaDependenciumViewModel ToViewModel(
            TareaDependencium ob,
            string descripcionTarea = "",
            string descripcionDependeDe = "")
        {
            return new TareaDependenciumViewModel()
            {
                IdTarea = ob.IdTarea,
                DescripcionTarea = descripcionTarea,
                IdDependeDe = ob.IdDependeDe,
                DescripcionDependeDe = descripcionDependeDe,
                Tipo = ob.Tipo,
                DesfaseDias = ob.DesfaseDias
            };
        }

        public static TareaDependencium ToTareaDependencium(TareaDependenciumViewModel ob)
        {
            return new TareaDependencium()
            {
                IdTarea = ob.IdTarea,
                IdDependeDe = ob.IdDependeDe,
                Tipo = ob.Tipo,
                DesfaseDias = ob.DesfaseDias
            };
        }
    }
}