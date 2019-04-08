using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourcePlayer : IResource
    {
        private readonly Server _server;
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourceBashpointPlayer _resourceBashpoint;
        private readonly ResourceVillage _resourceVillage;

        private readonly Dictionary<int, PlayerData> _playerData = new Dictionary<int, PlayerData>( );
        private readonly Dictionary<string, PlayerData> _playerDataByName = new Dictionary<string, PlayerData>( );

        public ResourcePlayer( 
            Server server,
            ResourceAlly resourceAlly,
            ResourceBashpointPlayer resourceBashpoint,
            ResourceVillage resourceVillage )
        {
            _server = server;
            _resourceAlly = resourceAlly;
            _resourceBashpoint = resourceBashpoint;
            _resourceVillage = resourceVillage;
        }

        public async Task InitializeAsync()
        {
            var playerData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/player.txt" ),
                ( buffer ) => new PlayerData( _resourceAlly, _resourceBashpoint, _resourceVillage )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Ally = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( )
                } );

            foreach( var i in playerData )
            {
                AddPlayer( i );
            }
        }

        private void AddPlayer( PlayerData data )
        {
            _playerData.Add( data.Id, data );
            _playerDataByName.Add( data.Name, data );
        }

        public IEnumerable<PlayerData> GetPlayerList( ) => 
            _playerData.Values.ToList( ).Where( x => x.Points > 0 );

        public PlayerData GetPlayerById( int id ) =>
            _playerData.TryGetValue( id, out var player ) ? player : new PlayerData( null, null, null );

        public PlayerData GetPlayerByName( string name ) =>
            _playerDataByName.TryGetValue( name, out var player ) ? player : new PlayerData( null, null, null );

        public int GetCount( ) =>
            _playerData.Count( );
    }

    public class PlayerData
    {
        /// <summary>
        /// Constructor, Injection
        /// </summary>
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourceBashpointPlayer _resourceBashpoint;
        private readonly ResourceVillage _resourceVillage;
        public PlayerData( 
            ResourceAlly resourceAlly,
            ResourceBashpointPlayer resourceBashpoint,
            ResourceVillage resourceVillage )
        {
            _resourceAlly = resourceAlly;
            _resourceBashpoint = resourceBashpoint;
            _resourceVillage = resourceVillage;
        }

        //=> $id, $name, $ally, $villages, $points, $rank
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ally { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }

        public int PointsVillage => Points / Villages;
        public string AllyString => _resourceAlly.GetAllyById( Ally ).Tag;
        public long BashpointAtt => _resourceBashpoint.GetBashpointAtt( this ).Kills;
        public long BashpointDef => _resourceBashpoint.GetBashpointDef( this ).Kills;
        public long BashpointAll => _resourceBashpoint.GetBashpointAll( this ).Kills;
        public long BashpointSup => BashpointAll - ( BashpointAtt + BashpointDef );
    }
}
