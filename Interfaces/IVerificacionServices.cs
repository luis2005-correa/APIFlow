using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IVerificacionService : IService<VerificacionViewModel, long>
    {
        Task<IEnumerable<VerificacionViewModel>> GetByProyectoId(long idProyecto);
        Task<IEnumerable<VerificacionViewModel>> GetByFaseId(long idFase);
        Task<IEnumerable<VerificacionViewModel>> GetByTareaId(long idTarea);
        Task<IEnumerable<VerificacionViewModel>> GetByAsignadoPorId(long idUsuario);
        Task<bool> RegistrarResultado(long idVerificacion, string resultado, string? observaciones);
    }
}
