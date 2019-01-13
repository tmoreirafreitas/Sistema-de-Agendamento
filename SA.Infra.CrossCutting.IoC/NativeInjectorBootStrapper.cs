using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SA.Domain.Interfaces.Repositories;
using SA.Domain.Interfaces.Services;
using SA.Infra.Data.Context;
using SA.Infra.Data.Repositories;
using SA.Infra.Data.UnitOfWork;
using SA.Service;

namespace SA.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Services dependency
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProcessoService, ProcessoService>();

            // Infra-Data dependency
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProcessoRepository, ProcessoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Instance Context
            services.AddScoped<DbContext, SaDbContext>();
            services.AddScoped<SaDbContext>();
        }
    }
}
