using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "config" )]
    public class UnitConfiguration
    {
        [XmlElement( "spear" )]
        public UnitTypeConfiguration Spear { get; set; }

        [XmlElement( "sword" )]
        public UnitTypeConfiguration Sword { get; set; }

        [XmlElement( "axe" )]
        public UnitTypeConfiguration Axe { get; set; }

        [XmlElement( "spy" )]
        public UnitTypeConfiguration Spy { get; set; }

        [XmlElement( "light" )]
        public UnitTypeConfiguration Light { get; set; }

        [XmlElement( "heavy" )]
        public UnitTypeConfiguration Heavy { get; set; }

        [XmlElement( "ram" )]
        public UnitTypeConfiguration Ram { get; set; }

        [XmlElement( "catapult" )]
        public UnitTypeConfiguration Catapult { get; set; }

        [XmlElement( "knight" )]
        public UnitTypeConfiguration Knight { get; set; }

        [XmlElement( "snob" )]
        public UnitTypeConfiguration Snob { get; set; }

        [XmlElement( "militia" )]
        public UnitTypeConfiguration Militia { get; set; }
    }
}
