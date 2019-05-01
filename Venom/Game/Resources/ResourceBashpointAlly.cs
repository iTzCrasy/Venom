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
        private readonly Server _server;

        private readonly Dictionary<int, BashpointAllyData> _bashpointAttData = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointDefData = new Dictionary<int, BashpointAllyData>( );
        private readonly Dictionary<int, BashpointAllyData> _bashpointAllData = new Dictionary<int, BashpointAllyData>( );

        public ResourceBashpointAlly( Server server )
        {
            _server = server;
        }

        public async Task InitializeAsync()
        {
            //=> Loading Bashpoints Att
            var bashpointsAttData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/kill_att_tribe.txt" ),
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

            //=> Loading Bashpoints Def
            var bashpointsDefData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/kill_def_tribe.txt" ),
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


            //=> Loading Bashpoints All
            var bashpointsAllData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/kill_all_tribe.txt" ),
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

        public List<BashpointAllyData> GetBashpointAttList( ) =>
            _bashpointAttData.Values.ToList( );
        public List<BashpointAllyData> GetBashpointDefList( ) =>
            _bashpointDefData.Values.ToList( );
        public List<BashpointAllyData> GetBashpointAllList( ) =>
            _bashpointAllData.Values.ToList();
    }

    public class BashpointAllyData
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }
}
