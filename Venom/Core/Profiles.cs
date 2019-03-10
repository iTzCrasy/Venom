using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Profiles /*: Singleton<Profiles>*/
    {
        public static void SaveProfiles()
        {
            using( var sw = File.CreateText( @".\Profiles.json" ) )
            {
                var js = new JsonSerializer();
                js.Serialize( sw, _ProfileList );
            }
        }

        public static void Load()
        {
            if( File.Exists( @".\Profiles.json" ) == false )
                return;

            using( var sr = File.OpenText( @".\Profiles.json" ) )
            {
                _ProfileList = JsonConvert.DeserializeObject<List<Profile>>( sr.ReadToEnd() );
            }
        }

        public static bool AddProfile( string Name, string Server )
        {
            var Check = GetProfile( Name, Server );
            //if( Check.Equals( default( Profile ) ) )
            //{
            //    //=> TODO: Handle Error!
            //    return false; 
            //}
            _ProfileList.Add( new Profile { Name = Name, Server = Server } );
            return true;
        }

        public static Profile GetProfile( string Name, string Server )
        {
            foreach( var Item in _ProfileList )
            {
                if( Item.Name.Equals( Name ) &&
                    Item.Server.Equals( Server ))
                {
                    return Item;
                }
            }
            return default( Profile );
        }

        public static void RemoveProfie( string Name, string Server )
        {
            _ProfileList.Remove( GetProfile( Name, Server ) );
        }

        public static List<Profile> GetProfileList() => _ProfileList;

        [JsonProperty( "Profiles" )]
        public static List< Profile > _ProfileList = new List< Profile >();

    }

    public struct Profile
    {
        [JsonProperty( "Name" )]
        public string Name { get; set; }

        [JsonProperty( "Server" )]
        public string Server { get; set; }
    }
}

