using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Services;
using SA.Service.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SA.Application.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.Get();
            var clientesViewModel = _mapper.Map<IList<ClienteViewModel>>(clientes);
            return View(clientesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id)
        {
            var cliente = await _clienteService.Get(id);
            return View(_mapper.Map<ClienteViewModel>(cliente));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel clienteModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var cliente = _mapper.Map<Cliente>(clienteModel);
                await _clienteService.Post(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.GetAsync(c => c.Id.Equals(id));
            if (cliente != null)
                await _clienteService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var cliente = _mapper.Map<Cliente>(clienteViewModel);
                await _clienteService.Put(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
