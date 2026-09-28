using AcademiaFlowAPI.ViewModel;

namespace AcademiaFlowAPI.Interfaces
{
    public interface IUsuarioRolService : IService<UsuarioRolViewModel, (long IdUsuario, long IdRol)>
    {
        Task<IEnumerable<UsuarioRolViewModel>> GetByUsuarioId(long idUsuario);
        Task<IEnumerable<UsuarioRolViewModel>> GetByRolId(long idRol);
        Task<bool> AsignarRol(long idUsuario, long idRol, long? asignadoPor = null);
        Task<bool> RemoverRol(long idUsuario, long idRol);
        Task<bool> UsuarioTieneRol(long idUsuario, long idRol);
    }
}
