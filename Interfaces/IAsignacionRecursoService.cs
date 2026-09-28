using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IAsignacionRecursoService : IService<AsignacionRecursoViewModel, long>
    {
        Task<IEnumerable<AsignacionRecursoViewModel>> GetByRecursoId(long idRecurso);
        Task<IEnumerable<AsignacionRecursoViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<AsignacionRecursoViewModel>> GetByFaseId(long idFase);
    }
}
