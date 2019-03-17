using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    internal class BashpointAllyResource : IResource
    {
        private readonly Dictionary<int, BashpointAllyData> _bashpointAtt = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointDef = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointAll = new Dictionary<int, BashpointAllyData>( );

        public BashpointAllyResource( )
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            //=> Loading Bashpoints Att
            var bashpointsAllysAtt = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_att_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAllysAtt )
            {
                _bashpointAtt.Add( i.Id, i );
            }


            var bashpointsAllysDef = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_def_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAllysDef )
            {
                _bashpointDef.Add( i.Id, i );
            }

            var bashpointsAllysAll = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_all_tribe.txt" ),
                ( buffer ) => new BashpointAllyData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAllysAll )
            {
                _bashpointAll.Add( i.Id, i );
            }
        }

        public BashpointAllyData GetBashpointAtt( AllyData data )
            => _bashpointAtt.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public BashpointAllyData GetBashpointDef( AllyData data )
            => _bashpointDef.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public BashpointAllyData GetBashpointAll( AllyData data )
            => _bashpointAll.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
    }

    public class BashpointAllyData
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }
}
