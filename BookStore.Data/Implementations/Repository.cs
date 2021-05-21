using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable => _dbSet.AsQueryable();

        public async Task<List<TEntity>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}