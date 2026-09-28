using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IComentarioService : IService<ComentarioViewModel, long>
    {
        Task<IEnumerable<ComentarioViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<ComentarioViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<ComentarioViewModel>> GetByActividadId(long idActividad);
        Task<IEnumerable<ComentarioViewModel>> GetRespuestas(long idComentarioPadre);
    }
}
