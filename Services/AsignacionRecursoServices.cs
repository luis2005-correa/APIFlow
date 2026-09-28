using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class AsignacionRecursoService : BaseService<AsignacionRecurso, AsignacionRecursoViewModel, long>, IAsignacionRecursoService
    {
        public AsignacionRecursoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<AsignacionRecurso> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdRecursoNavigation)
                .Include(x => x.IdFaseNavigation)
                .Include(x => x.IdTareaNavigation);
        }

        protected override AsignacionRecursoViewModel MapToViewModel(AsignacionRecurso entity)
        {
            return AsignacionRecursoViewModel.ToViewModel(
                entity,
                nombreRecurso: entity.IdRecursoNavigation?.Nombre ?? "",
                nombreFase: entity.IdFaseNavigation?.Nombre,
                nombreTarea: entity.IdTareaNavigation?.Descripcion
            );
        }

        protected override AsignacionRecurso MapToEntity(AsignacionRecursoViewModel model)
        {
            return AsignacionRecursoViewModel.ToAsignacionRecurso(model);
        }

        public async Task<IEnumerable<AsignacionRecursoViewModel>> GetByRecursoId(long idRecurso)
        {
            var asignaciones = await GetQueryable()
                .Where(x => x.IdRecurso == idRecurso)
                .ToListAsync();

            return asignaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<AsignacionRecursoViewModel>> GetByTareaId(long idTarea)
        {
            var asignaciones = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .ToListAsync();

            return asignaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<AsignacionRecursoViewModel>> GetByFaseId(long idFase)
        {
            var asignaciones = await GetQueryable()
                .Where(x => x.IdFase == idFase)
                .ToListAsync();

            return asignaciones.Select(MapToViewModel);
        }
    }
}
