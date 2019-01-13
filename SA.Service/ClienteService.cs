using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;
using SA.Domain.Interfaces.Services;
using SA.Domain.Validators;

namespace SA.Service
{
    public class ClienteService : Service<Cliente, ClienteValidator>, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _uow;
        public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _repository = repository;
            _uow = unitOfWork;
        }
    }
}
