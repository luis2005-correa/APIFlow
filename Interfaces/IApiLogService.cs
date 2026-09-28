using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.ViewModel;


namespace AcademiaFlowAPI.Services.Interfaces
{
    public interface IApiLogService : IService<ApiLogViewModel, long>
    {
        Task<IEnumerable<ApiLogViewModel>> GetByClienteId(long idCliente);
        Task<IEnumerable<ApiLogViewModel>> GetByUsuarioId(long idUsuario);
        Task<IEnumerable<ApiLogViewModel>> GetByStatusCode(short statusCode);
    }
}