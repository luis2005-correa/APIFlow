using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ComentarioService : BaseService<Comentario, ComentarioViewModel, long>, IComentarioService
    {
        public ComentarioService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Comentario> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdFaseNavigation)
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.IdActividadNavigation)
                .Include(x => x.IdUsuarioNavigation);
        }

        protected override ComentarioViewModel MapToViewModel(Comentario entity)
        {
            return ComentarioViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreFase: entity.IdFaseNavigation?.Nombre,
                nombreTarea: entity.IdTareaNavigation?.Descripcion,
                nombreActividad: entity.IdActividadNavigation?.Descripcion,
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? ""
            );
        }

        protected override Comentario MapToEntity(ComentarioViewModel model)
        {
            return ComentarioViewModel.ToComentario(model);
        }

        public override async Task<ComentarioViewModel> Create(ComentarioViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Eliminado ??= false;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, ComentarioViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FindAsync(id);
            if (entity == null || entity.Eliminado == true) return false;

            entity.Contenido = model.Contenido;
            entity.Menciones = model.Menciones;
            entity.EditadoEn = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        // Borrado lógico para preservar la jerarquía de respuestas
        public override async Task<bool> Delete(long id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return false;

            entity.Eliminado = true;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ComentarioViewModel>> GetByProyectoId(long idProyecto)
        {
            var comentarios = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto && x.Eliminado != true)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return comentarios.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ComentarioViewModel>> GetByTareaId(long idTarea)
        {
            var comentarios = await GetQueryable()
                .Where(x => x.IdTarea == idTarea && x.Eliminado != true)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return comentarios.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ComentarioViewModel>> GetByActividadId(long idActividad)
        {
            var comentarios = await GetQueryable()
                .Where(x => x.IdActividad == idActividad && x.Eliminado != true)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return comentarios.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ComentarioViewModel>> GetRespuestas(long idComentarioPadre)
        {
            var comentarios = await GetQueryable()
                .Where(x => x.IdComentarioPadre == idComentarioPadre && x.Eliminado != true)
                .OrderBy(x => x.FechaCreacion)
                .ToListAsync();

            return comentarios.Select(MapToViewModel);
        }
    }
}
