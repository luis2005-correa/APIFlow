using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class PermisoViewModel
    {
        public long Id { get; set; }

        public string Clave { get; set; } = null!;

        public string? Descripcion { get; set; }

        public static PermisoViewModel ToViewModel(Permiso ob)
        {
            return new PermisoViewModel()
            {
                Id = ob.Id,
                Clave = ob.Clave,
                Descripcion = ob.Descripcion
            };
        }

        public static Permiso ToPermiso(PermisoViewModel ob)
        {
            return new Permiso()
            {
                Id = ob.Id,
                Clave = ob.Clave,
                Descripcion = ob.Descripcion
            };
        }
    }
}