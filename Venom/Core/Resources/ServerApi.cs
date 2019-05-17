using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Venom.Core.Resources
{
    [DebuggerDisplay( "{Id} : {Url}" )]
    public class ServerData
    {
        public string Id { get; set; }

        public Uri Url { get; set; }
    }


    #region Server Config
    [Serializable]
    [XmlRoot( "config" )]
    public class ServerConfig
    {
        [XmlElement( "speed" )]
        public float Speed { get; set; }

        [XmlElement( "unit_speed" )]
        public float UnitSpeed { get; set; }

        [XmlElement( "moral" )]
        public float Moral { get; set; }

        [XmlElement( "game" )]
        public ServerGameConfig Game { get; set; }

        [XmlElement( "coord" )]
        public ServerGameCoord Coord { get; set; }
    }

    [Serializable]
    [XmlType( "game" )]
    public class ServerGameConfig
    {
        [XmlElement( "archer" )]
        public bool Archer { get; set; }
    }

    [Serializable]
    [XmlType( "coord" )]
    public class ServerGameCoord
    {
        [XmlElement( "map_size" )]
        public int MapSize { get; set; }

        [XmlElement( "func" )]
        public int Func { get; set; }

        [XmlElement( "empty_villages" )]
        public int EmptyVillages { get; set; }

        [XmlElement( "bonus_villages" )]
        public int BonusVillages { get; set; }

        [XmlElement( "bonus_new" )]
        public int BonusNew { get; set; }

        [XmlElement( "inner" )]
        public int Inner { get; set; }

        [XmlElement( "select_start" )]
        public int SelectStart { get; set; }

        [XmlElement( "village_move_wait" )]
        public int VillageMoveWait { get; set; }

        [XmlElement( "noble_restart" )]
        public int NobleRestart { get; set; }

        [XmlElement( "start_villages" )]
        public int StartVillages { get; set; }
    }
    #endregion


    #region Unit Config
    [Serializable]
    [XmlType( "config" )]
    public class ServerUnitConfig
    {
        [XmlElement( "spear" )]
        public ServerUnitTypeConfig Spear { get; set; }

        [XmlElement( "sword" )]
        public ServerUnitTypeConfig Sword { get; set; }

        [XmlElement( "axe" )]
        public ServerUnitTypeConfig Axe { get; set; }

        [XmlElement( "spy" )]
        public ServerUnitTypeConfig Spy { get; set; }

        [XmlElement( "light" )]
        public ServerUnitTypeConfig Light { get; set; }

        [XmlElement( "heavy" )]
        public ServerUnitTypeConfig Heavy { get; set; }

        [XmlElement( "ram" )]
        public ServerUnitTypeConfig Ram { get; set; }

        [XmlElement( "catapult" )]
        public ServerUnitTypeConfig Catapult { get; set; }

        [XmlElement( "knight" )]
        public ServerUnitTypeConfig Knight { get; set; }

        [XmlElement( "snob" )]
        public ServerUnitTypeConfig Snob { get; set; }

        [XmlElement( "militia" )]
        public ServerUnitTypeConfig Militia { get; set; }
    }


    [Serializable]
    public class ServerUnitTypeConfig
    {
        [XmlElement( "build_time" )]
        public float BuildTime { get; set; }

        [XmlElement( "pop" )]
        public int Pop { get; set; }

        [XmlElement( "speed" )]
        public double Speed { get; set; }

        [XmlElement( "attack" )]
        public int Attack { get; set; }

        [XmlElement( "defense" )]
        public int Defense { get; set; }

        [XmlElement( "defense_cavalry" )]
        public int DefenseCavalry { get; set; }

        [XmlElement( "defense_archer" )]
        public int DefenseArcher { get; set; }

        [XmlElement( "carry" )]
        public int Carry { get; set; }
    }
    #endregion


    public class AllyDataEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public int Members { get; set; }

        public int Villages { get; set; }

        public int Points { get; set; }

        public int AllPoints { get; set; }

        public int Rank { get; set; }
    }

    public class TribeStatDataEntry
    {
        public int Id { get; set; }

        public int Rank { get; set; }

        public long Kills { get; set; }
    }

    public class ConquerDataEntry
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public int NewOwner { get; set; }

        public int OldOwner { get; set; }
    }
    
    public class PlayerDataEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Ally { get; set; }

        public int Villages { get; set; }

        public int Points { get; set; }

        public int Rank { get; set; }
    }

    public class VillageDataEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Owner { get; set; }

        public int Points { get; set; }

        public int Bonus { get; set; }
    }


    public static class ServerApi
    {
        #region Server Config
        public static async Task<List<ServerData>> FetchServers( )
        {
            var result = new List<ServerData>( );

            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( "http://www.die-staemme.de/backend/get_servers.php" )
                    .ConfigureAwait( false );

                var text = new StreamReader( stream )
                    .ReadToEnd( );

                var list = DeserializePHP.Deserialize( text );


                if( list is IEnumerable serverList )
                {
                    foreach( DictionaryEntry i in serverList )
                    {
                        var data = new ServerData
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

        public static Task<ServerConfig> FetchServerConfig( ServerData data )
        {
            return Task.Run( ( ) =>
            {
                var settings = new XmlReaderSettings( )
                {
                    IgnoreWhitespace = true,
                };

                using( var reader = XmlReader.Create( $"{data.Url}/interface.php?func=get_config", settings ) )
                {
                    var serializer = new XmlSerializer( typeof( ServerConfig ) );

                    return ( ServerConfig )serializer.Deserialize( reader );
                }
            } );
        }

        public static Task<ServerUnitConfig> FetchServerUnitInfo( ServerData data )
        {
            return Task.Run( ( ) =>
            {
                var settings = new XmlReaderSettings( )
                {
                    IgnoreWhitespace = true,
                };

                using( var reader = XmlReader.Create( $"{data.Url}/interface.php?func=get_unit_info", settings ) )
                {
                    var serializer = new XmlSerializer( typeof( ServerUnitConfig ) );

                    return ( ServerUnitConfig )serializer.Deserialize( reader );
                }
            } );
        }
        #endregion


        #region Tribe
        public static Task<IReadOnlyList<TribeStatDataEntry>> FetchKillAttackTribe( ServerData data )
        {
            return FetchTribeData( data, "/map/kill_att_tribe.txt" );
        }

        public static Task<IReadOnlyList<TribeStatDataEntry>> FetchKillDefenseTribe( ServerData data )
        {
            return FetchTribeData( data, "/map/kill_def_tribe.txt" );
        }

        public static Task<IReadOnlyList<TribeStatDataEntry>> FetchKillAllTribe( ServerData data )
        {
            return FetchTribeData( data, "/map/kill_all_tribe.txt" );
        }

        private static Task<IReadOnlyList<TribeStatDataEntry>> FetchTribeData( ServerData data, string filePath )
        {
            return CSVReader.DownloadFileAsync( new Uri( data.Url + filePath ),
                ( buffer ) => new TribeStatDataEntry
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );
        }
        #endregion

        public static Task<IReadOnlyList<AllyDataEntry>> FetchAllyData( ServerData data )
        {
            return CSVReader.DownloadFileAsync( new Uri( $"{data.Url}/map/ally.txt" ),
                ( buffer ) => new AllyDataEntry( )
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


        public static Task<IReadOnlyList<ConquerDataEntry>> FetchConquerData( ServerData data )
        {
            return CSVReader.DownloadFileAsync( new Uri( $"{data.Url}/map/conquer.txt" ),
                ( buffer ) => new ConquerDataEntry
                {
                    Id = buffer.ReadInt( ),

                    Time = new DateTime( 1970, 1, 1, 0, 0, 0, 0 )
                        .AddSeconds( buffer.ReadInt( ) ),

                    NewOwner = buffer.ReadInt( ),

                    OldOwner = buffer.ReadInt( )
                } );
        }


        public static Task<IReadOnlyList<PlayerDataEntry>> FetchPlayerData( ServerData data )
        {
            return CSVReader.DownloadFileAsync( new Uri( $"{data.Url}/map/player.txt" ),
                ( buffer ) => new PlayerDataEntry( )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Ally = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( )
                } );
        }

        public static Task<IReadOnlyList<VillageDataEntry>> FetchVillageData( ServerData data )
        {
            return CSVReader.DownloadFileAsync( new Uri( $"{data.Url}/map/village.txt" ),
                ( buffer ) => new VillageDataEntry( )
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
    }
}
