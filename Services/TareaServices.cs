using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class TareaService : BaseService<Tarea, TareaViewModel, long>, ITareaService
    {
        public TareaService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Tarea> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdFaseNavigation)
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdResponsableNavigation);
        }

        protected override TareaViewModel MapToViewModel(Tarea entity)
        {
            return TareaViewModel.ToViewModel(
                entity,
                nombreFase: entity.IdFaseNavigation?.Nombre ?? "",
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreResponsable: entity.IdResponsableNavigation?.NombreCompleto
            );
        }

        protected override Tarea MapToEntity(TareaViewModel model)
        {
            return TareaViewModel.ToTarea(model);
        }

        public override async Task<TareaViewModel> Create(TareaViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Estado ??= "Pendiente";
            entity.Prioridad ??= "Media";
            entity.PorcentajeAvance ??= 0;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, TareaViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            entity.IdFase = model.IdFase;
            entity.IdProyecto = model.IdProyecto;
            entity.Descripcion = model.Descripcion;
            entity.Orden = model.Orden;
            entity.Prioridad = model.Prioridad;
            entity.Estado = model.Estado;
            entity.IdResponsable = model.IdResponsable;
            entity.FechaInicioPlan = model.FechaInicioPlan;
            entity.FechaFinPlan = model.FechaFinPlan;
            entity.FechaInicioReal = model.FechaInicioReal;
            entity.FechaFinReal = model.FechaFinReal;
            entity.HorasEstimadas = model.HorasEstimadas;
            entity.PorcentajeAvance = model.PorcentajeAvance;
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TareaViewModel>> GetByProyectoId(long idProyecto)
        {
            var tareas = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderBy(x => x.Orden)
                .ThenBy(x => x.FechaCreacion)
                .ToListAsync();

            return tareas.Select(MapToViewModel);
        }

        public async Task<IEnumerable<TareaViewModel>> GetByFaseId(long idFase)
        {
            var tareas = await GetQueryable()
                .Where(x => x.IdFase == idFase)
                .OrderBy(x => x.Orden)
                .ToListAsync();

            return tareas.Select(MapToViewModel);
        }

        public async Task<IEnumerable<TareaViewModel>> GetByResponsableId(long idResponsable)
        {
            var tareas = await GetQueryable()
                .Where(x => x.IdResponsable == idResponsable)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return tareas.Select(MapToViewModel);
        }

        public async Task<IEnumerable<TareaViewModel>> GetByEstado(string estado)
        {
            var tareas = await GetQueryable()
                .Where(x => x.Estado == estado)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return tareas.Select(MapToViewModel);
        }

        public async Task<bool> CambiarEstado(long idTarea, string nuevoEstado)
        {
            var tarea = await DbSet.FirstOrDefaultAsync(x => x.Id == idTarea);
            if (tarea == null) return false;

            tarea.Estado = nuevoEstado;
            tarea.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAvance(long idTarea, decimal porcentajeAvance)
        {
            var tarea = await DbSet.FirstOrDefaultAsync(x => x.Id == idTarea);
            if (tarea == null) return false;

            tarea.PorcentajeAvance = porcentajeAvance;
            tarea.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }
    }
}
