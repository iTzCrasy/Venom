using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Venom.Data.Models;
using Venom.Data.Models.Configuration;
using Venom.Data.Models.Statistic;
using Venom.Data.Utility;

namespace Venom.Data
{
    public static class ServerApi
    {
        #region Configuration
        public static async Task<List<GameServer>> FetchGameServers( )
        {
            var result = new List<GameServer>( );

            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( "http://www.die-staemme.de/backend/get_servers.php" )
                    .ConfigureAwait( false );

                var text = new StreamReader( stream )
                    .ReadToEnd( );

                var list = DeserializePhp.Deserialize( text );


                if( list is IEnumerable serverList )
                {
                    foreach( DictionaryEntry i in serverList )
                    {
                        var data = new GameServer
                        {
                            Id = i.Key.ToString( ),
                            Url = new Uri( i.Value.ToString( ) ),
                        };

                        result.Add( data );
                    }
                }
            }

            return result;
        }

        public static Task<ServerConfiguration> FetchServerConfiguration( GameServer server )
        {
            return Task.Run( ( ) =>
            {
                var settings = new XmlReaderSettings( )
                {
                    IgnoreWhitespace = true,
                };

                using( var reader = XmlReader.Create( $"{server.Url}/interface.php?func=get_config", settings ) )
                {
                    var serializer = new XmlSerializer( typeof( ServerConfiguration ) );

                    return serializer.Deserialize( reader ) as ServerConfiguration;
                }
            } );
        }

        public static Task<UnitConfiguration> FetchUnitConfiguration( GameServer server )
        {
            return Task.Run( ( ) =>
            {
                var settings = new XmlReaderSettings( )
                {
                    IgnoreWhitespace = true,
                };

                using( var reader = XmlReader.Create( $"{server.Url}/interface.php?func=get_unit_info", settings ) )
                {
                    var serializer = new XmlSerializer( typeof( UnitConfiguration ) );

                    return serializer.Deserialize( reader ) as UnitConfiguration;
                }
            } );

        }
        #endregion


        #region Map Data

        public static Task<IReadOnlyList<Ally>> FetchAllies( GameServer server )
        {
            return CsvReader.DownloadFileAsync( new Uri( $"{server.Url}/map/ally.txt" ),
                ( buffer ) => new Ally( )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Tag = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Members = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    AllPoints = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( ),
                } );
        }

        public static Task<IReadOnlyList<Village>> FetchVillages( GameServer server )
        {
            return CsvReader.DownloadFileAsync( new Uri( $"{server.Url}/map/village.txt" ),
                ( buffer ) => new Village( )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    X = buffer.ReadInt( ),
                    Y = buffer.ReadInt( ),
                    Owner = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Bonus = buffer.ReadInt( ),
                } );
        }

        public static Task<IReadOnlyList<Player>> FetchPlayers( GameServer server )
        {
            return CsvReader.DownloadFileAsync( new Uri( $"{server.Url}/map/player.txt" ),
                ( buffer ) => new Player( )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Ally = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( )
                } );
        }

        public static Task<IReadOnlyList<Conquered>> FetchConquered( GameServer server )
        {
            return CsvReader.DownloadFileAsync( new Uri( $"{server.Url}/map/conquer.txt" ),
                ( buffer ) => new Conquered
                {
                    Id = buffer.ReadInt( ),
                    Time = new DateTime( 1970, 1, 1, 0, 0, 0, 0 )
                        .AddSeconds( buffer.ReadInt( ) ),

                    NewOwner = buffer.ReadInt( ),
                    OldOwner = buffer.ReadInt( )
                } );
        }
        #endregion


		#region Statistics
		public static Task<IReadOnlyList<TribeStatistic>> FetchTribeAttack( GameServer server )
        {
            return FetchTribe( server, "/map/kill_att_tribe.txt" );

        }

        public static Task<IReadOnlyList<TribeStatistic>> FetchTribeDefense( GameServer server )
        {
            return FetchTribe( server, "/map/kill_def_tribe.txt" );

        }

        public static Task<IReadOnlyList<TribeStatistic>> FetchTribeAll( GameServer server )
        {
            return FetchTribe( server, "/map/kill_all_tribe.txt" );
        }

        private static Task<IReadOnlyList<TribeStatistic>> FetchTribe( GameServer server, string filePath )
        {
            return CsvReader.DownloadFileAsync( new Uri( server.Url + filePath ),
                ( buffer ) => new TribeStatistic
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );
        }
		#endregion
    }
}
