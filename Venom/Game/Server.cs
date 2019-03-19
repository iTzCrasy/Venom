using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Venom.Core;

namespace Venom.Game
{
    public class Server
    {
        private readonly List<ServerData> _ServerData = new List<ServerData>( );
        private ServerData _Local = default;

        public Server()
        {

        }

        /// <summary>
        /// Loading Server List
        /// </summary>
        public void Load()
        {
            using( var web = new WebClient( ) )
            {
                var stream = new StreamReader( web.OpenRead( new Uri( "http://www.die-staemme.de/backend/get_servers.php" ) ) ).ReadToEnd( );
                var objlist = new DeserializePHP( stream ).Deserialize( );
                if( objlist is IEnumerable data )
                {
                    foreach( DictionaryEntry item in data )
                    {
                        _ServerData.Add( new ServerData( ) { Id = item.Key.ToString( ), Url = item.Value.ToString( ) } );
                    }
                }
            }
        }

        /// <summary>
        /// Loading Server 
        /// </summary>
        public void LoadConfig( string serverId )
        {
            //=> TODO: Implement loading server config!
            _Local = _ServerData.FirstOrDefault( x => x.Id.Equals( serverId ) );

        }

        public IEnumerable<ServerData> GetList( ) => _ServerData;

        public ServerData Local
        {
            get => _Local;
            set => _Local = value;
        }
    }

    public class ServerData
    {
        public string Id { get; set; }
        public string Url { get; set; }
    }
}
