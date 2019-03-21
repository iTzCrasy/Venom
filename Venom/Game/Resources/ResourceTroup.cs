using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Venom.Game.Resources
{
    public class ResourceTroup : IResource
    {
        private readonly Profile _profile;

        private readonly Dictionary<int, TroupData> _troupData = new Dictionary<int, TroupData>( );

        public ResourceTroup( 
            Profile profile )
        {
            _profile = profile;
        }

        public async Task InitializeAsync()
        {
            //=> TODO: Load Troups from File!
        }

        public async Task Save()
        {
            var filePath = _profile.Local.Server + "\\" + _profile.Local.Name + "\\Troup.json";
            using( var sw = File.CreateText( filePath ) )
            {
                var js = new JsonSerializer( );
                await Task.Run( ( ) => js.Serialize( sw, _troupData ) );
            }
        }

        public TroupData GetTroupByVillage( VillageData data ) =>
            _troupData.TryGetValue( data.Id, out var troup ) ? troup : new TroupData( );
    }

    public class TroupData
    {
        [JsonProperty( "Id" )]
        public int Id { get; set; }

        [JsonProperty( "Spear" )]
        public int UnitSpear { get; set; }
        [JsonProperty( "Sword" )]
        public int UnitSword { get; set; }
        [JsonProperty( "Axe" )]
        public int UnitAxe { get; set; }
        [JsonProperty( "Archer" )]
        public int UnitArcher { get; set; }
        [JsonProperty( "Spy" )]
        public int UnitSpy { get; set; }
        [JsonProperty( "Light" )]
        public int UnitLight { get; set; }
        [JsonProperty( "Marcher" )]
        public int UnitMarcher { get; set; }
        [JsonProperty( "Heavy" )]
        public int UnitHeavy { get; set; }
        [JsonProperty( "Ram" )]
        public int UnitRam { get; set; }
        [JsonProperty( "Catapult" )]
        public int UnitCatapult { get; set; }
        [JsonProperty( "Knight" )]
        public int UnitKnight { get; set; }
        [JsonProperty( "Snob" )]
        public int UnitSnob { get; set; }

    }
}
