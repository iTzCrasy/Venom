using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Venom.API.Data.Utility
{
    public static class WebManager
    {
        public static async Task<DateTimeOffset?> GetFileHeader( Uri uri )
        {
            using( var client = new HttpClient() )
            {
                var response = await client.GetAsync( uri )
                    .ConfigureAwait( false );

                return response.Content.Headers.LastModified;
            }
        }
    }
}
