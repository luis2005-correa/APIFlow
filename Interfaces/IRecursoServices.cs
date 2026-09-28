using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IRecursoService : IService<RecursoViewModel, long>
    {
        Task<IEnumerable<RecursoViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<RecursoViewModel>> GetByTipo(string tipo);
        Task<decimal> GetCostoTotalByProyectoId(long idProyecto);
    }
}
