using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IApiWebhookService : IService<ApiWebhookViewModel, long>
    {
        Task<IEnumerable<ApiWebhookViewModel>> GetByClienteId(long idCliente);
        Task<IEnumerable<ApiWebhookViewModel>> GetByEvento(string evento);
    }
}
