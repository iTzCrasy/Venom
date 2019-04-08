using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// Loading Server Config, Units, Buildings
        /// </summary>
        /// <param name="serverId"></param>
        public void Load( string serverId )
        {
            _Local = _ServerData.FirstOrDefault( x => x.Id.Equals( serverId ) );

            using( var web = new WebClient( ) )
            {
                var xml = new XmlDocument( );

                //=> Config
                xml.LoadXml( new StreamReader( web.OpenRead( new Uri( _Local.Url + "/interface.php?func=get_config" ) ) ).ReadToEnd( ).Replace( "\n", "" ) );
                _Local.Config = new ServerConfig( JObject.Parse( JsonConvert.SerializeXmlNode( xml ) ) );

                //=> Config Units
                xml.LoadXml( new StreamReader( web.OpenRead( new Uri( _Local.Url + "/interface.php?func=get_unit_info" ) ) ).ReadToEnd( ).Replace( "\n", "" ) );
                _Local.ConfigUnits = new ServerConfigUnits( JObject.Parse( JsonConvert.SerializeXmlNode( xml ) ) );
            }
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

        public ServerConfig Config { get; set; }
        public ServerConfigUnits ConfigUnits { get; set; }
    }

    public class ServerConfig
    {
        private readonly JObject _jObject;
        public ServerConfig( JObject jObject )
        {
            _jObject = jObject;
        }

        //=> Base
        public JToken Speed => _jObject["config"]["speed"];
        public JToken UnitSpeed => _jObject["config"]["unit_speed"];
        public JToken Moral => _jObject["config"]["moral"];

        //=> Game
        public bool Archer => _jObject["config"]["game"]["archer"].ToObject<int>() >= 1 ? true : false;
    }

    public class ServerConfigUnits
    {
        private readonly JObject _jObject;
        public ServerConfigUnits( JObject jObject )
        {
            _jObject = jObject;
        }

        public T GetConfig<T>( string unit, string data ) => _jObject["config"][unit][data].ToObject<T>( );
    }

    public struct ConfigUnitInfo
    {
        public double Build;
        public int Pop;
    }
}

/*
 * <sword>
<build_time>937.5</build_time>
<pop>1</pop>
<speed>21.999999999296</speed>
<attack>25</attack>
<defense>50</defense>
<defense_cavalry>25</defense_cavalry>
<defense_archer>40</defense_archer>
<carry>15</carry>
</sword>
*/
