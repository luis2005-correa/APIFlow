using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProyectoEstadoHistorialService : IService<ProyectoEstadoHistorialViewModel, long>
    {
        Task<IEnumerable<ProyectoEstadoHistorialViewModel>> GetByProyectoId(long idProyecto);
        Task<ProyectoEstadoHistorialViewModel?> GetUltimoCambioByProyectoId(long idProyecto);
    }
}
