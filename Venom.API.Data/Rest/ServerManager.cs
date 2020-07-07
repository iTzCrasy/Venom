using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Venom.API.Data.Database.Entities;
using Venom.API.Data.Database.Global;
using Venom.API.Data.Database.Global.Entities;

namespace Venom.API.Data.Rest
{
    public class ServerManager
    {
        private readonly Dictionary<int, ServerData> _serverData = new Dictionary<int, ServerData>( );

        private readonly GlobalContext _globalContext;

        public ServerManager( GlobalContext globalContext )
        {
            _globalContext = globalContext;
        }

        public async Task CheckServerAsync()
        {
            var serverList = await Updater.FetchServerList( );

            foreach( var i in serverList )
            {
                AddServer( i );
            }
        }

        public void AddServer( ServerData server )
        {
            _serverData.Add( server.World, server );
        }
    }
}
