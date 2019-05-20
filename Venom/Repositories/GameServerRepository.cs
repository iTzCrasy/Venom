using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public class GameServerRepository : IGameServerRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

	    public GameServerRepository(
		    DataContext context,
			Logger<GameServerRepository> logger
		    )
	    {
            _context = context;
            _logger = logger;
	    }


	    public Task< List< GameServer > > GetGameServersAsync( )
	    {
            return _context.GetGameServers( );
	    }
    }
}
