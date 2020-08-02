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
using Venom.API.Database.Logging;
using Venom.API.Database.Logging.Entities;

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
        private readonly ILogger<ServerManager> _logger;
        private readonly ServerFiles _data;

        //=> New
        private readonly GlobalContext _globalContext;
        private readonly ServerContext _serverContext;
        private readonly LoggingContext _loggingContext;
        private bool _serverUpdateReady = false;
        private readonly DateTimeOffset _serverUpdateTime = DateTimeOffset.Now.Date.AddHours( DateTimeOffset.Now.Hour + 1 ).AddMinutes( 0 );

        public ServerManager(
           ILogger<ServerManager> logger,
           ServerFiles data,
           IServiceScopeFactory scopeFactory,
           ServerContext serverContext,
           GlobalContext globalContext,
           LoggingContext loggingContext )
        {
            _logger = logger;
            _data = data;
            _serverContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<ServerContext>( );
            _globalContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<GlobalContext>( );
            _loggingContext = scopeFactory.CreateScope( ).ServiceProvider.GetRequiredService<LoggingContext>( );
        }

        public void Initialize()
        {
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
                if( _serverUpdateReady == false )
                {
                    continue;
                }

                var checkTime = DateTimeOffset.Compare( TrimDate( DateTimeOffset.Now ), TrimDate( _serverUpdateTime ) );
                if( checkTime == 0 )
                {
                    var serverList = _globalContext.ServerList.ToList( );
                    foreach( var server in serverList )
                    {
                        watch.Restart( );

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

                        foreach( var player in dataPlayer )
                        {
                            player.Time = _serverUpdateTime;
                            player.BashAtt = dataBashPlayerAtt.ContainsKey( player.PlayerId ) ? dataBashPlayerAtt[player.PlayerId].Kills : 0;
                            player.BashDef = dataBashPlayerDef.ContainsKey( player.PlayerId ) ? dataBashPlayerDef[player.PlayerId].Kills : 0;
                            player.BashAll = dataBashPlayerAll.ContainsKey( player.PlayerId ) ? dataBashPlayerAll[player.PlayerId].Kills : 0;
                        }

                        foreach( var ally in dataAlly )
                        {
                            ally.Time = _serverUpdateTime;
                            ally.BashAtt = dataBashAllyAtt.ContainsKey( ally.AllyId ) ? dataBashAllyAtt[ally.AllyId].Kills : 0;
                            ally.BashDef = dataBashAllyDef.ContainsKey( ally.AllyId ) ? dataBashAllyDef[ally.AllyId].Kills : 0;
                            ally.BashAll = dataBashAllyAll.ContainsKey( ally.AllyId ) ? dataBashAllyAll[ally.AllyId].Kills : 0;
                        }

                        //=> Update Player Data
                        using( var transaction = _serverContext.Database.BeginTransaction( ) )
                        {
                            try
                            {
                                if( server.IsValid )
                                {
                                    await _serverContext.BulkInsertAsync( dataAlly );
                                    await _serverContext.BulkInsertAsync( dataPlayer );
                                    await _serverContext.BulkInsertOrUpdateAsync( dataVillage );
                                }

                                transaction.Commit( );
                            }
                            catch( Exception ex )
                            {
                                transaction.Rollback( );
                                continue;
                            }
                        }

                        watch.Stop( );

                        using( var transaction = _globalContext.Database.BeginTransaction( ) )
                        {
                            try
                            {
                                server.PlayerCount = dataPlayer.Count;
                                server.AllyCount = dataAlly.Count;
                                server.VillageCount = dataVillage.Where( p => p.Owner != 0 ).Count();
                                server.BarbarianCount = dataVillage.Where( p => p.Owner == 0 ).Count( );

                                _globalContext.SaveChanges( );

                                transaction.Commit( );
                            }
                            catch( Exception ex )
                            {
                                transaction.Rollback( );
                                _logger.LogError( $"Server Transaction Failed! [ GlobalContext ][ { server.World } ] { ex.Message }" );
                                continue;
                            }
                        }
                        
                        using( var transaction = _loggingContext.Database.BeginTransaction( ) )
                        {
                            try
                            {
                                var serverUpdates = new ServerUpdates
                                {
                                    Server = server.Id,
                                    UpdateTime = _serverUpdateTime,
                                    Duration = watch.Elapsed
                                };

                                _loggingContext.ServerUpdates.Add( serverUpdates );
                                _loggingContext.SaveChanges( );

                                transaction.Commit( );
                            }
                            catch( Exception ex )
                            {
                                transaction.Rollback( );
                                _logger.LogError( $"Logging Transaction Failed! [ GlobalContext ][ { server.World } ] { ex.Message }" );
                                continue;
                            }
                        }
                    }
                    _serverUpdateTime.AddHours( 1 );
                    _logger.LogInformation( "Server Updates Finished!" );
                }
           
                await Task.Delay( TimeSpan.FromMinutes( 1 ) );
            }
        }

        public async Task ServerListUpdater( )
        {
            while( true )
            {
                _logger.LogInformation( "Server List Update!" );

                var serverList = await _data.FetchServerList( );
                serverList.OrderBy( p => p.World );
                foreach( var i in serverList )
                {
                    using( var transaction = _globalContext.Database.BeginTransaction( ) )
                    {
                        try
                        {
                            if( _globalContext.ServerList.Any( p => p.World == i.World ) == false )
                            {
                                _globalContext.ServerList.Add( i );
                                _globalContext.SaveChanges( );
                            }

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


