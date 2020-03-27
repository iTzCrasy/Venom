using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Venom.API
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

            services.AddDistributedMemoryCache( );
            services.AddSession( );

            services.AddSwaggerGen( options =>
            {
                options.SwaggerDoc( "v1", new Info { Title = "Venom API", Version = "v1" } );
            } );

            services.AddDbContext<Context.DataContext>( options => options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) );

            services.AddHttpContextAccessor( );
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>( );

            services.AddHostedService<Services.ServerUpdateService>( );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //=> Swagger
                app.UseSwagger( );
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint( "/swagger/v1/swagger.json", "WideWorldImporters API V1" );
                } );
            }

            app.UseSession( );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}
