﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SA.Domain.Interfaces.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SA.Application.Mvc.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IProcessoService _service;
        private readonly IMapper _mapper;

        public ProcessoController(IProcessoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}