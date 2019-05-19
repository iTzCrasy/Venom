using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    public class UnitTypeConfiguration
    {
        [XmlElement( "build_time" )]
        public float BuildTime { get; set; }

        [XmlElement( "pop" )]
        public int Pop { get; set; }

        [XmlElement( "speed" )]
        public double Speed { get; set; }

        [XmlElement( "attack" )]
        public int Attack { get; set; }

        [XmlElement( "defense" )]
        public int Defense { get; set; }

        [XmlElement( "defense_cavalry" )]
        public int DefenseCavalry { get; set; }

        [XmlElement( "defense_archer" )]
        public int DefenseArcher { get; set; }

        [XmlElement( "carry" )]
        public int Carry { get; set; }
    }
}
