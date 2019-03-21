using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    public class ResourceVillage : IResource
    {
        private readonly Server _server;
        private readonly ResourceTroup _resourceTroup;

        private readonly Dictionary<int, VillageData> _villageData = new Dictionary<int, VillageData>();
        private readonly Dictionary<Tuple<int, int>, VillageData> _villageDataByCoord = new Dictionary<Tuple<int, int>, VillageData>( );

        public ResourceVillage( 
            Server server,
            ResourceTroup resourceTroup )
        {
            _server = server;
            _resourceTroup = resourceTroup;
        }

        public async Task InitializeAsync()
        {
            var villageData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/village.txt" ),
                ( buffer ) => new VillageData( _resourceTroup )
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    X = buffer.ReadInt( ),
                    Y = buffer.ReadInt( ),
                    Owner = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Bonus = buffer.ReadInt( ),
                } );

            foreach( var i in villageData )
            {
                AddVillage( i );
            }
        }

        private void AddVillage( VillageData data )
        {
            _villageData.Add( data.Id, data );
            _villageDataByCoord.Add( new Tuple<int, int>( data.X, data.Y ), data );
        }

        public IEnumerable<VillageData> GetVillagesByPlayer( PlayerData data ) =>
            _villageData.Values.ToList( ).Where( x => x.Owner == data.Id );

    }

    public class VillageData
    {
        /// <summary>
        /// Constructor, Injection
        /// </summary>
        private readonly ResourceTroup _resourceTroup;
        public VillageData( ResourceTroup resourceVillage ) => _resourceTroup = resourceVillage;
        //=> $id, $name, $x, $y, $player, $points, $bonus
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Owner { get; set; }
        public int Points { get; set; }
        public int Bonus { get; set; }

        public string CoordString => X + "|" + Y;
        public TroupData Troup => _resourceTroup.GetTroupByVillage( this );
    }
}
