using AutoMapper;
using SA.Domain.Entities;
using SA.Service.ViewModels;
using System.Collections.Generic;

namespace SA.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ProcessoViewModel, Processo>();
        }
    }
}
