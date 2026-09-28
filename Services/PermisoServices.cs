using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class PermisoService : BaseService<Permiso, PermisoViewModel, long>, IPermisoService
    {
        public PermisoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Permiso> GetQueryable()
        {
            return DbSet.AsNoTracking();
        }

        protected override PermisoViewModel MapToViewModel(Permiso entity)
        {
            return PermisoViewModel.ToViewModel(entity);
        }

        protected override Permiso MapToEntity(PermisoViewModel model)
        {
            return PermisoViewModel.ToPermiso(model);
        }

        public async Task<PermisoViewModel?> GetByClave(string clave)
        {
            var permiso = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Clave == clave);

            return permiso != null ? MapToViewModel(permiso) : null;
        }
    }
}