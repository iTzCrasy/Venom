using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Venom.API.Database.Logging;
using Venom.API.Database.Server;
using Venom.API.Database.Server.Entities;

namespace Venom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces( "application/json" )]
    public class DataController : ControllerBase
    {
        private readonly ServerContext _serverContext;
        private readonly LoggingContext _loggingContext;

        public DataController( IServiceScopeFactory scopeFactory )
        {
            _serverContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<ServerContext>( );
            _loggingContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<LoggingContext>( );
        }

        [HttpGet( "Player" )]
        //[Authorize]
        public async Task<IEnumerable<Player>> GetPlayerList( int Server )
        {
            //=> TODO: Make it safe!
            var serverUpdate = _loggingContext.ServerUpdates
                .Where( p => p.Server == Server )
                .OrderByDescending( p => p.UpdateTime )
                .Take( 1 ).Single( );

            return await _serverContext.Player
                .Where( p => p.Server == Server && p.Time == serverUpdate.UpdateTime )
                .ToListAsync( );
        }

        [HttpGet( "Ally" )]
        //[Authorize]
        public async Task<IEnumerable<Ally>> GetAllyList( int Server )
        {
            //=> TODO: Make it safe!
            var serverUpdate = _loggingContext.ServerUpdates
                .Where( p => p.Server == Server )
                .OrderByDescending( p => p.UpdateTime )
                .Take( 1 ).Single( );

            return await _serverContext.Ally
                .Where( p => p.Server == Server && p.Time == serverUpdate.UpdateTime )
                .ToListAsync( );
        }

        [HttpGet( "Village" )]
        //[Authorize]
        //[Produces( "application/gzip" )]
        public async Task<IEnumerable<Village>> GetVillageList( int Server )
        {
            //=> TODO: Make it safe!
            return await _serverContext.Village
                .Where( p => p.Server == Server )
                .ToListAsync( );
        }
    }
}
