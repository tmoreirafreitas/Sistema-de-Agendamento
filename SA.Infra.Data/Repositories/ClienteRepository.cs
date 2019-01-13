using Microsoft.EntityFrameworkCore;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;

namespace SA.Infra.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DbContext context) : base(context)
        {
        }
    }
}
