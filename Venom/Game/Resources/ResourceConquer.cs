using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourceConquer : IResource
    {
        private readonly Server _server;
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceVillage _resourceVillage;

        private readonly List<ConquerData> _conquerData = new List<ConquerData>( );

        public ResourceConquer( 
            Server server,
            ResourcePlayer resourcePlayer,
            ResourceVillage resourceVillage )
        {
            _server = server;
            _resourcePlayer = resourcePlayer;
            _resourceVillage = resourceVillage;
        }

        public async Task InitializeAsync()
        {
            var conquerData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/conquer.txt" ),
                ( buffer ) => new ConquerData( _resourcePlayer, _resourceVillage )
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

        public List<ConquerData> GetConquerByPlayer( PlayerData data ) =>
            _conquerData.Where( x => x.NewOwner == data.Id || x.OldOwner == data.Id ).ToList();

        public List<ConquerData> GetConquerList( ) =>
            _conquerData;

        public int GetCount( ) =>
            _conquerData.Count( );
    }

    public class ConquerData
    {
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceVillage _resourceVillage;
        public ConquerData( 
            ResourcePlayer resourcePlayer,
            ResourceVillage resourceVillage )
        {
            _resourcePlayer = resourcePlayer;
            _resourceVillage = resourceVillage;
        }

        //=> $village_id, $unix_timestamp, $new_owner, $old_owner
        public int Id { get; set; }
        public int Time { get; set; }
        public int NewOwner { get; set; }
        public int OldOwner { get; set; }

        public string Name => _resourceVillage.GetVillageById( Id ).Name;
        public int Points => _resourceVillage.GetVillageById( Id ).Points;
        public string NewOwnerString => _resourcePlayer.GetPlayerById( NewOwner ).Name;
        public string OldOwnerString => _resourcePlayer.GetPlayerById( OldOwner ).Name;
        public string TimeString => new DateTime( 1970, 1, 1, 0, 0, 0, 0 ).AddSeconds( Time ).ToString( "G", CultureInfo.CreateSpecificCulture( "de-DE" ) );
    }
}

