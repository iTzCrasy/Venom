using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data.Models;

namespace Venom.Data
{
    public class DataContext
    {
        private readonly ILogger _logger;

        public DataContext(
            ILogger<DataContext> logger
            )
        {
            _logger = logger;
        }


        public Task<List<GameServer>> GetGameServers( )
        {
            // todo implement caching layer

            return ServerApi.FetchGameServers( );
        }

    }
}
