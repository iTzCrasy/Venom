using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    internal class BashpointPlayerResource : IResource
    {
        private readonly Dictionary<int, BashpointPlayerData> _bashpointAtt = new Dictionary<int, BashpointPlayerData>( );
        private readonly Dictionary<int, BashpointPlayerData> _bashpointDef = new Dictionary<int, BashpointPlayerData>( );
        private readonly Dictionary<int, BashpointPlayerData> _bashpointAll = new Dictionary<int, BashpointPlayerData>( );

        public BashpointPlayerResource()
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            var bashpointsPlayerDataAtt = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_att.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsPlayerDataAtt )
            {
                _bashpointAtt.Add( i.Id, i );
            }

            var bashpointsPlayerDataDef = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_def.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsPlayerDataDef )
            {
                _bashpointDef.Add( i.Id, i );
            }

            var bashpointsPlayerDataAll = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_all.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsPlayerDataAll )
            {
                _bashpointAll.Add( i.Id, i );
            }
        }

        public BashpointPlayerData GetBashpointAtt( PlayerData data ) =>
            _bashpointAtt.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
        public BashpointPlayerData GetBashpointDef( PlayerData data ) =>
            _bashpointDef.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
        public BashpointPlayerData GetBashpointAll( PlayerData data ) =>
            _bashpointAll.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
    }

    public class BashpointPlayerData
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }
}
