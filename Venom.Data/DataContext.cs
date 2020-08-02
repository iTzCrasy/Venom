using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data.Cache;
using Venom.Data.Models;
using Venom.Data.Rest;
using Venom.Data.Models.Configuration;
using Venom.Data.Api;

namespace Venom.Data
{
    public class DataContext
    {
        private readonly ILogger _logger;
        private readonly CacheManager _cache;
        private readonly ApiClient _apiClient;

        private GameServer _currentServer;
        private string _currentPlayer;


        public DataContext(
            ILogger<DataContext> logger,
            ILoggerFactory loggerFactory
            )
        {
            _logger = logger;
            _cache = new CacheManager( loggerFactory.CreateLogger<CacheManager>( ) );
            _apiClient = new ApiClient( loggerFactory.CreateLogger<ApiClient>( ) );
        }

        /// <summary>
        /// Set / Get Current Server Selected
        /// </summary>
        public GameServer CurrentServer
        {
            get => _currentServer;
            set => _currentServer = value;
        }

        public async Task<List<ServerData>> GetGameServers( )
        {
            const string cacheKey = "GameServers";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await _apiClient.FetchServerList();
            } );
        }

        public async Task<IReadOnlyList<Player>> GetPlayers()
        {
            var cacheKey = $"Players_{CurrentServer.Id}";
            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await ServerApi.FetchPlayers( CurrentServer );
            } );
        }

        public async Task<IReadOnlyList<Ally>> GetAllys()
        {
            var cacheKey = $"Allys_{CurrentServer.Id}";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await ServerApi.FetchAllies( CurrentServer );
            } );
        }

        public async Task<IReadOnlyList<Village>> GetVillages( int Server )
        {
            var cacheKey = $"Villages_{Server}";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await _apiClient.FetchVillages( Server );
            } );
        }

        public async Task<BuildingConfiguration> GetBuildingConfiguration()
        {
            var cacheKey = $"Buildings_{CurrentServer.Id}";

            return await _cache.Get( cacheKey, async ( ) =>
            {
                return await ServerApi.FetchBuildingConfiguration( CurrentServer );
            } );
        }
    }
}




