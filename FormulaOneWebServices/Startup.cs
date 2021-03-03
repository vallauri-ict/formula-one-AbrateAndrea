using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FormulaOneWebServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Ecco le possibili api:\n");

                    await context.Response.WriteAsync("COUNTRY:\n");
                    await context.Response.WriteAsync("/api/country [lista di tutte le country]\n");
                    await context.Response.WriteAsync("/api/country/it [una nazione in base al codice]\n");

                    await context.Response.WriteAsync("DRIVER:\n");
                    await context.Response.WriteAsync("/api/driver [lista di tutti i driver]\n");
                    await context.Response.WriteAsync("/api/driver/id/1 [un driver in base all'id]\n");
                    //await context.Response.WriteAsync("/api/driver/number/44\n");
                    await context.Response.WriteAsync("/api/driver/name/Lewis Hamilton [un driver in base al nome]\n");
                    await context.Response.WriteAsync("/api/listDrivers [attributi principali di tutti i drivers]\n");
                    await context.Response.WriteAsync("/api/listDrivers/44 [attributi principali di un singolo driver richiesto]\n");

                    await context.Response.WriteAsync("TEAM:\n");
                    await context.Response.WriteAsync("/api/team [lista di tutti i driver]\n");
                    await context.Response.WriteAsync("/api/team/id/1 [un team in base all'id]\n");
                    await context.Response.WriteAsync("/api/team/name/Mercedes-AMG Petronas F1 Team [un team in base al nome]\n");
                    await context.Response.WriteAsync("/api/listTeams [attributi principali di tutti i teams]\n");
                    await context.Response.WriteAsync("/api/listTeams/1 [attributi principali di un singolo team]\n"); 

                });
                endpoints.MapControllers();
            });
        }
    }
}