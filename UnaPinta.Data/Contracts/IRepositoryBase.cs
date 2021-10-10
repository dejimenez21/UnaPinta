using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnaPinta.Data.Contracts
{
    public interface IRepositoryBase<TEntity, TKey>
    {
        Task<int> SaveChangesAsync();

        Task<TEntity> SelectByIdAsync(TKey id);
        Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> where, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges = false);
        
        Task<IEnumerable<TEntity>> SelectAllAsync();
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges = false);
        
        IQueryable<TEntity> QueryAll(bool trackChanges=false);
        IQueryable<TEntity> QueryByCondition(Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool trackChanges=false);
        
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> where);
        Task<long> CountAsync();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where);
    }
}
