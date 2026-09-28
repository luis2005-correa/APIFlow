using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class UsuarioRolService : BaseService<UsuarioRol, UsuarioRolViewModel, (long IdUsuario, long IdRol)>, IUsuarioRolService
    {
        public UsuarioRolService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<UsuarioRol> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdUsuarioNavigation)
                .Include(x => x.IdRolNavigation)
                .Include(x => x.AsignadoPorNavigation);
        }

        protected override UsuarioRolViewModel MapToViewModel(UsuarioRol entity)
        {
            return UsuarioRolViewModel.ToViewModel(
                entity,
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? "",
                nombreRol: entity.IdRolNavigation?.Nombre ?? "",
                nombreAsignadoPor: entity.AsignadoPorNavigation?.NombreCompleto
            );
        }

        protected override UsuarioRol MapToEntity(UsuarioRolViewModel model)
        {
            return UsuarioRolViewModel.ToUsuarioRol(model);
        }

        public override async Task<UsuarioRolViewModel?> GetById((long IdUsuario, long IdRol) id)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.IdUsuario == id.IdUsuario && x.IdRol == id.IdRol);

            return entity != null ? MapToViewModel(entity) : null;
        }

        public override async Task<UsuarioRolViewModel> Create(UsuarioRolViewModel model)
        {
            var existe = await DbSet.AnyAsync(x => x.IdUsuario == model.IdUsuario && x.IdRol == model.IdRol);
            if (existe)
            {
                throw new InvalidOperationException("El usuario ya tiene asignado este rol.");
            }

            var entity = MapToEntity(model);
            entity.AsignadoEn ??= DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById((entity.IdUsuario, entity.IdRol)) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update((long IdUsuario, long IdRol) id, UsuarioRolViewModel model)
        {
            if (id.IdUsuario != model.IdUsuario || id.IdRol != model.IdRol) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.IdUsuario == id.IdUsuario && x.IdRol == id.IdRol);
            if (entity == null) return false;

            entity.AsignadoPor = model.AsignadoPor;

            await Context.SaveChangesAsync();
            return true;
        }

        public override async Task<bool> Delete((long IdUsuario, long IdRol) id)
        {
            return await RemoverRol(id.IdUsuario, id.IdRol);
        }

        public async Task<IEnumerable<UsuarioRolViewModel>> GetByUsuarioId(long idUsuario)
        {
            var roles = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .OrderBy(x => x.IdRolNavigation.Nombre)
                .ToListAsync();

            return roles.Select(MapToViewModel);
        }

        public async Task<IEnumerable<UsuarioRolViewModel>> GetByRolId(long idRol)
        {
            var usuarios = await GetQueryable()
                .Where(x => x.IdRol == idRol)
                .OrderBy(x => x.IdUsuarioNavigation.NombreCompleto)
                .ToListAsync();

            return usuarios.Select(MapToViewModel);
        }

        public async Task<bool> AsignarRol(long idUsuario, long idRol, long? asignadoPor = null)
        {
            var existe = await DbSet.AnyAsync(x => x.IdUsuario == idUsuario && x.IdRol == idRol);
            if (existe) return false;

            var entity = new UsuarioRol
            {
                IdUsuario = idUsuario,
                IdRol = idRol,
                AsignadoPor = asignadoPor,
                AsignadoEn = DateTimeOffset.UtcNow
            };

            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverRol(long idUsuario, long idRol)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario && x.IdRol == idRol);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsuarioTieneRol(long idUsuario, long idRol)
        {
            return await DbSet.AnyAsync(x => x.IdUsuario == idUsuario && x.IdRol == idRol);
        }
    }
}