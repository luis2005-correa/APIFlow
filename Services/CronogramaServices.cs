using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class CronogramaService : BaseService<Cronograma, CronogramaViewModel, long>, ICronogramaService
    {
        public CronogramaService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Cronograma> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.SubidoPorNavigation);
        }

        protected override CronogramaViewModel MapToViewModel(Cronograma entity)
        {
            return CronogramaViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreUsuarioSubio: entity.SubidoPorNavigation?.NombreCompleto
            );
        }

        protected override Cronograma MapToEntity(CronogramaViewModel model)
        {
            return CronogramaViewModel.ToCronograma(model);
        }

        public override async Task<CronogramaViewModel> Create(CronogramaViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Version ??= 1;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<CronogramaViewModel>> GetByProyectoId(long idProyecto)
        {
            var cronogramas = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.Version)
                .ToListAsync();

            return cronogramas.Select(MapToViewModel);
        }

        public async Task<CronogramaViewModel?> GetUltimaVersionByProyectoId(long idProyecto)
        {
            var cronograma = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();

            return cronograma != null ? MapToViewModel(cronograma) : null;
        }
    }
}