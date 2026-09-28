using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class FaseService : BaseService<Fase, FaseViewModel, long>, IFaseService
    {
        public FaseService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Fase> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation);
        }

        protected override FaseViewModel MapToViewModel(Fase entity)
        {
            return FaseViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? ""
            );
        }

        protected override Fase MapToEntity(FaseViewModel model)
        {
            return FaseViewModel.ToFase(model);
        }

        public override async Task<FaseViewModel> Create(FaseViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.PorcentajeAvance ??= 0;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, FaseViewModel model)
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

        public async Task<IEnumerable<FaseViewModel>> GetByProyectoId(long idProyecto)
        {
            var fases = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderBy(x => x.Orden)
                .ThenBy(x => x.FechaInicioPlan)
                .ToListAsync();

            return fases.Select(MapToViewModel);
        }
    }
}