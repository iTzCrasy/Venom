using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public class GameServerRepository : IGameServerRepository
    {
	    public async Task< List< GameServer > > GetGameServersAsync( )
	    {
			// todo implement caching layer
		    return await ServerApi.FetchGameServers( )
			    .ConfigureAwait( false );
	    }
    }
}
