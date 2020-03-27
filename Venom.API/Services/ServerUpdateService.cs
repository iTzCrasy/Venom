using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Venom.API.Services
{
    public class ServerUpdateService : BackgroundService
    {
        private readonly ILogger<ServerUpdateService> _logger;
        private readonly Context.DataContext _dataContext;

        public ServerUpdateService( 
            ILogger<ServerUpdateService> logger,
            IServiceScopeFactory scopeFactory )
        {
            _logger = logger;
            _dataContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<Context.DataContext>( ) ;
        }

        protected override async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            _logger.LogInformation( $"Starting ServerUpdateService." );

            while( !cancellationToken.IsCancellationRequested )
            {
                var list = await Data.Rest.ServerApi.FetchGameServers( );

                await Task.Run( async ( ) =>
                {
                    foreach( var i in list )
                    {
                        //=> We don't want casual server
                        if( !i.Id.Contains( "dep" ) && !i.Id.Contains( "des" ) )
                        {
                            var serverId = i.Id.Remove( 0, i.Id.Length - 3 );

                            _logger.LogWarning( $"Adding Server { i.Id }" );

                            await _dataContext.Server.AddAsync( new Models.ServerModel { Server = int.Parse( serverId ), Lang = "de" } );
                        }
                    }
                    await _dataContext.SaveChangesAsync( );
                } );
                
                await Task.Delay( 3600000, cancellationToken );
            }
        }
    }
}
