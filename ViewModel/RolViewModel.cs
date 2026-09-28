using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class RolViewModel
    {
        public long Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool? Sistema { get; set; }

        public static RolViewModel ToViewModel(Rol ob)
        {
            return new RolViewModel()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Sistema = ob.Sistema
            };
        }

        public static Rol ToRol(RolViewModel ob)
        {
            return new Rol()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Descripcion = ob.Descripcion,
                Sistema = ob.Sistema
            };
        }
    }
}