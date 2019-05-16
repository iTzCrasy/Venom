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

namespace Venom.Core.Resources.API
{
    [DebuggerDisplay( "{Id} : {Url}" )]
    public class ServerData
    {
        public string Id { get; set; }

        public Uri Url { get; set; }
    }


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




    public static class GameServer
    {
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

        public static async Task FetchServerUnitInfo( ServerData data )
        {
            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( $"{data.Url}/interface.php?func=get_unit_info" )
                    .ConfigureAwait( false );

                var text = new StreamReader( stream )
                    .ReadToEnd( );

                var document = new XmlDocument
                {
                    XmlResolver = new XmlSecureResolver( new XmlUrlResolver( ), data.Url.ToString( ) ),
                };


            }
        }

    }
}
