using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class InstitucionService : BaseService<Institucion, InstitucionViewModel, long>, IInstitucionService
    {
        public InstitucionService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Institucion> GetQueryable()
        {
            return DbSet.AsNoTracking();
        }

        protected override InstitucionViewModel MapToViewModel(Institucion entity)
        {
            return InstitucionViewModel.ToViewModel(entity);
        }

        protected override Institucion MapToEntity(InstitucionViewModel model)
        {
            return InstitucionViewModel.ToInstitucion(model);
        }

        public override async Task<InstitucionViewModel> Create(InstitucionViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, InstitucionViewModel model)
        {
            if (id != model.Id) return false;

            var existe = await DbSet.AnyAsync(x => x.Id == id);
            if (!existe) return false;

            var entity = MapToEntity(model);
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            Context.Entry(entity).State = EntityState.Modified;
            Context.Entry(entity).Property(x => x.FechaCreacion).IsModified = false;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InstitucionViewModel>> GetActivas()
        {
            var instituciones = await GetQueryable()
                .Where(x => x.Activo == true)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return instituciones.Select(MapToViewModel);
        }

        public async Task<InstitucionViewModel?> GetByNit(string nit)
        {
            var institucion = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Nit == nit);

            return institucion != null ? MapToViewModel(institucion) : null;
        }
    }
}