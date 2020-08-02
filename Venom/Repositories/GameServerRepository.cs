using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data;
using Venom.Data.Models;
using Venom.Data.Models.Configuration;

namespace Venom.Repositories
{
    public class GameServerRepository : IGameServerRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

	    public GameServerRepository(
		    DataContext context,
			ILogger<GameServerRepository> logger
		    )
	    {
            _context = context;
            _logger = logger;
	    }


	    public Task< List<ServerData> > GetGameServersAsync( )
	    {
            return _context.GetGameServers( );
	    }

        public Task<BuildingConfiguration> GetBuildingConfigurationAsync()
        {
            return _context.GetBuildingConfiguration();
        }

        public async Task<ServerData> GetServerAsync( int Server )
        {
            var data = await _context.GetGameServers( ).ConfigureAwait( false );
            return data.Find( p => p.Id.Equals( Server ) );
        }
    }
}
