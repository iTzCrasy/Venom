using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Venom.Game.Resources;

namespace Venom.Core
{
    internal class Game
    {
        public Game()
        {
        }

        /// <summary>
        /// Server Base
        /// </summary>
        protected ServerInfo SelectedServer
        {
            get;
            set;
        } = default( ServerInfo );

        public void SetSelectedServer( string ID ) => SelectedServer = _ServerList.FirstOrDefault( p => p.ID.Equals( ID ) );
        public ServerInfo GetSelectedServer() => SelectedServer;

        public void LoadServerList()
        {
            if( _ServerList.Count.Equals( 0 ) ) //=> Check Empty list, if empty LOAD!
            {
                var ObjectList = new DeserializePHP( Network.GetUrlRead( "http://www.die-staemme.de/backend/get_servers.php" ) ).Deserialize();
                if (ObjectList is IEnumerable Enum)
                {
                    foreach (DictionaryEntry Item in Enum)
                    {
                        _ServerList.Add( new ServerInfo() { ID = Item.Key.ToString(), Url = Item.Value.ToString() } );
                    }
                }
            }
        }

        public List<ServerInfo> GetServerList() => _ServerList; 
        protected List<ServerInfo> _ServerList = new List<ServerInfo>();
    }

    public struct ServerInfo
    {
        public string ID { get; set; }
        public string Url { get; set; }
    }
}
