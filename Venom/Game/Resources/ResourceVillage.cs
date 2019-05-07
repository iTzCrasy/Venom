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

        private readonly Dictionary<int, VillageData> _villageData = new Dictionary<int, VillageData>();
        private readonly Dictionary<Tuple<int, int>, VillageData> _villageDataByCoord = new Dictionary<Tuple<int, int>, VillageData>( );

        public ResourceVillage( Server server )
        {
            _server = server;
        }

        public async Task InitializeAsync()
        {
            var villageData = await CSVReader.DownloadFileAsync(
                new Uri( _server.Local.Url + "/map/village.txt" ),
                ( buffer ) => new VillageData()
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

        public List<VillageData> GetVillageList( ) =>
            _villageData.Values.ToList( );

        public List<VillageData> GetVillagesByPlayer( PlayerData data ) =>
            _villageData.Values.Where( x => x.Owner == data.Id ).ToList( );

        public VillageData GetVillageById( int Id ) =>
            _villageData.TryGetValue( Id, out var village ) ? village : new VillageData();

        public VillageData GetVillageByCoord( int x, int y ) =>
            _villageDataByCoord.TryGetValue( new Tuple<int, int>( x, y ), out var village ) ? village : new VillageData();

        public int GetCount() =>
            _villageData.Count();
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

        public List<string> Groups { get; set; }

        public string CoordString => X + "|" + Y;
    }
}
