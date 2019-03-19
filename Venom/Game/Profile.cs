using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Venom.Game
{
    public class Profile
    {
        private List<ProfileData> _Profiles = new List<ProfileData>( );

        private ProfileData _Local = default;

        public Profile( )
        {
        }

        public void Save( )
        {
            using( var sw = File.CreateText( @".\Profiles.json" ) )
            {
                var js = new JsonSerializer( );
                js.Serialize( sw, _Profiles );
            }
        }

        public void Load( )
        {
            if( File.Exists( @".\Profiles.json" ) )
            {
                using( var sr = File.OpenText( @".\Profiles.json" ) )
                {
                    _Profiles = JsonConvert.DeserializeObject<List<ProfileData>>( sr.ReadToEnd( ) );
                }
            }
        }

        public bool Add( string name, string server )
        {
            var exists = _Profiles.Any( x => x.Name.Equals( name ) && x.Server.Equals( server ) );
            if( exists == false )
            {
                _Profiles.Add( new ProfileData( ) { Name = name, Server = server } );
                return true;
            }
            return false; //=> Profile already exists with same server!
        }

        public void Remove( ProfileData profile ) => _Profiles.Remove( profile );

        public ProfileData Get( string name, string server ) => _Profiles.FirstOrDefault( x => x.Name.Equals( name ) && x.Server.Equals( server ) );
        public IEnumerable<ProfileData> GetList() => _Profiles;

        public ProfileData Local
        {
            get => _Local;
            set => _Local = value;
        }
    }

    public class ProfileData
    {
        [JsonProperty( "Name" )]
        public string Name { get; set; }
        [JsonProperty( "Server" )]
        public string Server { get; set; }
    }
}
