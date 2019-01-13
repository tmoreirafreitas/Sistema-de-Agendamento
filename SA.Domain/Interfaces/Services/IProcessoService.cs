using SA.Domain.Entities;
using SA.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA.Domain.Interfaces.Services
{
    public interface IProcessoService : IService<Processo, ProcessoValidator>
    {
        Task<decimal> SumActiveProcesses();
        Task<decimal> AverageProcessesOf(string nomeCliente, string estado);
        Task<int> NumberProcessesAbove(decimal valor);
        Task<IList<Processo>> GetProcessesBetween(DateTime inicio, DateTime fim);
        Task<IList<Processo>> GetProcessesByStateClient(string estado);
        Task<IList<Processo>> GetProcessesByAcronym(string sigla);
    }
}
