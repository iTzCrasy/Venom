using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class GameBashpoint
    {
        //=> $rank, $id, $kills
        public int Id { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }


    internal class BashpointAlly : IResource
    {
        private readonly Dictionary<int, GameBashpoint> _bashpointAttack = new Dictionary<int, GameBashpoint>( );
        private readonly Dictionary<int, GameBashpoint> _bashpointDef = new Dictionary<int, GameBashpoint>( );
        private readonly Dictionary<int, GameBashpoint> _bashpointAll = new Dictionary<int, GameBashpoint>( );




        public BashpointAlly( )
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            //=> Loading Bashpoints Att
            var bashpointsAllysAtt = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_att_tribe.txt" ),
                ( buffer ) => new GameBashpoint
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

            foreach( var i in bashpointsAllysAtt )
            {
                _bashpointAttack.Add( i.Id, i );
            }


            var bashpointsAllysDef = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/kill_def_tribe.txt" ),
                ( buffer ) => new GameBashpoint
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
                ( buffer ) => new GameBashpoint
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



        public GameBashpoint GetBashpointAttack( AllyData data )
            => _bashpointAttack.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public GameBashpoint GetBashpointDef( AllyData data )
            => _bashpointDef.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;

        public GameBashpoint GetBashpointAll( AllyData data )
            => _bashpointAll.TryGetValue( data.Id, out var bashpoint ) ? bashpoint : null;
    }
}
