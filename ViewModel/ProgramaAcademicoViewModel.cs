using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProgramaAcademicoViewModel
    {
        public long Id { get; set; }

        public long IdUnidadAcademica { get; set; }

        public string NombreUnidadAcademica { get; set; } = "";

        public long? IdDisciplina { get; set; }

        public string? NombreDisciplina { get; set; }

        public string Nombre { get; set; } = null!;

        public string? CodigoSnies { get; set; }

        public string? Nivel { get; set; }

        public bool? Activo { get; set; }

        public static ProgramaAcademicoViewModel ToViewModel(
            ProgramaAcademico ob,
            string nombreUnidadAcademica = "",
            string? nombreDisciplina = null)
        {
            return new ProgramaAcademicoViewModel()
            {
                Id = ob.Id,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                NombreUnidadAcademica = nombreUnidadAcademica,
                IdDisciplina = ob.IdDisciplina,
                NombreDisciplina = nombreDisciplina,
                Nombre = ob.Nombre,
                CodigoSnies = ob.CodigoSnies,
                Nivel = ob.Nivel,
                Activo = ob.Activo
            };
        }

        public static ProgramaAcademico ToProgramaAcademico(ProgramaAcademicoViewModel ob)
        {
            return new ProgramaAcademico()
            {
                Id = ob.Id,
                IdUnidadAcademica = ob.IdUnidadAcademica,
                IdDisciplina = ob.IdDisciplina,
                Nombre = ob.Nombre,
                CodigoSnies = ob.CodigoSnies,
                Nivel = ob.Nivel,
                Activo = ob.Activo
            };
        }
    }
}