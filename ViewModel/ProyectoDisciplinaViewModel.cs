using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProyectoDisciplinaViewModel
    {
        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public long IdDisciplina { get; set; }

        public string NombreDisciplina { get; set; } = "";

        public bool? Principal { get; set; }

        public static ProyectoDisciplinaViewModel ToViewModel(
            ProyectoDisciplina ob,
            string nombreProyecto = "",
            string nombreDisciplina = "")
        {
            return new ProyectoDisciplinaViewModel()
            {
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdDisciplina = ob.IdDisciplina,
                NombreDisciplina = nombreDisciplina,
                Principal = ob.Principal
            };
        }

        public static ProyectoDisciplina ToProyectoDisciplina(ProyectoDisciplinaViewModel ob)
        {
            return new ProyectoDisciplina()
            {
                IdProyecto = ob.IdProyecto,
                IdDisciplina = ob.IdDisciplina,
                Principal = ob.Principal
            };
        }
    }
}