using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IAsignacionTareaService : IService<AsignacionTareaViewModel, (long idTarea, long idUsuario)>
    {
        Task<IEnumerable<AsignacionTareaViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<AsignacionTareaViewModel>> GetByUsuarioId(long idUsuario);
    }
}
