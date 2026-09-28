using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class AsignacionRecursoViewModel
    {
        public long Id { get; set; }

        public long IdRecurso { get; set; }

        public string NombreRecurso { get; set; } = "";

        public long? IdFase { get; set; }

        public string? NombreFase { get; set; }

        public long? IdTarea { get; set; }

        public string? NombreTarea { get; set; }

        public decimal Cantidad { get; set; }

        public string? Observacion { get; set; }

        public static AsignacionRecursoViewModel ToViewModel(
            AsignacionRecurso ob,
            string nombreRecurso = "",
            string? nombreFase = null,
            string? nombreTarea = null)
        {
            return new AsignacionRecursoViewModel()
            {
                Id = ob.Id,
                IdRecurso = ob.IdRecurso,
                NombreRecurso = nombreRecurso,
                IdFase = ob.IdFase,
                NombreFase = nombreFase,
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                Cantidad = ob.Cantidad,
                Observacion = ob.Observacion
            };
        }

        public static AsignacionRecurso ToAsignacionRecurso(AsignacionRecursoViewModel ob)
        {
            return new AsignacionRecurso()
            {
                Id = ob.Id,
                IdRecurso = ob.IdRecurso,
                IdFase = ob.IdFase,
                IdTarea = ob.IdTarea,
                Cantidad = ob.Cantidad,
                Observacion = ob.Observacion
            };
        }
    }
}