using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class EntidadFinanciadoraService : BaseService<EntidadFinanciadora, EntidadFinanciadoraViewModel, long>, IEntidadFinanciadoraService
    {
        public EntidadFinanciadoraService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<EntidadFinanciadora> GetQueryable()
        {
            return DbSet.AsNoTracking();
        }

        protected override EntidadFinanciadoraViewModel MapToViewModel(EntidadFinanciadora entity)
        {
            return EntidadFinanciadoraViewModel.ToViewModel(entity);
        }

        protected override EntidadFinanciadora MapToEntity(EntidadFinanciadoraViewModel model)
        {
            return EntidadFinanciadoraViewModel.ToEntidadFinanciadora(model);
        }

        public override async Task<EntidadFinanciadoraViewModel> Create(EntidadFinanciadoraViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return MapToViewModel(entity);
        }

        public async Task<IEnumerable<EntidadFinanciadoraViewModel>> GetActivas()
        {
            var entidades = await GetQueryable()
                .Where(x => x.Activo == true)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return entidades.Select(MapToViewModel);
        }

        public async Task<EntidadFinanciadoraViewModel?> GetByNit(string nit)
        {
            var entidad = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Nit == nit);

            return entidad != null ? MapToViewModel(entidad) : null;
        }
    }
}