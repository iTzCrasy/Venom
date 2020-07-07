using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Venom.API.Database.Global;
using Venom.API.Database.Global.Entities;
using Venom.API.Database.Server;
using Venom.API.Database.Server.Entities;
using System.Linq;

namespace Venom.API.Server
{
    public enum QueueType
    {
        ServerUpdate,
        ServerListAdd,
    }

    public class QueueData
    {
        public ServerData Server;
        public QueueType QueueType;
    }

    public class ServerManager 
    {
        private readonly Dictionary<int, ServerData> _serverData = new Dictionary<int, ServerData>( );
        private readonly ILogger<ServerManager> _logger;
        private readonly ServerFiles _data;

        //=> New
        private readonly GlobalContext _globalContext;
        private readonly ServerContext _serverContext;
        private bool _serverUpdateReady = false;
        private readonly DateTimeOffset _serverUpdateTime = DateTimeOffset.Now.Date.AddHours( DateTimeOffset.Now.Hour  ).AddMinutes( 10 );

        public ServerManager(
           ILogger<ServerManager> logger,
           ServerFiles data,
           IServiceScopeFactory scopeFactory )
        {
            _logger = logger;
            _data = data;
            _serverContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<ServerContext>( );
            _globalContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<GlobalContext>( );
        }

        public void Initialize()
        {
            //=> TODO: Start ServerUpdater!

            Task.Run( ( ) => ServerListUpdater( ) );
            Task.Run( ( ) => ServerUpdater( ) );
        }

        public DateTimeOffset TrimDate( DateTimeOffset dt )
        {
            return new DateTimeOffset( dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0, dt.Offset );
        }

        public async Task ServerUpdater()
        {
            var watch = new Stopwatch( );
            while( true )
            {
                var currentTime = DateTimeOffset.Now;

                if( _serverUpdateReady == false )
                {
                    continue;
                }

                var checkTime = DateTimeOffset.Compare( TrimDate( currentTime ), TrimDate( _serverUpdateTime ) );
                if( checkTime == 0 )
                {
                    var serverList = _globalContext.ServerList.ToList( );
                    foreach( var server in serverList )
                    {
                        watch.Restart( );

                        //=> Update Server Data!
                        using( var transaction = _serverContext.Database.BeginTransaction( ) )
                        {
                            _logger.LogInformation( $"Begin Server Transaction [ { server.World } ]" );
                            try
                            {
                                watch.Restart( );

                                //=> Load Data 
                                var dataPlayer = await _data.FetchPlayerList( server );
                                var dataAlly = await _data.FetchAllyList( server );
                                var dataVillage = await _data.FetchVillageList( server );

                                //=> Player Bash
                                var dataBashPlayerAtt = await _data.FetchBashpoints( server, "map/kill_att.txt" );
                                var dataBashPlayerDef = await _data.FetchBashpoints( server, "map/kill_def.txt" );
                                var dataBashPlayerAll = await _data.FetchBashpoints( server, "map/kill_all.txt" );

                                //=> Ally Bash
                                var dataBashAllyAtt = await _data.FetchBashpoints( server, "map/kill_att_tribe.txt" );
                                var dataBashAllyDef = await _data.FetchBashpoints( server, "map/kill_def_tribe.txt" );
                                var dataBashAllyAll = await _data.FetchBashpoints( server, "map/kill_all_tribe.txt" );

                                if( server.IsValid )
                                {
                                    foreach( var player in dataPlayer )
                                    {
                                        player.BashAtt = dataBashPlayerAtt.ContainsKey( player.PlayerId ) ? dataBashPlayerAtt[player.PlayerId].Kills : 0;
                                        player.BashDef = dataBashPlayerDef.ContainsKey( player.PlayerId ) ? dataBashPlayerDef[player.PlayerId].Kills : 0;
                                        player.BashAll = dataBashPlayerAll.ContainsKey( player.PlayerId ) ? dataBashPlayerAll[player.PlayerId].Kills : 0;
                                    }

                                    foreach( var ally in dataAlly )
                                    {
                                        ally.BashAtt = dataBashAllyAtt.ContainsKey( ally.AllyId ) ? dataBashAllyAtt[ally.AllyId].Kills : 0;
                                        ally.BashDef = dataBashAllyDef.ContainsKey( ally.AllyId ) ? dataBashAllyDef[ally.AllyId].Kills : 0;
                                        ally.BashAll = dataBashAllyAll.ContainsKey( ally.AllyId ) ? dataBashAllyAll[ally.AllyId].Kills : 0;
                                    }

                                    //=> Insert if Valid
                                    await _serverContext.BulkInsertOrUpdateAsync( dataPlayer );
                                    await _serverContext.BulkInsertOrUpdateAsync( dataAlly );
                                    await _serverContext.BulkInsertOrUpdateAsync( dataVillage );
                                }

                                transaction.Commit( );

                                watch.Stop( );
                            }
                            catch( Exception ex )
                            {
                                transaction.Rollback( );
                                _logger.LogError( $"Server Transaction Failed! [ ServerContext ][ { server.World } ] { ex.Message }" );
                                continue;
                            }
                        }

                        using( var transaction = _globalContext.Database.BeginTransaction( ) )
                        {
                            try
                            {
                                server.LastUpdate = DateTime.Now;
                                server.Duration = watch.ElapsedMilliseconds;

                                _globalContext.SaveChanges( );

                                transaction.Commit( );
                            }
                            catch( Exception ex )
                            {
                                transaction.Rollback( );
                                _logger.LogError( $"Server Transaction Failed! [ GlobalContext ][{ server.World } ] { ex.Message }" );
                                continue;
                            }
                        }                        
                    }

                    _serverUpdateTime.AddHours( 1 );
                }
           
                await Task.Delay( TimeSpan.FromMinutes( 1 ) );
            }
        }

        public async Task ServerListUpdater( )
        {
            while( true )
            {
                var serverList = await _data.FetchServerList( );

                foreach( var i in serverList )
                {
                    if( _serverData.ContainsKey( i.World ) )
                    {
                        continue;
                    }

                    using( var transaction = _globalContext.Database.BeginTransaction( ) )
                    {
                        try
                        {
                            if( _globalContext.ServerList.Any( p => p.World == i.World ) == false )
                            {
                                _globalContext.ServerList.Add( i );
                                _globalContext.SaveChanges( );
                            }

                            _serverData.Add( i.World, i );

                            transaction.Commit( );
                        }
                        catch( Exception ex )
                        {
                            transaction.Rollback( );
                            _logger.LogError( $"Server Transaction Failed! [{ i.World } ] { ex.Message }" );
                        }
                    }
                }

                _serverUpdateReady = true;

                await Task.Delay( TimeSpan.FromHours( 6 ) );
            }
        }
    }
}


