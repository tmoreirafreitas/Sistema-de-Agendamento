using Microsoft.EntityFrameworkCore;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;

namespace SA.Infra.Data.Repositories
{
    public class ProcessoRepository : Repository<Processo>, IProcessoRepository
    {
        public ProcessoRepository(DbContext context) : base(context)
        {
        }
    }
}
