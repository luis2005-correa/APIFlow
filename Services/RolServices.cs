using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class RolService : BaseService<Rol, RolViewModel, long>, IRolService
    {
        public RolService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Rol> GetQueryable()
        {
            return DbSet.AsNoTracking();
        }

        protected override RolViewModel MapToViewModel(Rol entity)
        {
            return RolViewModel.ToViewModel(entity);
        }

        protected override Rol MapToEntity(RolViewModel model)
        {
            return RolViewModel.ToRol(model);
        }

        public override async Task<RolViewModel> Create(RolViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Sistema ??= false;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, RolViewModel model)
        {
            if (id != model.Id) return false;

            var rolActual = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (rolActual == null) return false;

            rolActual.Nombre = model.Nombre;
            rolActual.Descripcion = model.Descripcion;

            await Context.SaveChangesAsync();
            return true;
        }

        public override async Task<bool> Delete(long id)
        {
            var rol = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (rol == null) return false;

            if (rol.Sistema == true)
            {
                throw new InvalidOperationException("No se puede eliminar un rol del sistema.");
            }

            DbSet.Remove(rol);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<RolViewModel?> GetByNombre(string nombre)
        {
            var rol = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Nombre.ToLower() == nombre.ToLower());

            return rol != null ? MapToViewModel(rol) : null;
        }

        public async Task<IEnumerable<RolViewModel>> GetRolesSistema()
        {
            var roles = await GetQueryable()
                .Where(x => x.Sistema == true)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return roles.Select(MapToViewModel);
        }
    }
}