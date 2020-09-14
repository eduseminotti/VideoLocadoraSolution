using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Dominio.Locatarios;
using VideoLocadora.Repositorio;
using VideoLocadora.Repositorio.Settings;
using VideoLocadora.Repositorio.Sql;

namespace VideoLocadora.WebApi
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
            services.AddControllers();

            services.AddControllers(options => options.RespectBrowserAcceptHeader = true)
                   .AddXmlDataContractSerializerFormatters();

            var dataSettings = this.Configuration.GetSection("DataSettings");
            services.Configure<DataSettings>(dataSettings);


            services.AddScoped<ILocatarioRepository, LocatarioRepositorio>();
            services.AddScoped<ILocatarioRepository, LocatarioRepositorio>();
            services.AddScoped<LocatarioDomainService>();
            services.AddScoped<IFilmeRepository, FilmeRepositorio>();
            services.AddScoped<FilmeDomainService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
