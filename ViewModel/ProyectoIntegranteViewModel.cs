using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ProyectoIntegranteViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string? NombreProyecto { get; set; }

        public long IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }

        public string Rol { get; set; } = null!;

        public long? IdDisciplina { get; set; }

        public string? NombreDisciplina { get; set; }

        public short? DedicacionHoras { get; set; }

        public DateOnly? FechaIngreso { get; set; }

        public DateOnly? FechaRetiro { get; set; }

        public bool? Activo { get; set; }

        public static ProyectoIntegranteViewModel ToViewModel(
            ProyectoIntegrante ob,
            string nombreProyecto = "",
            string nombreUsuario = "",
            string? nombreDisciplina = null)
        {
            return new ProyectoIntegranteViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                Rol = ob.Rol,
                IdDisciplina = ob.IdDisciplina,
                NombreDisciplina = nombreDisciplina,
                DedicacionHoras = ob.DedicacionHoras,
                FechaIngreso = ob.FechaIngreso,
                FechaRetiro = ob.FechaRetiro,
                Activo = ob.Activo
            };
        }

        public static ProyectoIntegrante ToProyectoIntegrante(ProyectoIntegranteViewModel ob)
        {
            return new ProyectoIntegrante()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                IdUsuario = ob.IdUsuario,
                Rol = ob.Rol,
                IdDisciplina = ob.IdDisciplina,
                DedicacionHoras = ob.DedicacionHoras,
                FechaIngreso = ob.FechaIngreso,
                FechaRetiro = ob.FechaRetiro,
                Activo = ob.Activo
            };
        }
    }
}