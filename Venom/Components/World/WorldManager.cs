using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Repositories;

namespace Venom.Components.World
{
    public class WorldManager
    {
        protected byte[] _decoration = new byte[1000000];
        private readonly IResourceRepository _resourceRepository;

        public byte GetDecoration( int test ) => _decoration[test];

        public WorldManager( IResourceRepository resourceRepository )
        {
            _resourceRepository = resourceRepository;
        }

        public Task Initialize()
        {
            var tasks = new List<Task>
            {
                LoadDecorationFile(),
            };

            return Task.WhenAll( tasks );
        }

        private Task LoadDecorationFile( )
        {
            return _resourceRepository.OpenEntryAsync( "world.dat", async ( entry ) =>
            {
                using( var stream = entry.Open() )
                {
                    var buffer = new byte[4096];
                    var offset = 0;
                    var read = 0;

                    while( ( read = await stream.ReadAsync( buffer, 0, 4096 ) ) > 0 )
                    {
                        Array.Copy( buffer, 0, _decoration, offset, read );
                        offset += read;
                    }
                }
            } );
        }
    }
}
