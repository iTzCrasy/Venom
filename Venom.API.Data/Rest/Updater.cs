using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Venom.API.Data.Database;
using Venom.API.Data.Database.Entities;
using Venom.API.Data.Database.Global.Entities;
using Venom.API.Data.Utility;

namespace Venom.API.Data.Rest
{
    public static class Updater
    {
        private static bool IsServerValid( string Id )
        {
            var serverFilter = new string[] { "dep", "dec", "des" };
            foreach( var f in serverFilter )
            {
                if( Id.Contains( f ) )
                {
                    return false;
                }
            }
            return true;
        }

        public static async Task<List<ServerData>> FetchServerList( )
        {
            var result = new List<ServerData>( );
            char[] trimId = { 'd', 'e' };

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
                        //=> Check Server, only normal server!
                        if( !IsServerValid( i.Key.ToString() ) )
                            continue;

                        var data = new ServerData
                        {
                            World = int.Parse( i.Key.ToString( ).Trim( trimId ) ),
                            Url = i.Value.ToString( )
                        };
                        result.Add( data );
                    }
                }
            }

            return result;
        }

        public static Task<IReadOnlyList<Player>> FetchPlayerList( ServerData server )
        {
            var reader = new CsvReader( $"{server.Url}/map/player.txt" );
            if( reader.LoadFileAsync().Result )
            {
                server.Size += reader.ContentLength;
                server.Seeds.Player = reader.ModifyDate;

                return reader.ReadFileAsync( ( buffer ) => new Player( )
                {
                    PlayerId = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Ally = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( )
                } );
            }
            return default;
        }
    }
}
