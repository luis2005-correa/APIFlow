using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IUnidadAcademicaService : IService<UnidadAcademicaViewModel, long>
    {
        Task<IEnumerable<UnidadAcademicaViewModel>> GetByInstitucionId(long idInstitucion);
        Task<IEnumerable<UnidadAcademicaViewModel>> GetHijos(long idPadre);
        Task<IEnumerable<UnidadAcademicaViewModel>> GetByTipo(string tipo);
        Task<bool> CambiarEstadoActivo(long id, bool activo);
    }
}
