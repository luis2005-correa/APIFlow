using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class UsuarioRolViewModel
    {
        public long IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public long IdRol { get; set; }

        public string NombreRol { get; set; } = "";

        public DateTimeOffset? AsignadoEn { get; set; }

        public long? AsignadoPor { get; set; }

        public string? NombreAsignadoPor { get; set; }

        public static UsuarioRolViewModel ToViewModel(
            UsuarioRol ob,
            string nombreUsuario = "",
            string nombreRol = "",
            string? nombreAsignadoPor = null)
        {
            return new UsuarioRolViewModel()
            {
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                IdRol = ob.IdRol,
                NombreRol = nombreRol,
                AsignadoEn = ob.AsignadoEn,
                AsignadoPor = ob.AsignadoPor,
                NombreAsignadoPor = nombreAsignadoPor
            };
        }

        public static UsuarioRol ToUsuarioRol(UsuarioRolViewModel ob)
        {
            return new UsuarioRol()
            {
                IdUsuario = ob.IdUsuario,
                IdRol = ob.IdRol,
                AsignadoEn = ob.AsignadoEn,
                AsignadoPor = ob.AsignadoPor
            };
        }
    }
}