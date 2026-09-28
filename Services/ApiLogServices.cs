using Microsoft.EntityFrameworkCore;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Services.Interfaces;

namespace AcademiaFlowAPI.Services
{
    public class ApiLogService : BaseService<ApiLog, ApiLogViewModel, long>, IApiLogService
    {
        public ApiLogService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ApiLog> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdUsuarioNavigation);
        }

        protected override ApiLogViewModel MapToViewModel(ApiLog entity)
        {
            return ApiLogViewModel.ToViewModel(
                entity,
                nombreCliente: entity.IdClienteNavigation?.Nombre ?? "",
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? ""
            );
        }

        protected override ApiLog MapToEntity(ApiLogViewModel model)
        {
            return ApiLogViewModel.ToApiLog(model);
        }

        public override async Task<ApiLogViewModel> Create(ApiLogViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ApiLogViewModel>> GetByClienteId(long idCliente)
        {
            var logs = await GetQueryable()
                .Where(x => x.IdCliente == idCliente)
                .ToListAsync();

            return logs.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ApiLogViewModel>> GetByUsuarioId(long idUsuario)
        {
            var logs = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .ToListAsync();

            return logs.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ApiLogViewModel>> GetByStatusCode(short statusCode)
        {
            var logs = await GetQueryable()
                .Where(x => x.StatusCode == statusCode)
                .ToListAsync();

            return logs.Select(MapToViewModel);
        }
    }
}