using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Venom.Network.Rest
{
    public static class Client
    {
        private static readonly RestClient _client = new RestClient( "http://127.0.0.1" );

        private static IRestResponse CheckResponse( IRestResponse rep )
        {
            return rep;
        }


        #region Update
        public static void ReqUpdateList( )
        {
            var req = new RestRequest( "update/{ver}", Method.POST );
            {
                req.AddParameter( "ver", "" );
            }

            var rep = _client.Execute( req );
            
            if( rep.StatusCode == System.Net.HttpStatusCode.NotFound )
            {
                //=> TODO: Error
            }
            else
            {
                _client.DownloadData( req );
            }
        }

        public static Task DownloadUpdate()
        {
            throw new NotImplementedException( "DownloadUpdate" );
        }

        public static void ReqServerList()
        {
            var req = new RestRequest( "server/list", Method.GET );
            var rep = _client.Execute( req );

        }
        #endregion

        #region Login
        public static async Task<int> LoginAsync( string Username, string Password )
        {
            return 0;

            throw new NotImplementedException( "LoginAsync" );
        }
        #endregion
    }
}
