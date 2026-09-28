using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface INotificacionService : IService<NotificacionViewModel, long>
    {
        Task<IEnumerable<NotificacionViewModel>> GetByUsuarioId(long idUsuario);
        Task<IEnumerable<NotificacionViewModel>> GetNoLeidasByUsuarioId(long idUsuario);
        Task<bool> MarcarComoLeida(long idNotificacion);
        Task<bool> MarcarTodasComoLeidas(long idUsuario);
    }
}
