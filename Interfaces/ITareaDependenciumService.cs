using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface ITareaDependenciumService : IService<TareaDependenciumViewModel, (long IdTarea, long IdDependeDe)>
    {
        Task<IEnumerable<TareaDependenciumViewModel>> GetDependenciasByTareaId(long idTarea);
        Task<IEnumerable<TareaDependenciumViewModel>> GetTareasQueDependenDe(long idDependeDe);
        Task<bool> ExisteDependenciaCircular(long idTarea, long idDependeDe);
    }
}
