using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class ProgramaAcademicoService : BaseService<ProgramaAcademico, ProgramaAcademicoViewModel, long>, IProgramaAcademicoService
    {
        public ProgramaAcademicoService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<ProgramaAcademico> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdUnidadAcademicaNavigation)
                .Include(x => x.IdDisciplinaNavigation);
        }

        protected override ProgramaAcademicoViewModel MapToViewModel(ProgramaAcademico entity)
        {
            return ProgramaAcademicoViewModel.ToViewModel(
                entity,
                nombreUnidadAcademica: entity.IdUnidadAcademicaNavigation?.Nombre ?? "",
                nombreDisciplina: entity.IdDisciplinaNavigation?.Nombre
            );
        }

        protected override ProgramaAcademico MapToEntity(ProgramaAcademicoViewModel model)
        {
            return ProgramaAcademicoViewModel.ToProgramaAcademico(model);
        }

        public override async Task<ProgramaAcademicoViewModel> Create(ProgramaAcademicoViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<ProgramaAcademicoViewModel>> GetActivos()
        {
            var programas = await GetQueryable()
                .Where(x => x.Activo == true)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return programas.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProgramaAcademicoViewModel>> GetByUnidadAcademicaId(long idUnidadAcademica)
        {
            var programas = await GetQueryable()
                .Where(x => x.IdUnidadAcademica == idUnidadAcademica)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return programas.Select(MapToViewModel);
        }

        public async Task<ProgramaAcademicoViewModel?> GetByCodigoSnies(string codigoSnies)
        {
            var programa = await GetQueryable()
                .FirstOrDefaultAsync(x => x.CodigoSnies == codigoSnies);

            return programa != null ? MapToViewModel(programa) : null;
        }
    }
}
