using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Venom.Data.Models;

namespace Venom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder( args ).Build( );

            using( var scope = host.Services.CreateScope( ) )
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>( );
                    context.Initialize( );
                   // context.Database.EnsureDeleted( );
                    //context.Database.EnsureCreated( );
                    //context._Players.Add( new Models.Game.GameAccounts { Id = 1337, PlayerName = "Test" } );
                    //context._Accounts.Add( new Models.Account { Id = 1337, Username = "Test", Password = "1234" } );
                    //context.SaveChanges( );
                }
                catch( Exception ex )
                {
                    var logger = services.GetRequiredService<ILogger<Program>>( );
                    logger.LogError( ex, "An error occurred while seeding the database." );
                }
            }

            host.Run( );
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
