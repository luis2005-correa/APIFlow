using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IRegistroAvanceService : IService<RegistroAvanceViewModel, long>
    {
        Task<IEnumerable<RegistroAvanceViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<RegistroAvanceViewModel>> GetByActividadId(long idActividad);
        Task<IEnumerable<RegistroAvanceViewModel>> GetByUsuarioId(long idUsuario);
        Task<decimal> GetHorasTotalesByTareaId(long idTarea);
        Task<decimal> GetHorasTotalesByActividadId(long idActividad);
    }
}
