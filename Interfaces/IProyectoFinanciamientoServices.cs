using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProyectoFinanciamientoService : IService<ProyectoFinanciamientoViewModel, long>
    {
        Task<IEnumerable<ProyectoFinanciamientoViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<ProyectoFinanciamientoViewModel>> GetByEntidadId(long idEntidad);
        Task<decimal> GetMontoTotalByProyectoId(long idProyecto);
    }
}
