using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class RegistroAvanceService : BaseService<RegistroAvance, RegistroAvanceViewModel, long>, IRegistroAvanceService
    {
        public RegistroAvanceService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<RegistroAvance> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.IdActividadNavigation)
                .Include(x => x.IdUsuarioNavigation);
        }

        protected override RegistroAvanceViewModel MapToViewModel(RegistroAvance entity)
        {
            return RegistroAvanceViewModel.ToViewModel(
                entity,
                nombreTarea: entity.IdTareaNavigation?.Descripcion,
                nombreActividad: entity.IdActividadNavigation?.Nombre,
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? ""
            );
        }

        protected override RegistroAvance MapToEntity(RegistroAvanceViewModel model)
        {
            return RegistroAvanceViewModel.ToRegistroAvance(model);
        }

        public override async Task<RegistroAvanceViewModel> Create(RegistroAvanceViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaReporte ??= DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<RegistroAvanceViewModel>> GetByTareaId(long idTarea)
        {
            var avances = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .OrderByDescending(x => x.FechaReporte)
                .ToListAsync();

            return avances.Select(MapToViewModel);
        }

        public async Task<IEnumerable<RegistroAvanceViewModel>> GetByActividadId(long idActividad)
        {
            var avances = await GetQueryable()
                .Where(x => x.IdActividad == idActividad)
                .OrderByDescending(x => x.FechaReporte)
                .ToListAsync();

            return avances.Select(MapToViewModel);
        }

        public async Task<IEnumerable<RegistroAvanceViewModel>> GetByUsuarioId(long idUsuario)
        {
            var avances = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .OrderByDescending(x => x.FechaReporte)
                .ToListAsync();

            return avances.Select(MapToViewModel);
        }

        public async Task<decimal> GetHorasTotalesByTareaId(long idTarea)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdTarea == idTarea)
                .SumAsync(x => x.HorasDedicadas);
        }

        public async Task<decimal> GetHorasTotalesByActividadId(long idActividad)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdActividad == idActividad)
                .SumAsync(x => x.HorasDedicadas);
        }
    }
}
