using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProyectoIntegranteService : BaseService<ProyectoIntegrante, ProyectoIntegranteViewModel, long>, IProyectoIntegranteService
    {
        public ProyectoIntegranteService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ProyectoIntegrante> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdUsuarioNavigation)
                .Include(x => x.IdDisciplinaNavigation);
        }

        protected override ProyectoIntegranteViewModel MapToViewModel(ProyectoIntegrante entity)
        {
            return ProyectoIntegranteViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? "",
                nombreDisciplina: entity.IdDisciplinaNavigation?.Nombre
            );
        }

        protected override ProyectoIntegrante MapToEntity(ProyectoIntegranteViewModel model)
        {
            return ProyectoIntegranteViewModel.ToProyectoIntegrante(model);
        }

        public override async Task<ProyectoIntegranteViewModel> Create(ProyectoIntegranteViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Activo ??= true;
            entity.FechaIngreso ??= DateOnly.FromDateTime(DateTime.UtcNow);

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ProyectoIntegranteViewModel>> GetByProyectoId(long idProyecto)
        {
            var integrantes = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderBy(x => x.Rol)
                .ThenBy(x => x.IdUsuarioNavigation!.NombreCompleto)
                .ToListAsync();

            return integrantes.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoIntegranteViewModel>> GetByUsuarioId(long idUsuario)
        {
            var proyectos = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .OrderByDescending(x => x.FechaIngreso)
                .ToListAsync();

            return proyectos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoIntegranteViewModel>> GetActivosByProyectoId(long idProyecto)
        {
            var integrantes = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto && x.Activo == true)
                .OrderBy(x => x.Rol)
                .ToListAsync();

            return integrantes.Select(MapToViewModel);
        }
    }
}
