using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProyectoIntegranteService : IService<ProyectoIntegranteViewModel, long>
    {
        Task<IEnumerable<ProyectoIntegranteViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<ProyectoIntegranteViewModel>> GetByUsuarioId(long idUsuario);
        Task<IEnumerable<ProyectoIntegranteViewModel>> GetActivosByProyectoId(long idProyecto);
    }
}
