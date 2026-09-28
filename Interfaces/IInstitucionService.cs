using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IInstitucionService : IService<InstitucionViewModel, long>
    {
        Task<IEnumerable<InstitucionViewModel>> GetActivas();
        Task<InstitucionViewModel?> GetByNit(string nit);
    }
}
