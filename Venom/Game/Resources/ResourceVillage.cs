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
        private readonly Dictionary<int, VillageData> _villageData = new Dictionary<int, VillageData>();
        private readonly Dictionary<Tuple<int, int>, VillageData> _villageDataByCoord = new Dictionary<Tuple<int, int>, VillageData>( );

        public ResourceVillage()
        {

        }

        public async Task InitializeAsync( ServerInfo server )
        {
            var villageData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/village.txt" ),
                ( buffer ) => new VillageData
                {
                    Id = buffer.ReadInt( ),
                    Name = buffer.ReadString( ),
                    X = buffer.ReadInt( ),
                    Y = buffer.ReadInt( ),
                    Owner = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Bonus = buffer.ReadInt( )
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
    }

    public class VillageData
    {
        //=> $id, $name, $x, $y, $player, $points, $bonus
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Owner { get; set; }
        public int Points { get; set; }
        public int Bonus { get; set; }
    }
}
