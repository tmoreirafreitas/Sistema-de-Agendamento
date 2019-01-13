using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SA.Domain.Entities;
using SA.Domain.Interfaces.Services;
using SA.Infra.Data.Context;
using SA.Service.AutoMapper;
using AutoMapper;
using SA.Infra.CrossCutting.IoC;

namespace SA.Application.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddAutoMapper();

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();

            services.AddDbContext<SaDbContext>(opt => opt.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitialDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }            

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        private static void InitialDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<SaDbContext>();
                
                var clienteA = new Cliente()
                {
                    Nome = "Empresa A",
                    Cnpj = "00000000001",
                    Estado = "Rio de Janeiro"
                };

                var clienteB = new Cliente()
                {
                    Nome = "Empresa B",
                    Cnpj = "00000000002",
                    Estado = "São Paulo"
                };

                // Processos da Empresa A
                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00001CIVELRJ",
                    Estado = "Rio de Janeiro",
                    Valor = 200000,
                    DataCriacao = new DateTime(2007, 10, 10),
                    Cliente = clienteA
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00002CIVELSP",
                    Estado = "São Paulo",
                    Valor = 100000,
                    DataCriacao = new DateTime(2007, 10, 20),
                    Cliente = clienteA
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = false,
                    Numero = "00003TRABMG",
                    Estado = "Minas Gerais",
                    Valor = 10000,
                    DataCriacao = new DateTime(2007, 10, 30),
                    Cliente = clienteA
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = false,
                    Numero = "00004CIVELRJ",
                    Estado = "Rio de Janeiro",
                    Valor = 20000,
                    DataCriacao = new DateTime(2007, 11, 10),
                    Cliente = clienteA
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00005CIVELSP",
                    Estado = "São Paulo",
                    Valor = 35000,
                    DataCriacao = new DateTime(2007, 11, 15),
                    Cliente = clienteA
                });

                // Processos da Empresa B
                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00006CIVELRJ",
                    Estado = "Rio de Janeiro",
                    Valor = 20000,
                    DataCriacao = new DateTime(2007, 5, 1),
                    Cliente = clienteB
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00007CIVELRJ",
                    Estado = "Rio de Janeiro",
                    Valor = 700000,
                    DataCriacao = new DateTime(2007, 6, 2),
                    Cliente = clienteB
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = false,
                    Numero = "00008CIVELSP",
                    Estado = "São Paulo",
                    Valor = 500,
                    DataCriacao = new DateTime(2007, 7, 3),
                    Cliente = clienteB
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = true,
                    Numero = "00009CIVELSP",
                    Estado = "São Paulo",
                    Valor = 32000,
                    DataCriacao = new DateTime(2007, 8, 4),
                    Cliente = clienteB
                });

                dbContext.Set<Processo>().Add(new Processo()
                {
                    IsAtivo = false,
                    Numero = "00010TRABAM",
                    Estado = "Amazonas",
                    Valor = 1000,
                    DataCriacao = new DateTime(2007, 9, 5),
                    Cliente = clienteB
                });

                dbContext.SaveChanges();
            }
               
        }
    }
}
