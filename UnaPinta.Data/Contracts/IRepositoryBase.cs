﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnaPinta.Data.Contracts
{
    interface IRepositoryBase<T>
    {
        Task<int> SaveChangesAsync();
        IQueryable<T> FindAll(bool trackChanges); 
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); 
        void Create(T entity); 
        void Update(T entity); 
        void Delete(T entity);
    }
}
