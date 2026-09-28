using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ApiWebhookViewModel
    {
        public long Id { get; set; }

        public long IdCliente { get; set; }

        public string NombreCliente { get; set; } = "";

        public string Evento { get; set; } = null!;

        public string Secreto { get; set; } = null!;

        public bool? Activo { get; set; }

        public static ApiWebhookViewModel ToViewModel(ApiWebhook ob, string nombreCliente = "")
        {
            return new ApiWebhookViewModel()
            {
                Id = ob.Id,
                IdCliente = ob.IdCliente,
                NombreCliente = nombreCliente,
                Evento = ob.Evento,
                Secreto = ob.Secreto,
                Activo = ob.Activo
            };
        }

        public static ApiWebhook ToApiWebhook(ApiWebhookViewModel ob)
        {
            return new ApiWebhook()
            {
                Id = ob.Id,
                IdCliente = ob.IdCliente,
                Evento = ob.Evento,
                Secreto = ob.Secreto,
                Activo = ob.Activo
            };
        }
    }
}