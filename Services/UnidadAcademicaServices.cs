using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class UnidadAcademicaService : BaseService<UnidadAcademica, UnidadAcademicaViewModel, long>, IUnidadAcademicaService
    {
        public UnidadAcademicaService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<UnidadAcademica> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdInstitucionNavigation)
                .Include(x => x.IdPadreNavigation);
        }

        protected override UnidadAcademicaViewModel MapToViewModel(UnidadAcademica entity)
        {
            return UnidadAcademicaViewModel.ToViewModel(
                entity,
                nombreInstitucion: entity.IdInstitucionNavigation?.Nombre ?? "",
                nombreUnidadPadre: entity.IdPadreNavigation?.Nombre
            );
        }

        protected override UnidadAcademica MapToEntity(UnidadAcademicaViewModel model)
        {
            return UnidadAcademicaViewModel.ToUnidadAcademica(model);
        }

        public override async Task<UnidadAcademicaViewModel> Create(UnidadAcademicaViewModel model)
        {
            if (model.IdPadre.HasValue)
            {
                var padreExiste = await DbSet.AnyAsync(x => x.Id == model.IdPadre.Value);
                if (!padreExiste)
                {
                    throw new InvalidOperationException("La unidad académica padre especificada no existe.");
                }
            }

            var entity = MapToEntity(model);
            entity.FechaCreacion = DateTimeOffset.UtcNow;
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public override async Task<bool> Update(long id, UnidadAcademicaViewModel model)
        {
            if (id != model.Id) return false;

            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            if (model.IdPadre.HasValue)
            {
                if (model.IdPadre.Value == id)
                {
                    throw new InvalidOperationException("Una unidad académica no puede ser padre de sí misma.");
                }

                if (await EsAncestroDirectoOIndirecto(id, model.IdPadre.Value))
                {
                    throw new InvalidOperationException("Asignar esta unidad padre genera un ciclo jerárquico no permitido.");
                }
            }

            entity.IdInstitucion = model.IdInstitucion;
            entity.IdPadre = model.IdPadre;
            entity.Nombre = model.Nombre;
            entity.Tipo = model.Tipo;
            entity.Activo = model.Activo;
            entity.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UnidadAcademicaViewModel>> GetByInstitucionId(long idInstitucion)
        {
            var unidades = await GetQueryable()
                .Where(x => x.IdInstitucion == idInstitucion)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return unidades.Select(MapToViewModel);
        }

        public async Task<IEnumerable<UnidadAcademicaViewModel>> GetHijos(long idPadre)
        {
            var unidades = await GetQueryable()
                .Where(x => x.IdPadre == idPadre)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return unidades.Select(MapToViewModel);
        }

        public async Task<IEnumerable<UnidadAcademicaViewModel>> GetByTipo(string tipo)
        {
            var unidades = await GetQueryable()
                .Where(x => x.Tipo == tipo)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return unidades.Select(MapToViewModel);
        }

        public async Task<bool> CambiarEstadoActivo(long id, bool activo)
        {
            var unidad = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (unidad == null) return false;

            unidad.Activo = activo;
            unidad.FechaActualizacion = DateTimeOffset.UtcNow;

            await Context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> EsAncestroDirectoOIndirecto(long unidadId, long posiblePadreId)
        {
            var actualId = (long?)posiblePadreId;

            while (actualId.HasValue)
            {
                if (actualId.Value == unidadId) return true;

                actualId = await DbSet.AsNoTracking()
                    .Where(x => x.Id == actualId.Value)
                    .Select(x => x.IdPadre)
                    .FirstOrDefaultAsync();
            }

            return false;
        }
    }
}