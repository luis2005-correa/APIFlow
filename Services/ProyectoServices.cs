using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProyectoService : BaseService<Proyecto, ProyectoViewModel, long>, IProyectoService
    {
        public ProyectoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Proyecto> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Where(x => x.EliminadoEn == null)
                .Include(x => x.IdInstitucionNavigation)
                .Include(x => x.IdUnidadAcademicaNavigation)
                .Include(x => x.IdLiderNavigation)
                .Include(x => x.CreadoPorNavigation)
                .Include(x => x.ActualizadoPorNavigation);
        }

        protected override ProyectoViewModel MapToViewModel(Proyecto entity)
        {
            return ProyectoViewModel.ToViewModel(
                entity,
                nombreInstitucion: entity.IdInstitucionNavigation?.Nombre ?? "",
                nombreUnidadAcademica: entity.IdUnidadAcademicaNavigation?.Nombre,
                nombreLider: entity.IdLiderNavigation?.NombreCompleto ?? "",
                nombreCreadoPor: entity.CreadoPorNavigation?.NombreCompleto,
                nombreActualizadoPor: entity.ActualizadoPorNavigation?.NombreCompleto
            );
        }

        protected override Proyecto MapToEntity(ProyectoViewModel model)
        {
            return ProyectoViewModel.ToProyecto(model);
        }

        public override async Task<ProyectoViewModel> Create(ProyectoViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Estado ??= "Registrado";
            entity.PorcentajeAvance ??= 0;
            entity.Moneda ??= "COP";

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, ProyectoViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.EliminadoEn == null);
            if (entity == null) return false;

            entity.Codigo = model.Codigo;
            entity.Nombre = model.Nombre;
            entity.Descripcion = model.Descripcion;
            entity.Justificacion = model.Justificacion;
            entity.ObjetivoGeneral = model.ObjetivoGeneral;
            entity.Resumen = model.Resumen;
            entity.PalabrasClave = model.PalabrasClave;
            entity.IdInstitucion = model.IdInstitucion;
            entity.IdUnidadAcademica = model.IdUnidadAcademica;
            entity.IdLider = model.IdLider;
            entity.Estado = model.Estado;
            entity.OrigenFinanciamiento = model.OrigenFinanciamiento;
            entity.PresupuestoTotal = model.PresupuestoTotal;
            entity.Moneda = model.Moneda;
            entity.FechaInicioPlan = model.FechaInicioPlan;
            entity.FechaFinPlan = model.FechaFinPlan;
            entity.FechaInicioReal = model.FechaInicioReal;
            entity.FechaFinReal = model.FechaFinReal;
            entity.PorcentajeAvance = model.PorcentajeAvance;
            entity.JustificacionCancelacion = model.JustificacionCancelacion;
            entity.ActualizadoPor = model.ActualizadoPor;
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<ProyectoViewModel?> GetByCodigo(string codigo)
        {
            var proyecto = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Codigo == codigo);

            return proyecto != null ? MapToViewModel(proyecto) : null;
        }

        public async Task<IEnumerable<ProyectoViewModel>> GetByEstado(string estado)
        {
            var proyectos = await GetQueryable()
                .Where(x => x.Estado == estado)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return proyectos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoViewModel>> GetByLiderId(long idLider)
        {
            var proyectos = await GetQueryable()
                .Where(x => x.IdLider == idLider)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return proyectos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProyectoViewModel>> GetByInstitucionId(long idInstitucion)
        {
            var proyectos = await GetQueryable()
                .Where(x => x.IdInstitucion == idInstitucion)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return proyectos.Select(MapToViewModel);
        }

        public async Task<bool> CambiarEstado(long idProyecto, string nuevoEstado, string? justificacion = null, long? usuarioId = null)
        {
            var proyecto = await DbSet.FirstOrDefaultAsync(x => x.Id == idProyecto && x.EliminadoEn == null);
            if (proyecto == null) return false;

            proyecto.Estado = nuevoEstado;
            proyecto.ActualizadoPor = usuarioId;
            proyecto.FechaActualizacion = DateTimeOffset.UtcNow;

            if (!string.IsNullOrWhiteSpace(justificacion))
            {
                proyecto.JustificacionCancelacion = justificacion;
            }

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDelete(long id)
        {
            var proyecto = await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.EliminadoEn == null);
            if (proyecto == null) return false;

            proyecto.EliminadoEn = DateTimeOffset.UtcNow;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
