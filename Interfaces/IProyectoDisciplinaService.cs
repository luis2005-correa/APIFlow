using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProyectoDisciplinaService
    {
        Task<IEnumerable<ProyectoDisciplinaViewModel>> GetAll();
        Task<ProyectoDisciplinaViewModel?> GetById(long idProyecto, long idDisciplina);
        Task<ProyectoDisciplinaViewModel> Create(ProyectoDisciplinaViewModel model);
        Task<bool> Update(long idProyecto, long idDisciplina, ProyectoDisciplinaViewModel model);
        Task<bool> Delete(long idProyecto, long idDisciplina);
        Task<IEnumerable<ProyectoDisciplinaViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<ProyectoDisciplinaViewModel>> GetByDisciplinaId(long idDisciplina);
    }
}
