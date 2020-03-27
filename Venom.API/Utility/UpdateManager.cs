using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Hosting;
using Venom.API.Context;

namespace Venom.API.Utility
{
    public class UpdateManager
    {
        private readonly DataContext _dataContext;

        public UpdateManager( DataContext dataContext )
        {
            _dataContext = dataContext;
        }

        public void Initialize()
        {
        }

        private async Task UpdateGameServerAsync()
        {
            //=> 

            await Task.Run( async ( ) =>
            {
            } );
        }
    }
}
