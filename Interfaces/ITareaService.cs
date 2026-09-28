using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface ITareaService : IService<TareaViewModel, long>
    {
        Task<IEnumerable<TareaViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<TareaViewModel>> GetByFaseId(long idFase);
        Task<IEnumerable<TareaViewModel>> GetByResponsableId(long idResponsable);
        Task<IEnumerable<TareaViewModel>> GetByEstado(string estado);
        Task<bool> CambiarEstado(long idTarea, string nuevoEstado);
        Task<bool> ActualizarAvance(long idTarea, decimal porcentajeAvance);
    }
}
