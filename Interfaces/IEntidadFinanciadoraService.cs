using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IEntidadFinanciadoraService : IService<EntidadFinanciadoraViewModel, long>
    {
        Task<IEnumerable<EntidadFinanciadoraViewModel>> GetActivas();
        Task<EntidadFinanciadoraViewModel?> GetByNit(string nit);
    }
}
