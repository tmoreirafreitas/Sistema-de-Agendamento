using FluentValidation;
using SA.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SA.Domain.Interfaces.Services
{
    public interface IService<TEntity, VEntity> where TEntity : Entity where VEntity : AbstractValidator<TEntity>
    {
        Task<TEntity> Get(int id);
        Task<IQueryable<TEntity>> Get();
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);        
        Task<bool> Post(TEntity obj);
        Task<TEntity> Put(TEntity obj);
        Task<int> Put(Expression<Func<TEntity, bool>> predicate, TEntity obj);
        Task Delete(int id);
    }
}
