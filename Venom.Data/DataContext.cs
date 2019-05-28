using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data.Cache;
using Venom.Data.Models;
using Venom.Data.Rest;

namespace Venom.Data
{
    public class DataContext
    {
        private readonly ILogger _logger;
        private readonly CacheManager _cache;


        public DataContext(
            ILogger<DataContext> logger,
            ILoggerFactory loggerFactory
            )
        {
            _logger = logger;

            _cache = new CacheManager(
                loggerFactory.CreateLogger<CacheManager>( )
                );
        }


        public async Task<List<GameServer>> GetGameServers( )
        {
            const string cacheKey = "GameServers";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await ServerApi.FetchGameServers( );
            } );
        }

        public async Task<IReadOnlyList<Player>> GetPlayers( GameServer server )
        {
            var cacheKey = $"Players_{server.Id}";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await ServerApi.FetchPlayers( server );
            } );
        }

    }
}
