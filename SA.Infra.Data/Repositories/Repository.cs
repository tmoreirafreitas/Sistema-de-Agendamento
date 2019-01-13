using Microsoft.EntityFrameworkCore;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SA.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public Task AddAsync(TEntity obj)
        {
            return _dbSet.AddAsync(obj);
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet);
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.Run(() =>
            {
                var items = _dbSet.Where(expression);
                _dbSet.RemoveRange(items);
            });
        }

        public Task DeleteAsync(TEntity obj)
        {            
            return Task.Run(() =>
            {
                var result = _dbSet.Find(obj.Id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                var result = _dbSet.Find(id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return _dbSet.AsQueryable();
            });
        }

        public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.Run(() =>
            {
                return _dbSet.Where(expression);
            });
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                return _dbSet.SingleAsync(o => o.Id.Equals(id));
            });
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.Run(() =>
            {
                return _dbSet.SingleAsync(expression);
            });
        }

        public Task<int> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity obj)
        {
            return Task.Run(() =>
            {
                var updated = _dbSet.Update(obj);
                updated.State = EntityState.Modified;
                return updated.Entity.Id;
            });            
        }

        public Task<TEntity> UpdateAsync(TEntity obj)
        {
            return Task.Run(() =>
            {
                var updated = _dbSet.Update(obj);
                updated.State = EntityState.Modified;
                return updated.Entity;
            });
        }
    }
}
