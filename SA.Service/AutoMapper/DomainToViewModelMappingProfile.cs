using AutoMapper;
using SA.Domain.Entities;
using SA.Service.ViewModels;
using System.Collections.Generic;

namespace SA.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Processo, ProcessoViewModel>();
        }
    }
}
