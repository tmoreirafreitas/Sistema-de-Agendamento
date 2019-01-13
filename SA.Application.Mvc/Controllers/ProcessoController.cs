using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SA.Domain.Interfaces.Services;
using SA.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA.Application.Mvc.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IProcessoService _processoService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ProcessoController(IProcessoService processoService, IClienteService clienteService, IMapper mapper)
        {
            _processoService = processoService;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var processos = await _processoService.Get();
            return View(_mapper.Map<IList<ProcessoViewModel>>(processos));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var processosResult = await _processoService.GetAsync(p => p.Id.Equals(id));
            var processo = processosResult.FirstOrDefault();
            return View(_mapper.Map<ProcessoViewModel>(processo));
        }

        [HttpGet]
        public async Task<IActionResult> SumActive()
        {
            var somaAtivos = await _processoService.SumActiveProcesses();
            ViewData["SomaAtivos"] = somaAtivos;
            return View();
        }

        [HttpGet]
        public IActionResult Average()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Average(string nomeCliente, string estado)
        {
            var media = await _processoService.AverageProcessesOf(nomeCliente, estado);
            ViewData["Average"] = media;
            return View();
        }

        [HttpGet]
        public IActionResult NumberProcessValueAbove()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NumberProcessValueAbove(decimal valor)
        {
            var number = await _processoService.NumberProcessesAbove(valor);
            ViewData["ProcessNumber"] = number;
            return View();
        }

        [HttpGet]
        public IActionResult ProcessBetween()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessBetween(DateTime inicio, DateTime fim)
        {
            var processos = await _processoService.GetProcessesBetween(inicio, fim);
            var processosViewModel = _mapper.Map<IList<ProcessoViewModel>>(processos);
            return View(processosViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessByStateClient(string estado)
        {
            var processos = await _processoService.GetProcessesByStateClient(estado);
            return View(_mapper.Map<IList<ProcessoViewModel>>(processos));
        }

        [HttpGet]
        public async Task<IActionResult> ProcessByStateClient()
        {
            var processos = new List<ProcessoViewModel>();
            var clientes = await _clienteService.Get();
            foreach (var cliente in clientes)
            {
                var result = await _processoService.GetProcessesByStateClient(cliente.Estado);
                foreach(var p in result)
                {
                    p.Cliente = cliente;
                }
                processos.AddRange(_mapper.Map<IList<ProcessoViewModel>>(result));
            }

            return View(processos);
        }

        [HttpGet]
        public IActionResult ProcessesByAcronym()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessesByAcronym(string sigla)
        {
            var processos = await _processoService.GetProcessesByAcronym(sigla);
            return View(_mapper.Map<IList<ProcessoViewModel>>(processos));
        }
    }
}