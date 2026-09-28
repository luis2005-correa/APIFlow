using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaFlowAPI.Services.Common
{
    public abstract class BaseService<TEntity, TViewModel, TKey> : IService<TViewModel, TKey>
        where TEntity : class
        where TViewModel : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseService(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected abstract TViewModel MapToViewModel(TEntity entity);

        protected abstract TEntity MapToEntity(TViewModel model);

        protected virtual IQueryable<TEntity> GetQueryable() => DbSet.AsNoTracking();

        public virtual async Task<IEnumerable<TViewModel>> GetAll()
        {
            var entities = await GetQueryable().ToListAsync();
            return entities.Select(MapToViewModel);
        }

        public virtual async Task<TViewModel?> GetById(TKey id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return null;

            return MapToViewModel(entity);
        }

        public virtual async Task<TViewModel> Create(TViewModel model)
        {
            var entity = MapToEntity(model);

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return MapToViewModel(entity);
        }

        public virtual async Task<bool> Update(TKey id, TViewModel model)
        {
            var entity = MapToEntity(model);
            Context.Entry(entity).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public virtual async Task<bool> Delete(TKey id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}