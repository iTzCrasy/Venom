using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Venom.Data.Cache
{
    internal class CacheManager
    {
        // Path.RemoveRelativeSegments(string path);
        private readonly string _localPath = Path.Combine( Directory.GetCurrentDirectory( ), "Cache\\" );
        private readonly ILogger _logger;

        private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>( );



        public CacheManager(
            ILogger<CacheManager> logger
            )
        {
            _logger = logger;

            if( !Directory.Exists( _localPath ))
            {
                Directory.CreateDirectory( _localPath );
            }
        }



        public async Task<T> Get<T>( string cacheKey, Func<Task<T>> request )
        {
            if( _cache.TryGetValue( cacheKey, out var path ) )
            {
                using( var reader = new StreamReader( path ) )
                {
                    _logger.LogInformation( $"retrieving cached item {cacheKey} from {path}" );

                    var json = await reader.ReadToEndAsync( );

                    return await Task.Run( ( ) => JsonConvert.DeserializeObject<T>( json ) );
                }
            }
            else
            {
                var data = await request( );

                return await Put( cacheKey, data );
            }
        }


        public async Task<T> Put<T>( string cacheKey, T data )
        {
            var path = Path.Combine( _localPath, $"{cacheKey}.json" );

            _logger.LogInformation( $"requesting {cacheKey}, {path}" );


            var text = await Task.Run( ( ) => JsonConvert.SerializeObject( data ) );

            using( var writer = new StreamWriter( path, false, Encoding.UTF8 ) )
            {
                await writer.WriteAsync( text );
            }

            return data;
        }
    }
}
