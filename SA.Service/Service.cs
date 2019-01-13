using FluentValidation;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;
using SA.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SA.Service
{
    public class Service<TEntity, VEntity> : IService<TEntity, VEntity> where TEntity : Entity where VEntity : AbstractValidator<TEntity>
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _uow;

        public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _uow = unitOfWork;
        }

        public async Task Delete(int id)
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    throw new ArgumentException("O id não pode ser nulo ou vazio.");

                _repository.DeleteAsync(id);
                _uow.Commit();
            }); ;
        }

        public async Task<TEntity> Get(int id)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    throw new ArgumentException("O id não pode ser nulo ou vazio.");

                return _repository.GetByIdAsync(id);
            });
        }

        public async Task<IQueryable<TEntity>> Get()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.GetAsync(expression);            
        }

        public async Task<bool> Post(TEntity obj)
        {
            Validate(obj, Activator.CreateInstance<VEntity>());
            await _repository.AddAsync(obj);
            return _uow.Commit();
        }

        public async Task<TEntity> Put(TEntity obj)
        {
            Validate(obj, Activator.CreateInstance<VEntity>());
            return await Task.Run(() =>
            {
                var objUpdated = _repository.UpdateAsync(obj);
                _uow.Commit();
                return objUpdated;
            });
        }

        public async Task<int> Put(Expression<Func<TEntity, bool>> predicate, TEntity obj)
        {
            var objUpdated = await _repository.UpdateAsync(predicate, obj);
            _uow.Commit();
            return objUpdated;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}