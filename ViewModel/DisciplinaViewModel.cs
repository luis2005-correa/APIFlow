using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class DisciplinaViewModel
    {
        public long Id { get; set; }

        public long? IdPadre { get; set; }

        public string? NombreDisciplinaPadre { get; set; }

        public string? Codigo { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }

        public static DisciplinaViewModel ToViewModel(Disciplina ob, string? nombreDisciplinaPadre = null)
        {
            return new DisciplinaViewModel()
            {
                Id = ob.Id,
                IdPadre = ob.IdPadre,
                NombreDisciplinaPadre = nombreDisciplinaPadre,
                Codigo = ob.Codigo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Activo = ob.Activo
            };
        }

        public static Disciplina ToDisciplina(DisciplinaViewModel ob)
        {
            return new Disciplina()
            {
                Id = ob.Id,
                IdPadre = ob.IdPadre,
                Codigo = ob.Codigo,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Activo = ob.Activo
            };
        }
    }
}