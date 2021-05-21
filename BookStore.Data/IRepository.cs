using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }
        Task<TEntity> GetById(object id);
        Task<List<TEntity>> Get();
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}