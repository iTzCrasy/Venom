using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Core.Resources;
using Venom.Core.Resources.API;
using Xunit;

namespace Venom.Test.Core.Resources
{
    
    public class GameServerTest
    {
        [Fact]
        public async Task FetchServers( )
        {
            var serverList = await GameServer.FetchServers( );

            Assert.True( serverList.Count > 0 );
        }

        [Fact]
        public async Task FetchServerConfig( )
        {
            var serverList = await GameServer.FetchServers( );

            var taskList = new List<Task<ServerConfig>>( );

            foreach( var i in serverList)
            {
                taskList.Add( GameServer.FetchServerConfig( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                Assert.True( i.Speed != 0 );
                Assert.True( i.UnitSpeed != 0 );
            }
        }

    }
}
