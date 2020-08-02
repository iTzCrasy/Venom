using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;
using Venom.Data.Models.Configuration;

namespace Venom.Repositories
{
    public interface IGameServerRepository
    {
	    Task< List<ServerData> > GetGameServersAsync( );
        Task<BuildingConfiguration> GetBuildingConfigurationAsync( );
        Task<ServerData> GetServerAsync( int Server );
    }
}
