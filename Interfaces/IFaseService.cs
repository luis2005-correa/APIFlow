using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IFaseService : IService<FaseViewModel, long>
    {
        Task<IEnumerable<FaseViewModel>> GetByProyectoId(long idProyecto);
    }
}
