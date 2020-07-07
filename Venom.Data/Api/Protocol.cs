using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Venom.Data.Models;

namespace Venom.Data.Api
{
    public static class Protocol
    {
        private static readonly string DefaultApiData = "https://localhost:5001/api/data/";
        private static readonly string DefaultApiGlobal = "https://localhost:5001/api/global/";

        public enum LoginTypes
        {
            Blocked,
            Payment,
            WrongData,
            Success
        }

        public static async Task<List<ServerData>> GetServerList()
            => JsonConvert.DeserializeObject<List<ServerData>>( await GetStreamData( DefaultApiGlobal + "serverlist" ) );

        public static async Task<List<Player>> GetGameDataPlayer( int Server ) 
            => JsonConvert.DeserializeObject<List<Player>>( await GetStreamData( DefaultApiData + $"{Server}/Player" ) );

        public static async Task<List<Ally>> GetGameDataAlly( int Server )
            => JsonConvert.DeserializeObject<List<Ally>>( await GetStreamData( DefaultApiData + $"{Server}/Ally" ) );

        public static async Task<List<Village>> GetGameDataVillage( )
            => JsonConvert.DeserializeObject<List<Village>>( await GetStreamData( DefaultApiData + "village" ) );

        private static async Task<string> GetStreamData( string Url )
        {
            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( Url );
                var data = new StreamReader( stream )
                    .ReadToEnd( );
                return data;
            }
        }
    }
}
