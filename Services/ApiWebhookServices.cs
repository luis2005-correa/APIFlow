using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ApiWebhookService : BaseService<ApiWebhook, ApiWebhookViewModel, long>, IApiWebhookService
    {
        public ApiWebhookService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ApiWebhook> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdClienteNavigation);
        }

        protected override ApiWebhookViewModel MapToViewModel(ApiWebhook entity)
        {
            return ApiWebhookViewModel.ToViewModel(
                entity,
                nombreCliente: entity.IdClienteNavigation?.Nombre ?? ""
            );
        }

        protected override ApiWebhook MapToEntity(ApiWebhookViewModel model)
        {
            return ApiWebhookViewModel.ToApiWebhook(model);
        }

        public override async Task<ApiWebhookViewModel> Create(ApiWebhookViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ApiWebhookViewModel>> GetByClienteId(long idCliente)
        {
            var webhooks = await GetQueryable()
                .Where(x => x.IdCliente == idCliente)
                .ToListAsync();

            return webhooks.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ApiWebhookViewModel>> GetByEvento(string evento)
        {
            var webhooks = await GetQueryable()
                .Where(x => x.Evento.ToLower() == evento.ToLower())
                .ToListAsync();

            return webhooks.Select(MapToViewModel);
        }

        public override async Task<bool> Delete(long id)
        {
            var webhook = await DbSet.FindAsync(id);
            if (webhook == null) return false;

            webhook.Activo = false;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
