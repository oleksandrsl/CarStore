using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using CarStore.Models;
using CarStore.Repositories;
using Microsoft.AspNetCore.Http;

namespace CarStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration["SqlServerConnectionString"]));

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
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

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            SampleData.InitializeStoreDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
