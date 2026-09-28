using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class EntidadFinanciadoraViewModel
    {
        public long Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Tipo { get; set; }

        public string? Nit { get; set; }

        public string? Pais { get; set; }

        public string? Contacto { get; set; }

        public bool? Activo { get; set; }

        public static EntidadFinanciadoraViewModel ToViewModel(EntidadFinanciadora ob)
        {
            return new EntidadFinanciadoraViewModel()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Tipo = ob.Tipo,
                Nit = ob.Nit,
                Pais = ob.Pais,
                Contacto = ob.Contacto,
                Activo = ob.Activo
            };
        }

        public static EntidadFinanciadora ToEntidadFinanciadora(EntidadFinanciadoraViewModel ob)
        {
            return new EntidadFinanciadora()
            {
                Id = ob.Id,
                Nombre = ob.Nombre,
                Tipo = ob.Tipo,
                Nit = ob.Nit,
                Pais = ob.Pais,
                Contacto = ob.Contacto,
                Activo = ob.Activo
            };
        }
    }
}