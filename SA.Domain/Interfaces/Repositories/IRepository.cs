using SA.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SA.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity obj);        
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity obj);
        Task AddAsync(TEntity obj);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(TEntity obj);
        Task DeleteAsync(int id);
        void DeleteAll();
    }
}
