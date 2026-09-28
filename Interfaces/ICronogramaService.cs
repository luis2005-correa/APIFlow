using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface ICronogramaService : IService<CronogramaViewModel, long>
    {
        Task<IEnumerable<CronogramaViewModel>> GetByProyectoId(long idProyecto);
        Task<CronogramaViewModel?> GetUltimaVersionByProyectoId(long idProyecto);
    }
}
