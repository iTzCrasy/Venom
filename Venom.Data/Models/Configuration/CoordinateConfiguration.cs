using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "coord" )]
    public class CoordinateConfiguration
    {
        [XmlElement( "map_size" )]
        public int MapSize { get; set; }

        [XmlElement( "func" )]
        public int Func { get; set; }

        [XmlElement( "empty_villages" )]
        public int EmptyVillages { get; set; }

        [XmlElement( "bonus_villages" )]
        public int BonusVillages { get; set; }

        [XmlElement( "bonus_new" )]
        public int BonusNew { get; set; }

        [XmlElement( "inner" )]
        public int Inner { get; set; }

        [XmlElement( "select_start" )]
        public int SelectStart { get; set; }

        [XmlElement( "village_move_wait" )]
        public int VillageMoveWait { get; set; }

        [XmlElement( "noble_restart" )]
        public int NobleRestart { get; set; }

        [XmlElement( "start_villages" )]
        public int StartVillages { get; set; }
    }
}
