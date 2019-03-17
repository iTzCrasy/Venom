using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourceBashpointPlayer : IResource
    {
        private readonly Dictionary<int, BashpointPlayerData> _bashpointAttData = new Dictionary<int, BashpointPlayerData>( );
        private readonly Dictionary<int, BashpointPlayerData> _bashpointDefData = new Dictionary<int, BashpointPlayerData>( );
        private readonly Dictionary<int, BashpointPlayerData> _bashpointAllData = new Dictionary<int, BashpointPlayerData>( );

        public ResourceBashpointPlayer()
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            var bashpointAttData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_att.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointAttData )
            {
                _bashpointAttData.Add( i.Id, i );
            }

            var bashpointDefData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_def.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointDefData )
            {
                _bashpointDefData.Add( i.Id, i );
            }

            var bashpointAllData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_all.txt" ),
                ( buffer ) => new BashpointPlayerData
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointAllData )
            {
                _bashpointAllData.Add( i.Id, i );
            }
        }

        public BashpointPlayerData GetBashpointAtt( PlayerData data ) =>
            _bashpointAttData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
        public BashpointPlayerData GetBashpointDef( PlayerData data ) =>
            _bashpointDefData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
        public BashpointPlayerData GetBashpointAll( PlayerData data ) =>
            _bashpointAllData.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
    }

    public class BashpointPlayerData
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }
}
