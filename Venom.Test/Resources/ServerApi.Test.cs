using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Rest;
using Venom.Data.Models.Configuration;
using Xunit;
using Venom.Data.Models;
using Venom.Data.Models.Statistic;

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
                Assert.True( i.Game.Knight != 0 || i.Game.Knight != 3 );
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

        [Fact]
        public async Task FetchServerBuildingInfo( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<BuildingConfiguration>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchBuildingConfiguration( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchAllies()
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<Ally>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchAllies( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchPlayers( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<Player>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchPlayers( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchVillages( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<Village>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchVillages( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchConquered( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<Conquered>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchConquered( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchTribeAll( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchTribeAll( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchTribeAttack( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchTribeAttack( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchTribeDefense( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchTribeDefense( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchPlayerBashAll( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchPlayerBashAll( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchPlayerBashAttack( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchPlayerBashAttack( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }

        [Fact]
        public async Task FetchPlayerBashDefense( )
        {
            var serverList = await ServerApi.FetchGameServers( );

            var taskList = new List<Task<IReadOnlyList<BashpointsStatistic>>>( );

            foreach( var i in serverList )
            {
                taskList.Add( ServerApi.FetchPlayerBashDefense( i ) );
            }

            var result = await Task.WhenAll( taskList );

            foreach( var i in result )
            {
                //Console.WriteLine( i.ToString() );
                //Assert.True( i.Main.MinLevel != 1 );
            }
        }
    }
}
