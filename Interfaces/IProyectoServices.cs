using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IProyectoService : IService<ProyectoViewModel, long>
    {
        Task<ProyectoViewModel?> GetByCodigo(string codigo);
        Task<IEnumerable<ProyectoViewModel>> GetByEstado(string estado);
        Task<IEnumerable<ProyectoViewModel>> GetByLiderId(long idLider);
        Task<IEnumerable<ProyectoViewModel>> GetByInstitucionId(long idInstitucion);
        Task<bool> CambiarEstado(long idProyecto, string nuevoEstado, string? justificacion = null, long? usuarioId = null);
        Task<bool> SoftDelete(long id);
    }
}
