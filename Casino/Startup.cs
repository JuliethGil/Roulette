using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Casino
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration.GetConnectionString("DefaultConnection"));
            #region Register (dependency injection)
            
            services.AddScoped<IRouletteQuery, RouletteQuery>();
            services.AddScoped<ITypeBetQuery, TypeBetQuery>();
            services.AddScoped<IRouletteNumberQuery, RouletteNumberQuery>();
            services.AddScoped<IBetQuery, BetQuery>();
            services.AddScoped<IRouletteLogic, RouletteLogic>();
            services.AddControllers();
            #endregion Register (dependency injection)

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
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}
