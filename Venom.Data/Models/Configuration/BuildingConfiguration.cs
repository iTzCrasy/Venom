using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "config" )]
    public class BuildingConfiguration
    {
        [XmlElement( "main" )]
        public BuildingTypeConfiguration Main { get; set; }

        [XmlElement( "barracks" )]
        public BuildingTypeConfiguration Barracks { get; set; }

        [XmlElement( "stable" )]
        public BuildingTypeConfiguration Stable { get; set; }

        [XmlElement( "garage" )]
        public BuildingTypeConfiguration Garage { get; set; }

        [XmlElement( "church" )]
        public BuildingTypeConfiguration Church { get; set; }

        [XmlElement( "church_fr" )]
        public BuildingTypeConfiguration ChurchFr { get; set; }

        [XmlElement( "snob" )]
        public BuildingTypeConfiguration Snob { get; set; }

        [XmlElement( "smith" )]
        public BuildingTypeConfiguration Smith { get; set; }

        [XmlElement( "place" )]
        public BuildingTypeConfiguration Place { get; set; }

        [XmlElement( "statue" )]
        public BuildingTypeConfiguration Statue { get; set; }

        [XmlElement( "market" )]
        public BuildingTypeConfiguration Market { get; set; }

        [XmlElement( "wood" )]
        public BuildingTypeConfiguration Wood { get; set; }

        [XmlElement( "stone" )]
        public BuildingTypeConfiguration Stone { get; set; }

        [XmlElement( "iron" )]
        public BuildingTypeConfiguration Iron { get; set; }

        [XmlElement( "hide" )]
        public BuildingTypeConfiguration Hide { get; set; }

        [XmlElement( "wall" )]
        public BuildingTypeConfiguration Wall { get; set; }

        [XmlElement( "watchtower" )]
        public BuildingTypeConfiguration Watchtower { get; set; }
    }
}
