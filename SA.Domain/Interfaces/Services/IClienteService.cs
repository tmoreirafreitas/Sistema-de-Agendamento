using SA.Domain.Entities;
using SA.Domain.Validators;

namespace SA.Domain.Interfaces.Services
{
    public interface IClienteService : IService<Cliente, ClienteValidator>
    {
        
    }
}
