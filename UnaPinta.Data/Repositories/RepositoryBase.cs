using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Data.Extensions;
//OJASO: REVISAR ASUNTO DE LAZY LOADING Y ASNOTRACKING
namespace UnaPinta.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly UnaPintaDBContext _dbContext;
        protected DbSet<TEntity> dbSet => _dbContext.Set<TEntity>();

        public RepositoryBase(UnaPintaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        #region Create
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }
        #endregion

        #region Retrieve
        #region Querys
        public virtual IQueryable<TEntity> QueryAll(bool trackChanges=true) => 
            !trackChanges ? dbSet.Where(e => !e.DeletedAt.HasValue).AsNoTracking() : dbSet.Where(e => !e.DeletedAt.HasValue);

        public virtual IQueryable<TEntity> QueryByCondition(Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges=true)
        {
            var query = dbSet.IncludeMany(includes).Where(e => !e.DeletedAt.HasValue);
            if (where != null) query = query.Where(where);

            return !trackChanges ?
                query.AsNoTracking() :
                query;
        }

        #endregion
        public async Task<TEntity> SelectByIdAsync(TKey id) => await dbSet.FindAsync(id);

        public async Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, 
            IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges = true)
        {
            var query = QueryByCondition(where, includes, trackChanges);
            return await query.FirstOrDefaultAsync();
        }

        public Task<IEnumerable<TEntity>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges = true)
        {
            var query = QueryByCondition(where, includes, trackChanges);
            return await query.ToListAsync();
        }
        #endregion

        #region Update
        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }
        #endregion

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> where) 
        {
            var active = dbSet.Where(e => !e.DeletedAt.HasValue);
            return active.LongCountAsync(where);
        }

        public Task<long> CountAsync()
        {
            var active = dbSet.Where(e => !e.DeletedAt.HasValue);
            return active.LongCountAsync();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where) => dbSet.AnyAsync(where);
    }
}
