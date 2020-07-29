using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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

using Venom.API.Server;
using Venom.API.Database.Global;
using Venom.API.Database.Server;
using System.Runtime.InteropServices;
using Venom.API.Database.Logging;
using Microsoft.AspNetCore.ResponseCompression;

namespace Venom.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson( );
            services.AddSession( );
            services.AddDistributedMemoryCache( );

            services.AddSwaggerGen( options =>
            {
                options.SwaggerDoc( "v1", new Info { Title = "Venom API", Version = "v1" } );
            } );

            services.AddResponseCompression( options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>( );
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat( new[]
                          {
                               "image/svg+xml",
                               "application/json"
                            } ); ;
            } );
            services.Configure<GzipCompressionProviderOptions>( options => options.Level = System.IO.Compression.CompressionLevel.Optimal );

            services.AddDbContextPool<GlobalContext>( options => options.UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=VenomGlobal;Trusted_Connection=True;MultipleActiveResultSets=true" ) );
            services.AddDbContextPool<ServerContext>( options => options.UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=VenomServer;Trusted_Connection=True;MultipleActiveResultSets=true" ) );
            services.AddDbContextPool<LoggingContext>( options => options.UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=VenomLogging;Trusted_Connection=True;MultipleActiveResultSets=true" ) );

            services.AddHttpContextAccessor( );
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>( );

            services.AddTransient<ServerFiles>();
            services.AddTransient<ServerManager>( );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseResponseCompression( );

                //=> Swagger
                app.UseSwagger( );
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" );
                } );
            }

            using( var scope = app.ApplicationServices.CreateScope() )
            {
                var services = scope.ServiceProvider;
                var globalContext = services.GetRequiredService<GlobalContext>( );
                var serverContext = services.GetRequiredService<ServerContext>( );
                var serverManager = services.GetRequiredService<ServerManager>( );
                var loggingContext = services.GetRequiredService<LoggingContext>( );

                if( env.IsDevelopment() )
                {
                    globalContext.Database.EnsureCreated( );
                    serverContext.Database.EnsureCreated( );
                    loggingContext.Database.EnsureCreated( );
                }

                serverManager.Initialize( );
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
        public new string Title { get; set; }
        public new string Version { get; set; }
    }
}
