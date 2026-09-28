using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.Services.Interfaces;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ApiClienteservice : BaseService<ApiCliente, ApiClienteViewModel, long>, IApiClienteservice
    {
        public ApiClienteservice(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ApiCliente> GetQueryable()
        {
            return DbSet.AsNoTracking();
        }

        protected override ApiClienteViewModel MapToViewModel(ApiCliente entity)
        {
            return ApiClienteViewModel.ToViewModel(entity);
        }

        protected override ApiCliente MapToEntity(ApiClienteViewModel model)
        {
            return ApiClienteViewModel.ToApiClientes(model);
        }

        public async Task<ApiClienteViewModel?> GetByClientId(Guid clientId)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.ClientId == clientId);

            return entity != null ? MapToViewModel(entity) : null;
        }

        public override async Task<ApiClienteViewModel> Create(ApiClienteViewModel model)
        {
            if (model.ClientId != Guid.Empty)
            {
                var existeClientId = await DbSet.AnyAsync(x => x.ClientId == model.ClientId);
                if (existeClientId)
                {
                    throw new InvalidOperationException("Ya existe un cliente API registrado con ese ClientId.");
                }
            }

            var entity = MapToEntity(model);

            if (entity.ClientId == Guid.Empty)
            {
                entity.ClientId = Guid.NewGuid();
            }

            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, ApiClienteViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            entity.Nombre = model.Nombre;
            entity.Activo = model.Activo;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}