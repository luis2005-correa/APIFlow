using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class RecursoService : BaseService<Recurso, RecursoViewModel, long>, IRecursoService
    {
        public RecursoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Recurso> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation);
        }

        protected override RecursoViewModel MapToViewModel(Recurso entity)
        {
            return RecursoViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? ""
            );
        }

        protected override Recurso MapToEntity(RecursoViewModel model)
        {
            return RecursoViewModel.ToRecurso(model);
        }

        public override async Task<RecursoViewModel> Create(RecursoViewModel model)
        {
            var entity = MapToEntity(model);

            if (!entity.CostoTotal.HasValue && entity.Cantidad.HasValue && entity.CostoUnitario.HasValue)
            {
                entity.CostoTotal = entity.Cantidad.Value * entity.CostoUnitario.Value;
            }

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, RecursoViewModel model)
        {
            if (id != model.Id) return false;

            var existe = await DbSet.AnyAsync(x => x.Id == id);
            if (!existe) return false;

            var entity = MapToEntity(model);

            if (!entity.CostoTotal.HasValue && entity.Cantidad.HasValue && entity.CostoUnitario.HasValue)
            {
                entity.CostoTotal = entity.Cantidad.Value * entity.CostoUnitario.Value;
            }

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RecursoViewModel>> GetByProyectoId(long idProyecto)
        {
            var recursos = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderBy(x => x.Tipo)
                .ThenBy(x => x.Nombre)
                .ToListAsync();

            return recursos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<RecursoViewModel>> GetByTipo(string tipo)
        {
            var recursos = await GetQueryable()
                .Where(x => x.Tipo == tipo)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return recursos.Select(MapToViewModel);
        }

        public async Task<decimal> GetCostoTotalByProyectoId(long idProyecto)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdProyecto == idProyecto)
                .SumAsync(x => x.CostoTotal ?? 0);
        }
    }
}
