using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaFlowAPI.Services
{
    public class ActividadService : BaseService<Actividad, ActividadViewModel, long>, IActividadService
    {
        public ActividadService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Actividad> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(a => a.IdTareaNavigation)
                .Include(a => a.IdResponsableNavigation);
        }

        protected override ActividadViewModel MapToViewModel(Actividad entity)
        {
            return ActividadViewModel.ToViewModel(
                entity,
                nombreTarea: entity.IdTareaNavigation?.Descripcion ?? "",
                nombreResponsable: entity.IdResponsableNavigation?.NombreCompleto ?? ""
            );
        }

        protected override Actividad MapToEntity(ActividadViewModel model)
        {
            return ActividadViewModel.ToActividad(model);
        }

        public override async Task<ActividadViewModel> Create(ActividadViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, ActividadViewModel model)
        {
            if (id != model.Id) return false;

            var existe = await DbSet.AnyAsync(a => a.Id == id);
            if (!existe) return false;

            var entity = MapToEntity(model);
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            Context.Entry(entity).State = EntityState.Modified;
            Context.Entry(entity).Property(x => x.FechaCreacion).IsModified = false;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ActividadViewModel>> GetByTareaId(long idTarea)
        {
            var entidades = await GetQueryable()
                .Where(a => a.IdTarea == idTarea)
                .ToListAsync();

            return entidades.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ActividadViewModel>> GetByResponsableId(long idResponsable)
        {
            var entidades = await GetQueryable()
                .Where(a => a.IdResponsable == idResponsable)
                .ToListAsync();

            return entidades.Select(MapToViewModel);
        }
    }
}