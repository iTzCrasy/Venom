using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Venom.Game
{
    public class ProfileData
    {
        [JsonProperty( "Name" )]
        public string Name { get; set; }

        [JsonProperty( "Server" )]
        public string Server { get; set; }
    }

    public class Profile
    {
        private List<ProfileData> _profiles = new List<ProfileData>( );

        public ProfileData Local { get; set; } = default;


        public Profile( )
        {
        }

        public void Save( )
        {
            using( var sw = File.CreateText( @".\Profiles.json" ) )
            {
                var js = new JsonSerializer( );
                js.Serialize( sw, _profiles );
            }
        }

        public void Load( )
        {
            if( File.Exists( @".\Profiles.json" ) )
            {
                using( var sr = File.OpenText( @".\Profiles.json" ) )
                {
                    _profiles = JsonConvert.DeserializeObject<List<ProfileData>>( sr.ReadToEnd( ) );
                }
            }
        }

        public bool Add( string name, string server )
        {
            var exists = _profiles.Any( x => x.Name.Equals( name ) && x.Server.Equals( server ) );
            if( exists == false )
            {
                _profiles.Add( new ProfileData( ) { Name = name, Server = server } );
                return true;
            }
            return false; //=> Profile already exists with same server!
        }

        public void Remove( ProfileData profile ) => _profiles.Remove( profile );

        public ProfileData Get( string name, string server ) => _profiles.FirstOrDefault( x => x.Name.Equals( name ) && x.Server.Equals( server ) );
        public IEnumerable<ProfileData> GetList() => _profiles;
    }
}
