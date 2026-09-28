using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IDisciplinaService : IService<DisciplinaViewModel, long>
    {
        Task<IEnumerable<DisciplinaViewModel>> GetActivas();
        Task<IEnumerable<DisciplinaViewModel>> GetSubdisciplinas(long idPadre);
    }
}
