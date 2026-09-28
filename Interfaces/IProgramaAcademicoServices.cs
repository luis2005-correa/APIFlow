using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProgramaAcademicoService : IService<ProgramaAcademicoViewModel, long>
    {
        Task<IEnumerable<ProgramaAcademicoViewModel>> GetActivos();
        Task<IEnumerable<ProgramaAcademicoViewModel>> GetByUnidadAcademicaId(long idUnidadAcademica);
        Task<ProgramaAcademicoViewModel?> GetByCodigoSnies(string codigoSnies);
    }
}
