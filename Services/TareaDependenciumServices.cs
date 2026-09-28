using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class TareaDependenciumService : BaseService<TareaDependencium, TareaDependenciumViewModel, (long IdTarea, long IdDependeDe)>, ITareaDependenciumService
    {
        public TareaDependenciumService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<TareaDependencium> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.IdDependeDeNavigation);
        }

        protected override TareaDependenciumViewModel MapToViewModel(TareaDependencium entity)
        {
            return TareaDependenciumViewModel.ToViewModel(
                entity,
                descripcionTarea: entity.IdTareaNavigation?.Descripcion ?? "",
                descripcionDependeDe: entity.IdDependeDeNavigation?.Descripcion ?? ""
            );
        }

        protected override TareaDependencium MapToEntity(TareaDependenciumViewModel model)
        {
            return TareaDependenciumViewModel.ToTareaDependencium(model);
        }

        public override async Task<TareaDependenciumViewModel?> GetById((long IdTarea, long IdDependeDe) id)
        {
            var entity = await GetQueryable()
                .FirstOrDefaultAsync(x => x.IdTarea == id.IdTarea && x.IdDependeDe == id.IdDependeDe);

            return entity != null ? MapToViewModel(entity) : null;
        }

        public override async Task<TareaDependenciumViewModel> Create(TareaDependenciumViewModel model)
        {
            // Validación de autoreferencia
            if (model.IdTarea == model.IdDependeDe)
            {
                throw new InvalidOperationException("Una tarea no puede depender de sí misma.");
            }

            // Validación de circularidad
            if (await ExisteDependenciaCircular(model.IdTarea, model.IdDependeDe))
            {
                throw new InvalidOperationException("La relación crea una dependencia circular entre tareas.");
            }

            var entity = MapToEntity(model);
            entity.Tipo ??= "FS";
            entity.DesfaseDias ??= 0;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById((entity.IdTarea, entity.IdDependeDe)) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update((long IdTarea, long IdDependeDe) id, TareaDependenciumViewModel model)
        {
            if (id.IdTarea != model.IdTarea || id.IdDependeDe != model.IdDependeDe) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.IdTarea == id.IdTarea && x.IdDependeDe == id.IdDependeDe);
            if (entity == null) return false;

            entity.Tipo = model.Tipo;
            entity.DesfaseDias = model.DesfaseDias;

            await Context.SaveChangesAsync();
            return true;
        }

        public override async Task<bool> Delete((long IdTarea, long IdDependeDe) id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.IdTarea == id.IdTarea && x.IdDependeDe == id.IdDependeDe);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TareaDependenciumViewModel>> GetDependenciasByTareaId(long idTarea)
        {
            var dependencias = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .ToListAsync();

            return dependencias.Select(MapToViewModel);
        }

        public async Task<IEnumerable<TareaDependenciumViewModel>> GetTareasQueDependenDe(long idDependeDe)
        {
            var dependencias = await GetQueryable()
                .Where(x => x.IdDependeDe == idDependeDe)
                .ToListAsync();

            return dependencias.Select(MapToViewModel);
        }

        public async Task<bool> ExisteDependenciaCircular(long idTarea, long idDependeDe)
        {
            var visitados = new HashSet<long>();
            var cola = new Queue<long>();
            cola.Enqueue(idDependeDe);

            while (cola.Count > 0)
            {
                var actual = cola.Dequeue();
                if (actual == idTarea) return true;

                if (visitados.Add(actual))
                {
                    var dependenciasPadre = await DbSet.AsNoTracking()
                        .Where(x => x.IdTarea == actual)
                        .Select(x => x.IdDependeDe)
                        .ToListAsync();

                    foreach (var dep in dependenciasPadre)
                    {
                        cola.Enqueue(dep);
                    }
                }
            }

            return false;
        }
    }
}
