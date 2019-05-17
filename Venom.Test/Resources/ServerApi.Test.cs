using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Resources;
using Xunit;

namespace Venom.Test.Resources
{
    public class ServerApiTest
    {
        [Fact]
        public async Task FetchServers( )
        {
            var serverList = await ServerApi.FetchServers( );

            Assert.True( serverList.Count > 0 );
        }

        [Fact]
        public async Task FetchServerConfig( )
        {
            var serverList = await ServerApi.FetchServers( );

            var taskList = new List<Task<ServerConfig>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchServerConfig( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                Assert.True( i.Speed != 0 );
                Assert.True( i.UnitSpeed != 0 );
            }
        }

        [Fact]
        public async Task FetchServerUnitInfo( )
        {
            var serverList = await ServerApi.FetchServers( );

            var taskList = new List<Task<ServerUnitConfig>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchServerUnitInfo( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                Assert.True( i.Spear.Speed != 0.0 );
            }
        }
    }
}
