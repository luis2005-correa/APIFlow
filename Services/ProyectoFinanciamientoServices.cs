using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProyectoFinanciamientoService : BaseService<ProyectoFinanciamiento, ProyectoFinanciamientoViewModel, long>, IProyectoFinanciamientoService
    {
        public ProyectoFinanciamientoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ProyectoFinanciamiento> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdEntidadNavigation);
        }

        protected override ProyectoFinanciamientoViewModel MapToViewModel(ProyectoFinanciamiento entity)
        {
            return ProyectoFinanciamientoViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreEntidad: entity.IdEntidadNavigation?.Nombre ?? ""
            );
        }

        protected override ProyectoFinanciamiento MapToEntity(ProyectoFinanciamientoViewModel model)
        {
            return ProyectoFinanciamientoViewModel.ToProyectoFinanciamiento(model);
        }

        public override async Task<ProyectoFinanciamientoViewModel> Create(ProyectoFinanciamientoViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Moneda ??= "COP";

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ProyectoFinanciamientoViewModel>> GetByProyectoId(long idProyecto)
        {
            var financiamientos = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.FechaAprobacion)
                .ToListAsync();

            return financiamientos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoFinanciamientoViewModel>> GetByEntidadId(long idEntidad)
        {
            var financiamientos = await GetQueryable()
                .Where(x => x.IdEntidad == idEntidad)
                .OrderByDescending(x => x.FechaAprobacion)
                .ToListAsync();

            return financiamientos.Select(MapToViewModel);
        }

        public async Task<decimal> GetMontoTotalByProyectoId(long idProyecto)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.IdProyecto == idProyecto)
                .SumAsync(x => x.Monto);
        }
    }
}

