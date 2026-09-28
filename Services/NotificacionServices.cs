using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class NotificacionService : BaseService<Notificacion, NotificacionViewModel, long>, INotificacionService
    {
        public NotificacionService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Notificacion> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdUsuarioNavigation)
                .Include(x => x.IdProyectoNavigation);
        }

        protected override NotificacionViewModel MapToViewModel(Notificacion entity)
        {
            return NotificacionViewModel.ToViewModel(
                entity,
                nombreUsuario: entity.IdUsuarioNavigation?.NombreCompleto ?? "",
                nombreProyecto: entity.IdProyectoNavigation?.Nombre
            );
        }

        protected override Notificacion MapToEntity(NotificacionViewModel model)
        {
            return NotificacionViewModel.ToNotificacion(model);
        }

        public override async Task<NotificacionViewModel> Create(NotificacionViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Leida ??= false;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<NotificacionViewModel>> GetByUsuarioId(long idUsuario)
        {
            var notificaciones = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return notificaciones.Select(MapToViewModel);
        }

        public async Task<IEnumerable<NotificacionViewModel>> GetNoLeidasByUsuarioId(long idUsuario)
        {
            var notificaciones = await GetQueryable()
                .Where(x => x.IdUsuario == idUsuario && x.Leida != true)
                .OrderByDescending(x => x.FechaCreacion)
                .ToListAsync();

            return notificaciones.Select(MapToViewModel);
        }

        public async Task<bool> MarcarComoLeida(long idNotificacion)
        {
            var entity = await DbSet.FindAsync(idNotificacion);
            if (entity == null) return false;

            entity.Leida = true;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarcarTodasComoLeidas(long idUsuario)
        {
            var notificaciones = await DbSet
                .Where(x => x.IdUsuario == idUsuario && x.Leida != true)
                .ToListAsync();

            if (!notificaciones.Any()) return false;

            foreach (var notificacion in notificaciones)
            {
                notificacion.Leida = true;
            }

            await Context.SaveChangesAsync();
            return true;
        }
    }
}