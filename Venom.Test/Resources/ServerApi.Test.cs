using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Rest;
using Venom.Data.Models.Configuration;
using Xunit;

namespace Venom.Test.Resources
{
    public class ServerApiTest
    {
        [Fact]
        public async Task FetchServers( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            Assert.True( serverList.Count > 0 );
        }

        [Fact]
        public async Task FetchServerConfig( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<ServerConfiguration>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchServerConfiguration( i ) );
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
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<UnitConfiguration>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchUnitConfiguration( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                Assert.True( i.Spear.Speed != 0.0 );
            }
        }
    }
}
