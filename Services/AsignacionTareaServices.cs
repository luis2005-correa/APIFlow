using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class AsignacionTareaService
            : BaseService<AsignacionTarea, AsignacionTareaViewModel, (long idTarea, long idUsuario)>, IAsignacionTareaService
    {
        public AsignacionTareaService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<AsignacionTarea> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.IdUsuarioNavigation)
                .Include(x => x.AsignadoPorNavigation);
        }

        protected override AsignacionTareaViewModel MapToViewModel(AsignacionTarea entity)
        {
            return AsignacionTareaViewModel.ToViewModel(
                entity,
                nombreTarea: entity.IdTareaNavigation?.Descripcion ?? "",
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? "",
                nombreUsuarioAsigno: entity.AsignadoPorNavigation?.NombreCompleto
            );
        }

        protected override AsignacionTarea MapToEntity(AsignacionTareaViewModel model)
        {
            return AsignacionTareaViewModel.ToAsignacionTarea(model);
        }

        public override async Task<AsignacionTareaViewModel?> GetById((long idTarea, long idUsuario) key)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.IdTarea == key.idTarea && x.IdUsuario == key.idUsuario);

            return entity != null ? MapToViewModel(entity) : null;
        }

        public override async Task<AsignacionTareaViewModel> Create(AsignacionTareaViewModel model)
        {
            var entity = MapToEntity(model);
            entity.AsignadoEn = DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById((entity.IdTarea, entity.IdUsuario)) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update((long idTarea, long idUsuario) key, AsignacionTareaViewModel model)
        {
            if (key.idTarea != model.IdTarea || key.idUsuario != model.IdUsuario)
                return false;

            var entity = await DbSet.FindAsync(key.idTarea, key.idUsuario);
            if (entity == null) return false;

            entity.HorasAsignadas = model.HorasAsignadas;
            entity.AsignadoPor = model.AsignadoPor;

            await Context.SaveChangesAsync();
            return true;
        }

        public override async Task<bool> Delete((long idTarea, long idUsuario) key)
        {
            var entity = await DbSet.FindAsync(key.idTarea, key.idUsuario);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AsignacionTareaViewModel>> GetByTareaId(long idTarea)
        {
            var asignaciones = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .ToListAsync();

            return asignaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<AsignacionTareaViewModel>> GetByUsuarioId(long idUsuario)
        {
            var asignaciones = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .ToListAsync();

            return asignaciones.Select(MapToViewModel);
        }
    }
}
