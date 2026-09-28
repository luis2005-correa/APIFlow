using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class VerificacionService : BaseService<Verificacion, VerificacionViewModel, long>, IVerificacionService
    {
        public VerificacionService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Verificacion> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdFaseNavigation)
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.AsignadoPorNavigation);
        }

        protected override VerificacionViewModel MapToViewModel(Verificacion entity)
        {
            return VerificacionViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreFase: entity.IdFaseNavigation?.Nombre,
                descripcionTarea: entity.IdTareaNavigation?.Descripcion,
                nombreAsignadoPor: entity.AsignadoPorNavigation?.NombreCompleto
            );
        }

        protected override Verificacion MapToEntity(VerificacionViewModel model)
        {
            return VerificacionViewModel.ToVerificacion(model);
        }

        public override async Task<VerificacionViewModel> Create(VerificacionViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaAsignacion ??= DateTimeOffset.UtcNow;
            entity.Resultado ??= "Pendiente";

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, VerificacionViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            entity.IdProyecto = model.IdProyecto;
            entity.IdFase = model.IdFase;
            entity.IdTarea = model.IdTarea;
            entity.Tipo = model.Tipo;
            entity.AsignadoPor = model.AsignadoPor;
            entity.Resultado = model.Resultado;
            entity.Observaciones = model.Observaciones;
            entity.FechaLimite = model.FechaLimite;
            entity.FechaVerificacion = model.FechaVerificacion;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VerificacionViewModel>> GetByProyectoId(long idProyecto)
        {
            var verificaciones = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .OrderByDescending(x => x.FechaAsignacion)
                .ToListAsync();

            return verificaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<VerificacionViewModel>> GetByFaseId(long idFase)
        {
            var verificaciones = await GetQueryable()
                .Where(x => x.IdFase == idFase)
                .OrderByDescending(x => x.FechaAsignacion)
                .ToListAsync();

            return verificaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<VerificacionViewModel>> GetByTareaId(long idTarea)
        {
            var verificaciones = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .OrderByDescending(x => x.FechaAsignacion)  
                .ToListAsync();

            return verificaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<VerificacionViewModel>> GetByAsignadoPorId(long idUsuario)
        {
            var verificaciones = await GetQueryable()
                .Where(x => x.AsignadoPor == idUsuario)
                .OrderByDescending(x => x.FechaAsignacion)
                .ToListAsync();

            return verificaciones.Select(MapToViewModel);
        }

        public async Task<bool> RegistrarResultado(long idVerificacion, string resultado, string? observaciones)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == idVerificacion);
            if (entity == null) return false;

            entity.Resultado = resultado;
            entity.Observaciones = observaciones;
            entity.FechaVerificacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }
    }
}