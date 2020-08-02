using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Venom.Data.Models;

namespace Venom.Data.Api
{
    public class ApiClient 
    {
        private readonly ILogger _logger;

        private HttpClient _ApiClient { get; set; }

        public ApiClient( ILogger<ApiClient> logger )
        {
            _logger = logger;

            _ApiClient = new HttpClient
            {
                BaseAddress = new Uri( "https://localhost:5001/api/" )
            };
            _ApiClient.DefaultRequestHeaders.Accept.Clear( );
            _ApiClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
        }

        public async Task Login( string Username, string Password )
        {
            using( var response = await _ApiClient.GetAsync( $"global/login" ) )
            {
                if( response.IsSuccessStatusCode )
                {
                    _ApiClient.DefaultRequestHeaders.Add( "X-API-KEY", "" ); //=> TODO: Add Response Key!
                }
                else
                {
                    throw new Exception( response.ReasonPhrase );
                }
            }
        }

        public async Task<List<ServerData>> FetchServerList()
        {
            using( var response = await _ApiClient.GetAsync( $"global/serverlist" ) )
            {
                if( response.IsSuccessStatusCode )
                {
                    _logger.LogInformation( $"retrieving ServerList" );
                    return JsonConvert.DeserializeObject<List<ServerData>>( await response.Content.ReadAsStringAsync( ) );
                }
                else
                {
                    throw new Exception( response.ReasonPhrase );
                }
            }
        }

        // https://localhost:5001/api/Data/Village?Server=2

        public async Task<List<Village>> FetchVillages( int Server )
        {
            using( var response = await _ApiClient.GetAsync( $"Data/Village?Server={Server}" ) )
            {
                if( response.IsSuccessStatusCode )
                {
                    _logger.LogInformation( $"retrieving Villages from Server {Server}" );
                    return JsonConvert.DeserializeObject<List<Village>>( await response.Content.ReadAsStringAsync( ) );
                }
                else
                {
                    throw new Exception( response.ReasonPhrase );
                }
            }
        }
    }
}
