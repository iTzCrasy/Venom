using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Venom.Utility
{
    internal class Config
    {
        private readonly ILogger _logger;

        public Config( ILogger<Config> logger )
        {
            _logger = logger;
        }

        public void Load()
        {
            if( !File.Exists( "Venom.json" ) )
            {
                _logger.LogWarning( "Local config file not found! --> create basic" );
            }
        }
    }
}
