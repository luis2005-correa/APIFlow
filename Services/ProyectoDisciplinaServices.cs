using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProyectoDisciplinaService : IProyectoDisciplinaService
    {
        protected readonly GestionesAcademicasDbContext Context;
        protected readonly DbSet<ProyectoDisciplina> DbSet;

        public ProyectoDisciplinaService(GestionesAcademicasDbContext context)
        {
            Context = context;
            DbSet = context.Set<ProyectoDisciplina>();
        }

        private IQueryable<ProyectoDisciplina> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdDisciplinaNavigation);
        }

        private ProyectoDisciplinaViewModel MapToViewModel(ProyectoDisciplina entity)
        {
            return ProyectoDisciplinaViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreDisciplina: entity.IdDisciplinaNavigation?.Nombre ?? ""
            );
        }

        private ProyectoDisciplina MapToEntity(ProyectoDisciplinaViewModel model)
        {
            return ProyectoDisciplinaViewModel.ToProyectoDisciplina(model);
        }

        public async Task<IEnumerable<ProyectoDisciplinaViewModel>> GetAll()
        {
            var entidades = await GetQueryable().ToListAsync();
            return entidades.Select(MapToViewModel);
        }

        public async Task<ProyectoDisciplinaViewModel?> GetById(long idProyecto, long idDisciplina)
        {
            var entidad = await GetQueryable()
                .FirstOrDefaultAsync(x => x.IdProyecto == idProyecto && x.IdDisciplina == idDisciplina);

            return entidad != null ? MapToViewModel(entidad) : null;
        }

        public async Task<ProyectoDisciplinaViewModel> Create(ProyectoDisciplinaViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Principal ??= false;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.IdProyecto, entity.IdDisciplina) ?? MapToViewModel(entity);
        }

        public async Task<bool> Update(long idProyecto, long idDisciplina, ProyectoDisciplinaViewModel model)
        {
            if (idProyecto != model.IdProyecto || idDisciplina != model.IdDisciplina)
                return false;

            var existe = await DbSet.AnyAsync(x => x.IdProyecto == idProyecto && x.IdDisciplina == idDisciplina);
            if (!existe) return false;

            var entity = MapToEntity(model);
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long idProyecto, long idDisciplina)
        {
            var entity = await DbSet.FindAsync(idProyecto, idDisciplina);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProyectoDisciplinaViewModel>> GetByProyectoId(long idProyecto)
        {
            var relaciones = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .ToListAsync();

            return relaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoDisciplinaViewModel>> GetByDisciplinaId(long idDisciplina)
        {
            var relaciones = await GetQueryable()
                .Where(x => x.IdDisciplina == idDisciplina)
                .ToListAsync();

            return relaciones.Select(MapToViewModel);
        }
    }
}
