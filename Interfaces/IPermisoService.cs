using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IPermisoService : IService<PermisoViewModel, long>
    {
        Task<PermisoViewModel?> GetByClave(string clave);
    }
}
