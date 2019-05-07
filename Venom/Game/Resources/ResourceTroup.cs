using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Venom.Game.Resources
{
    public class ResourceTroup
    {
        private readonly Server _server;
        private readonly Profile _profile;
        private readonly ResourceVillage _resourceVillage;
        private readonly string[] _unitType;
        private readonly string _parserMask;
        private readonly IEnumerable<TroupType> _troupType;
        private readonly IEnumerable<TroupTable> _troupTable;

        [JsonProperty( "TroupDataOwn" )]
        private readonly Dictionary<Tuple<int, TroupType>, TroupData> _troupData = new Dictionary<Tuple<int, TroupType>, TroupData>( );

        private Dictionary<int, TroupDataVillage> _troupDataVillage = new Dictionary<int, TroupDataVillage>( );

        public ResourceTroup( 
            Server server,
            Profile profile,
            ResourceVillage resourceVillage )
        {
            _server = server;
            _profile = profile;
            _resourceVillage = resourceVillage;

            _unitType = new string[]
            {
                "eigene",
                "im Dorf",
                "auswärts",
                "unterwegs"
            };

            _parserMask = _server.Local.Config.Archer ?
                @"\d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+" :
                @"\d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+ \d+";

            _troupTable = _server.Local.Config.Archer ? 
                Enum.GetValues( typeof( TroupTable ) ).Cast<TroupTable>( ) :
                Enum.GetValues( typeof( TroupTable ) ).Cast<TroupTable>( )
                    .Except( new TroupTable[] { TroupTable.UNIT_ARCHER, TroupTable.UNIT_MARCHER } );

            _troupType = Enum.GetValues( typeof( TroupType ) ).Cast<TroupType>( );
        }

        public void Parse( string data )
        {
            //=> eigene 0 0 0 0 0 0 0 0 0 0 0 
            //=> im Dorf 0 0 0 0 0 0 0 0 0 0 0 
            //=> auswärts 0 0 0 0 0 0 0 0 0 0 0 
            //=> unterwegs 0 0 0 1 0 0 2 0 0 0 0 0 0 
            //=> 11 Troups --> 13 if archer is enabled
            //=> 51 if archer is disabled
            //=> 59 if archer is enabled

            var splittedData = data.Split( );
            if( splittedData[0].Contains( "Dorf" ) )
                return;

            var village = splittedData[0].Replace( "(", "" ).Replace( ")", "" ).Split( '|' );

            for( int i = 0; i < _unitType.Count(); i++ )
            {
                var match = Regex.Match( data, $"{_unitType[i]} {_parserMask}" );
                if( match.Success )
                {
                    var troups = match.Value.Replace( _unitType[i], $"{_troupType.ElementAt(i)}" ).Split( ' ' );
                    var trouptable = troups.AsEnumerable( ).ToList().GetRange( 1, _troupTable.Count() );
                    var villageId = _resourceVillage.GetVillageByCoord( Convert.ToInt32( village[0] ), Convert.ToInt32( village[1] ) ).Id;
                    AddUnits( villageId, _troupType.ElementAt( i ),
                        new TroupData( )
                        {
                            Troup = trouptable.Select( x => Convert.ToInt32( x ) ).ToList( ),
                            Table = _troupTable
                        } );
                }
            }
        }

        public async Task Load()
        {
            var filePath = _profile.Local.Server + "\\" + _profile.Local.Name + "\\Troup.json";
            if( File.Exists( filePath ) )
            {
                using( var sr = File.OpenText( filePath ) )
                {
                    _troupDataVillage = JsonConvert.DeserializeObject<Dictionary<int, TroupDataVillage>>( sr.ReadToEnd( ) );
                }
            }
        }

        public async Task Save()
        {
            var filePath = _profile.Local.Server + "\\" + _profile.Local.Name + "\\Troup.json";
            using( var sw = File.CreateText( filePath ) )
            {
                var js = new JsonSerializer( );
                await Task.Run( ( ) => js.Serialize( sw, _troupDataVillage ) );
            }
        }

        private void AddUnits( int villageId, TroupType type, TroupData data )
        {
            if( !_troupDataVillage.TryGetValue( villageId, out var troupDataVillage ) )
            {
                _troupDataVillage.Add( villageId, new TroupDataVillage { VillageId = villageId } );
            }

            switch( type )
            {
                case TroupType.TROUP_OWN:
                    _troupDataVillage[villageId].TroupOwn = data;
                    break;
                case TroupType.TROUP_VILLAGE:
                    _troupDataVillage[villageId].TroupVillage = data;
                    break;
                case TroupType.TROUP_OUT:
                    _troupDataVillage[villageId].TroupOut = data;
                    break;
                case TroupType.TROUP_AWAY:
                    _troupDataVillage[villageId].TroupAway = data;
                    break;
            }
        }

        public List<TroupDataVillage> GetTroupDataList() =>
            _troupDataVillage.Values.ToList( );

        public int GetTroupPop( TroupData data )
        {
            var pop = 0;
            pop += data.UnitSpear * _server.Local.ConfigUnits.GetConfig<int>( "spear", "pop" );
            pop += data.UnitSword * _server.Local.ConfigUnits.GetConfig<int>( "sword", "pop" );
            pop += data.UnitAxe * _server.Local.ConfigUnits.GetConfig<int>( "axe", "pop" );
            pop += data.UnitSpy * _server.Local.ConfigUnits.GetConfig<int>( "spy", "pop" );
            pop += data.UnitLight * _server.Local.ConfigUnits.GetConfig<int>( "light", "pop" );
            pop += data.UnitHeavy * _server.Local.ConfigUnits.GetConfig<int>( "heavy", "pop" );
            pop += data.UnitRam * _server.Local.ConfigUnits.GetConfig<int>( "ram", "pop" );
            pop += data.UnitCatapult * _server.Local.ConfigUnits.GetConfig<int>( "catapult", "pop" );
            pop += data.UnitKnight * _server.Local.ConfigUnits.GetConfig<int>( "knight", "pop" );
            pop += data.UnitSnob * _server.Local.ConfigUnits.GetConfig<int>( "snob", "pop" );
            return pop;
        }
    }

    public class TroupDataVillage
    {
        public int VillageId;
        [JsonProperty( "Troup1" )]
        public TroupData TroupOwn = new TroupData();
        [JsonProperty( "Troup2" )]
        public TroupData TroupVillage = new TroupData( );
        [JsonProperty( "Troup3" )]
        public TroupData TroupOut = new TroupData( );
        [JsonProperty( "Troup4" )]
        public TroupData TroupAway = new TroupData( );
    }

    public class TroupData
    {
        public TroupType Type;

        [JsonProperty( "Troup" )]
        public List<int> Troup = new List<int>( );

        public IEnumerable<TroupTable> Table = Enum.GetValues( typeof( TroupTable ) ).Cast<TroupTable>( );

        private int GetTroup( TroupTable troup )
        {
            var index = 0;
            var comparer = EqualityComparer<TroupTable>.Default;
            foreach( var item in Table )
            {
                if( comparer.Equals( item, troup ) )
                    return Troup.ElementAtOrDefault( index );
                index++;
            }
            return 0;
        }

        public int UnitSpear => GetTroup( TroupTable.UNIT_SPEAR );
        public int UnitSword => GetTroup( TroupTable.UNIT_SWORD );
        public int UnitAxe => GetTroup( TroupTable.UNIT_AXE );
        public int UnitArcher => GetTroup( TroupTable.UNIT_ARCHER );
        public int UnitSpy => GetTroup( TroupTable.UNIT_SPY );
        public int UnitLight => GetTroup( TroupTable.UNIT_LIGHT );
        public int UnitMarcher => GetTroup( TroupTable.UNIT_MARCHER );
        public int UnitHeavy => GetTroup( TroupTable.UNIT_HEAVY );
        public int UnitRam => GetTroup( TroupTable.UNIT_RAM );
        public int UnitCatapult => GetTroup( TroupTable.UNIT_CATAPULT );
        public int UnitKnight => GetTroup( TroupTable.UNIT_KNIGHT );
        public int UnitSnob => GetTroup( TroupTable.UNIT_SNOB );
    }

    public enum TroupTable : int
    {
        UNIT_SPEAR = 0,
        UNIT_SWORD, 
        UNIT_AXE,
        UNIT_ARCHER, 
        UNIT_SPY, 
        UNIT_LIGHT,  
        UNIT_MARCHER,   
        UNIT_HEAVY,
        UNIT_RAM,
        UNIT_CATAPULT,
        UNIT_KNIGHT,
        UNIT_SNOB
    }

    public enum TroupType : int
    {
        TROUP_OWN = 0,  //=> Eigene
        TROUP_VILLAGE,  //=> Im Dorf
        TROUP_OUT,      //=> Auswärts
        TROUP_AWAY      //=> Unterwegs
    }
}
