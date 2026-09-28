using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProyectoEstadoHistorialService : BaseService<ProyectoEstadoHistorial, ProyectoEstadoHistorialViewModel, long>, IProyectoEstadoHistorialService
    {
        public ProyectoEstadoHistorialService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ProyectoEstadoHistorial> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.CambiadoPorNavigation)
                .Include(x => x.IdResponsableVerificacionNavigation);
        }

        protected override ProyectoEstadoHistorialViewModel MapToViewModel(ProyectoEstadoHistorial entity)
        {
            return ProyectoEstadoHistorialViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreCambiadoPor: entity.CambiadoPorNavigation?.NombreCompleto,
                nombreResponsableVerificacion: entity.IdResponsableVerificacionNavigation?.NombreCompleto
            );
        }

        protected override ProyectoEstadoHistorial MapToEntity(ProyectoEstadoHistorialViewModel model)
        {
            return ProyectoEstadoHistorialViewModel.ToProyectoEstadoHistorial(model);
        }

        public override async Task<ProyectoEstadoHistorialViewModel> Create(ProyectoEstadoHistorialViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCambio = DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ProyectoEstadoHistorialViewModel>> GetByProyectoId(long idProyecto)
        {
            var historial = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.FechaCambio)
                .ToListAsync();

            return historial.Select(MapToViewModel);
        }

        public async Task<ProyectoEstadoHistorialViewModel?> GetUltimoCambioByProyectoId(long idProyecto)
        {
            var ultimoCambio = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.FechaCambio)
                .FirstOrDefaultAsync();

            return ultimoCambio != null ? MapToViewModel(ultimoCambio) : null;
        }
    }
}
