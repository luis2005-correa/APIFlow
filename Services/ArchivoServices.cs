using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ArchivoService : BaseService<Archivo, ArchivoViewModel, long>, IArchivoService
    {
        public ArchivoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Archivo> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdProyectoNavigation)
                .Include(x => x.IdFaseNavigation)
                .Include(x => x.IdTareaNavigation)
                .Include(x => x.SubidoPorNavigation);
        }

        protected override ArchivoViewModel MapToViewModel(Archivo entity)
        {
            return ArchivoViewModel.ToViewModel(
                entity,
                nombreProyecto: entity.IdProyectoNavigation?.Nombre ?? "",
                nombreFase: entity.IdFaseNavigation?.Nombre,
                nombreTarea: entity.IdTareaNavigation?.Descripcion,
                nombreUsuarioSubio: entity.SubidoPorNavigation?.NombreCompleto
            );
        }

        protected override Archivo MapToEntity(ArchivoViewModel model)
        {
            return ArchivoViewModel.ToArchivo(model);
        }

        public override async Task<ArchivoViewModel> Create(ArchivoViewModel model)
        {
            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.EsVigente ??= true;
            entity.Version ??= 1;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ArchivoViewModel>> GetByProyectoId(long idProyecto)
        {
            var archivos = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto)
                .ToListAsync();

            return archivos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ArchivoViewModel>> GetByTareaId(long idTarea)
        {
            var archivos = await GetQueryable()
                .Where(x => x.IdTarea == idTarea)
                .ToListAsync();

            return archivos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ArchivoViewModel>> GetByFaseId(long idFase)
        {
            var archivos = await GetQueryable()
                .Where(x => x.IdFase == idFase)
                .ToListAsync();

            return archivos.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ArchivoViewModel>> GetVigentesByProyectoId(long idProyecto)
        {
            var archivos = await GetQueryable()
                .Where(x => x.IdProyecto == idProyecto && x.EsVigente == true)
                .ToListAsync();

            return archivos.Select(MapToViewModel);
        }

        public override async Task<bool> Delete(long id)
        {
            var archivo = await DbSet.FindAsync(id);
            if (archivo == null) return false;

            archivo.EsVigente = false;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}