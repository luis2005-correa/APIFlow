using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IRolService : IService<RolViewModel, long>
    {
        Task<RolViewModel?> GetByNombre(string nombre);
        Task<IEnumerable<RolViewModel>> GetRolesSistema();
    }
}
