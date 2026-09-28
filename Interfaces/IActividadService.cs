using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IActividadService : IService<ActividadViewModel, long>
    {
        Task<IEnumerable<ActividadViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<ActividadViewModel>> GetByResponsableId(long idResponsable);
    }
}
