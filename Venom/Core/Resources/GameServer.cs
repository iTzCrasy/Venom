using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Core.Resources
{
    public class ServerData
    {

    }

    public class GameServer
    {

        public async Task FetchServers( )
        {
            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( "http://www.die-staemme.de/backend/get_servers.php" )
                    .ConfigureAwait( false );

                var text = new StreamReader( stream )
                    .ReadToEnd( );

                var list = DeserializePHP.Deserialize( text );

                if( list is IEnumerable serverList )
                {

                }
                else
                {
                    throw new IOException( "serverList is not an IEnumerable!" );
                }
            }
        }


    }
}
