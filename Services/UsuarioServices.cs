using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class UsuarioService : BaseService<Usuario, UsuarioViewModel, long>, IUsuarioService
    {
        public UsuarioService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Usuario> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdInstitucionNavigation)
                .Include(x => x.IdUnidadAcademicaNavigation);
        }

        protected override UsuarioViewModel MapToViewModel(Usuario entity)
        {
            return UsuarioViewModel.ToViewModel(
                entity,
                nombreInstitucion: entity.IdInstitucionNavigation?.Nombre,
                nombreUnidadAcademica: entity.IdUnidadAcademicaNavigation?.Nombre
            );
        }

        protected override Usuario MapToEntity(UsuarioViewModel model)
        {
            return UsuarioViewModel.ToUsuario(model);
        }

        public override async Task<UsuarioViewModel> Create(UsuarioViewModel model)
        {
            var emailExiste = await DbSet.AnyAsync(x => x.Email.ToLower() == model.Email.ToLower());
            if (emailExiste)
            {
                throw new InvalidOperationException("Ya existe un usuario registrado con ese correo electrónico.");
            }

            if (!string.IsNullOrWhiteSpace(model.NumeroDocumento))
            {
                var docExiste = await DbSet.AnyAsync(x => x.NumeroDocumento == model.NumeroDocumento);
                if (docExiste)
                {
                    throw new InvalidOperationException("Ya existe un usuario registrado con ese número de documento.");
                }
            }

            var entity = MapToEntity(model);
            entity.NombreCompleto = $"{model.Nombres} {model.Apellidos}".Trim();
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Activo ??= true;
            entity.EmailConfirmado ??= false;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, UsuarioViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            var emailExiste = await DbSet.AnyAsync(x => x.Email.ToLower() == model.Email.ToLower() && x.Id != id);
            if (emailExiste)
            {
                throw new InvalidOperationException("El correo electrónico ya está en uso por otro usuario.");
            }

            if (!string.IsNullOrWhiteSpace(model.NumeroDocumento))
            {
                var docExiste = await DbSet.AnyAsync(x => x.NumeroDocumento == model.NumeroDocumento && x.Id != id);
                if (docExiste)
                {
                    throw new InvalidOperationException("El número de documento ya está en uso por otro usuario.");
                }
            }

            entity.IdInstitucion = model.IdInstitucion;
            entity.IdUnidadAcademica = model.IdUnidadAcademica;
            entity.TipoDocumento = model.TipoDocumento;
            entity.NumeroDocumento = model.NumeroDocumento;
            entity.Nombres = model.Nombres;
            entity.Apellidos = model.Apellidos;
            entity.NombreCompleto = $"{model.Nombres} {model.Apellidos}".Trim();
            entity.Email = model.Email;
            entity.EmailConfirmado = model.EmailConfirmado;
            entity.Telefono = model.Telefono;
            entity.Cargo = model.Cargo;
            entity.Activo = model.Activo;
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioViewModel?> GetByEmail(string email)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return entity != null ? MapToViewModel(entity) : null;
        }

        public async Task<UsuarioViewModel?> GetByDocumento(string numeroDocumento)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.NumeroDocumento == numeroDocumento);

            return entity != null ? MapToViewModel(entity) : null;
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetByInstitucionId(long idInstitucion)
        {
            var usuarios = await GetQueryable()
                .Where(x => x.IdInstitucion == idInstitucion)
                .OrderBy(x => x.NombreCompleto)
                .ToListAsync();

            return usuarios.Select(MapToViewModel);
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetByUnidadAcademicaId(long idUnidadAcademica)
        {
            var usuarios = await GetQueryable()
                .Where(x => x.IdUnidadAcademica == idUnidadAcademica)
                .OrderBy(x => x.NombreCompleto)
                .ToListAsync();

            return usuarios.Select(MapToViewModel);
        }

        public async Task<bool> CambiarEstadoActivo(long id, bool activo)
        {
            var usuario = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null) return false;

            usuario.Activo = activo;
            usuario.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistrarUltimoAcceso(long id)
        {
            var usuario = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null) return false;

            usuario.UltimoAcceso = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }
    }
}