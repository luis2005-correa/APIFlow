using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IArchivoService : IService<ArchivoViewModel, long>
    {
        Task<IEnumerable<ArchivoViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<ArchivoViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<ArchivoViewModel>> GetByFaseId(long idFase);
        Task<IEnumerable<ArchivoViewModel>> GetVigentesByProyectoId(long idProyecto);
    }
}
