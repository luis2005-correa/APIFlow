using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ApiLogViewModel
    {
        public long Id { get; set; }

        public long? IdCliente { get; set; }

        public string NombreCliente { get; set; } = "";

        public long? IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string Metodo { get; set; } = null!;

        public string Ruta { get; set; } = null!;

        public short StatusCode { get; set; }

        public int? DuracionMs { get; set; }

        public string? Ip { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public static ApiLogViewModel ToViewModel(ApiLog ob, string nombreCliente = "", string nombreUsuario = "")
        {
            return new ApiLogViewModel()
            {
                Id = ob.Id,
                IdCliente = ob.IdCliente,
                NombreCliente = nombreCliente,
                IdUsuario = ob.IdUsuario,
                NombreUsuario = nombreUsuario,
                Metodo = ob.Metodo,
                Ruta = ob.Ruta,
                StatusCode = ob.StatusCode,
                DuracionMs = ob.DuracionMs,
                Ip = ob.Ip,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static ApiLog ToApiLog(ApiLogViewModel ob)
        {
            return new ApiLog()
            {
                Id = ob.Id,
                IdCliente = ob.IdCliente,
                IdUsuario = ob.IdUsuario,
                Metodo = ob.Metodo,
                Ruta = ob.Ruta,
                StatusCode = ob.StatusCode,
                DuracionMs = ob.DuracionMs,
                Ip = ob.Ip,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}