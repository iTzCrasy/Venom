using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;

namespace Venom.Data.Api
{
    public interface IApiClient
    {
        void Initialize( );

        Task Login( string Username, string Password );
        Task<List<ServerData>> FetchServerList( );
        Task<List<Village>> FetchVillages( int Server );
    }
}
