using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourceBashpointAlly : IResource
    {
        private readonly Dictionary<int, BashpointAllyData> _bashpointAttData = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointDefData = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointAllData = new Dictionary<int, BashpointAllyData>( );

        public ResourceBashpointAlly( )
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            //=> Loading Bashpoints Att
            var bashpointsAttData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_att_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAttData )
            {
                _bashpointAttData.Add( i.Id, i );
            }


            var bashpointsDefData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_def_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsDefData )
            {
                _bashpointDefData.Add( i.Id, i );
            }

            var bashpointsAllData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_all_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAllData )
            {
                _bashpointAllData.Add( i.Id, i );
            }
        }

        public BashpointAllyData GetBashpointAtt( AllyData data ) => 
            _bashpointAttData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public BashpointAllyData GetBashpointDef( AllyData data ) => 
            _bashpointDefData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public BashpointAllyData GetBashpointAll( AllyData data ) => 
            _bashpointAllData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
    }

    public class BashpointAllyData
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }
}
