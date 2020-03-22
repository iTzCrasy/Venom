using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "game" )]
    public class GameConfiguration
    {
        [XmlElement( "buildtime_formula" )]
        public byte BuildTime_Formula { get; set; }

        [XmlElement( "knight" )]
        public byte Knight { get; set; }

        [XmlElement( "knight_new_items" )]
        public string KnightNewItems { get; set; }

        [XmlElement( "archer" )]
        public bool Archer { get; set; }

        [XmlElement( "tech" )]
        public byte Tech { get; set; }

        [XmlElement( "farm_limit" )]
        public int Farm_Limit { get; set; }

        [XmlElement( "church" )]
        public bool Church { get; set; }

        [XmlElement( "watchtower" )]
        public bool Watchtower { get; set; }

        //[XmlElement( "stronghold" )]
        //public bool Stronghold { get; set; }

        [XmlElement( "fake_limit" )]
        public int Fake_Limit { get; set; }

        [XmlElement( "barbarian_rise" )]
        public float Barbarian_Rise { get; set; }

        [XmlElement( "barbarian_shrink" )]
        public string Barbarian_Shrink { get; set; }

        [XmlElement( "barbarian_max_points" )]
        public int Barbarian_Max_Points { get; set; }

        [XmlElement( "hauls" )]
        public int Hauls { get; set; }

        [XmlElement( "hauls_base" )]
        public int Hauls_Base { get; set; }

        [XmlElement( "hauls_max" )]
        public int Hauls_Max { get; set; }

        [XmlElement( "base_production" )]
        public int Base_Production { get; set; }

        [XmlElement( "event" )]
        public int Event { get; set; }
    }
}
