using SA.Domain.Entities;
using SA.Domain.Interfaces.Repositories;
using SA.Domain.Interfaces.Services;
using SA.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA.Service
{
    public class ProcessoService : Service<Processo, ProcessoValidator>, IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _uow;

        public ProcessoService(IProcessoRepository processoRepository, IClienteRepository clienteRepository, IUnitOfWork unitOfWork) : base(processoRepository, unitOfWork)
        {
            _processoRepository = processoRepository;
            _clienteRepository = clienteRepository;
            _uow = unitOfWork;
        }

        public async Task<decimal> AverageProcessesOf(string nomeCliente, string estado)
        {
            var cliente = await _clienteRepository.SingleAsync(c => c.Nome.ToLowerInvariant().Equals(nomeCliente.ToLowerInvariant()));
            var processos = await _processoRepository.GetAsync(p => p.ClienteId.Equals(cliente.Id)
            && p.Estado.ToLowerInvariant().Equals(estado.ToLowerInvariant()));
            decimal media = processos.Average(p => p.Valor);
            return media;
        }

        public async Task<IList<Processo>> GetProcessesBetween(DateTime inicio, DateTime fim)
        {
            var processos = await _processoRepository.GetAsync(p => p.DataCriacao >= inicio && p.DataCriacao <= fim);
            return processos.ToList();
        }

        public async Task<IList<Processo>> GetProcessesByAcronym(string sigla)
        {
            if (string.IsNullOrEmpty(sigla))
                throw new ArgumentException("Por favor informar a sigla para pesquisa");

            var processos = await _processoRepository.GetAsync(p => p.Numero.ToLowerInvariant().Contains(sigla.ToLowerInvariant()));
            return processos.ToList();
        }

        public async Task<IList<Processo>> GetProcessesByStateClient(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                throw new ArgumentException("Por favor informar o estado para pesquisa");

            var cliente = await _clienteRepository.SingleAsync(c => c.Estado.ToLowerInvariant().Equals(estado.ToLowerInvariant()));
            IList<Processo> processos = null;
            if (cliente != null)
            {
                var result = await _processoRepository.GetAsync(p => p.Estado.ToLowerInvariant().Equals(estado.ToLowerInvariant()) && p.ClienteId.Equals(cliente.Id));
                processos = result.ToList();
            }

            return processos;
        }

        public async Task<int> NumberProcessesAbove(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Por favor informar um valor positivo para pesquisa");

            var processos = await _processoRepository.GetAsync(p => p.Valor > valor);

            if (processos != null)
                return processos.ToList().Count;
            return 0;
        }

        public async Task<decimal> SumActiveProcesses()
        {
            var processosAtivos = await _processoRepository.GetAsync(p => p.IsAtivo.Equals(true));
            return processosAtivos.Sum(p => p.Valor);
        }
    }
}
