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
using NLog.Extensions.Logging;

namespace Venom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder( args ).Build( ).Run( );
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging( logging =>
                {
                    logging.AddFilter( "Microsoft", LogLevel.Warning );
                    //logging.AddFilter( "Microsoft", LogLevel.Information );
                    logging.AddFilter( "System", LogLevel.Warning );
                    logging.AddFilter( "LoggingConsoleApp.Program", LogLevel.Debug );

                    logging.AddFilter( "Microsoft", LogLevel.Warning );
                    logging.SetMinimumLevel( LogLevel.Trace );
                    logging.AddNLog( new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    } );
                } )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
