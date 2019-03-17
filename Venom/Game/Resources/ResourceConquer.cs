using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourceConquer : IResource
    {
        private readonly List<ConquerData> _conquerData = new List<ConquerData>( );

        public ResourceConquer()
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            var conquerData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/conquer.txt" ),
                ( buffer ) => new ConquerData
                {
                    Id = buffer.ReadInt( ),
                    Time = buffer.ReadInt( ),
                    NewOwner = buffer.ReadInt( ),
                    OldOwner = buffer.ReadInt( )
                } );

            foreach( var i in conquerData )
            {
                _conquerData.Add( i );
            }
        }
    }

    public class ConquerData
    {
        //=> $village_id, $unix_timestamp, $new_owner, $old_owner
        public int Id { get; set; }
        public int Time { get; set; }
        public int NewOwner { get; set; }
        public int OldOwner { get; set; }
    }
}
