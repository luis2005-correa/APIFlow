using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Models;
using AcademiaFlowAPI.Services.Common;
using AcademiaFlowAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Services
{
    public class DisciplinaService : BaseService<Disciplina, DisciplinaViewModel, long>, IDisciplinaService
    {
        public DisciplinaService(GestionesAcademicasDbContext context) : base(context)
        {
        }

        protected override IQueryable<Disciplina> GetQueryable()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.IdPadreNavigation);
        }

        protected override DisciplinaViewModel MapToViewModel(Disciplina entity)
        {
            return DisciplinaViewModel.ToViewModel(
                entity,
                nombreDisciplinaPadre: entity.IdPadreNavigation?.Nombre
            );
        }

        protected override Disciplina MapToEntity(DisciplinaViewModel model)
        {
            return DisciplinaViewModel.ToDisciplina(model);
        }

        public override async Task<DisciplinaViewModel> Create(DisciplinaViewModel model)
        {
            var entity = MapToEntity(model);
            entity.Activo ??= true;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return await GetById(entity.Id) ?? MapToViewModel(entity);
        }

        public async Task<IEnumerable<DisciplinaViewModel>> GetActivas()
        {
            var entidades = await GetQueryable()
                .Where(x => x.Activo == true)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return entidades.Select(MapToViewModel);
        }

        public async Task<IEnumerable<DisciplinaViewModel>> GetSubdisciplinas(long idPadre)
        {
            var entidades = await GetQueryable()
                .Where(x => x.IdPadre == idPadre)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return entidades.Select(MapToViewModel);
        }
    }
}
