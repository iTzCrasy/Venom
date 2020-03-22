using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "coord" )]
    public class CoordConfiguration
    {
        [XmlElement( "map_size" )]
        public int MapSize { get; set; } //=> Min: 50, Max: 1000

        [XmlElement( "func" )]
        public byte Func { get; set; } //=> 2 normal difference betweed villages, 3 Smaller difference, 4 higher difference

        [XmlElement( "empty_villages" )]
        public byte EmptyVillages { get; set; } //=> Percentage of barbarian 

        [XmlElement( "bonus_villages" )]
        public byte BonusVillages { get; set; } //=> Percentage of barbarian 

        [XmlElement( "bonus_new" )]
        public bool BonusNew { get; set; } //=> Better Bonus villages

        [XmlElement( "inner" )]
        public int Inner { get; set; } //=> First x villages in center of the map

        [XmlElement( "select_start" )]
        public bool SelectStart { get; set; } //=> User allowed to select start with N, O, S, W axis

        [XmlElement( "village_move_wait" )]
        public int VillageMovWait { get; set; } //=> Hours to wait for new start

        [XmlElement( "noble_restart" )]
        public bool NobleRestart { get; set; } //=> User can restart after get conquered by other

        [XmlElement( "start_villages")]
        public byte StartVillages { get; set; } //=> Number of start Villages, default 1.
    }
}
