using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IUsuarioService : IService<UsuarioViewModel, long>
    {
        Task<UsuarioViewModel?> GetByEmail(string email);
        Task<UsuarioViewModel?> GetByDocumento(string numeroDocumento);
        Task<IEnumerable<UsuarioViewModel>> GetByInstitucionId(long idInstitucion);
        Task<IEnumerable<UsuarioViewModel>> GetByUnidadAcademicaId(long idUnidadAcademica);
        Task<bool> CambiarEstadoActivo(long id, bool activo);
        Task<bool> RegistrarUltimoAcceso(long id);
    }
}
