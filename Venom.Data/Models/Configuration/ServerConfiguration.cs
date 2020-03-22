using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlRoot( "config" )]
    public class ServerConfiguration
    {
        [XmlElement( "speed" )]
        public float Speed { get; set; }

        [XmlElement( "unit_speed" )]
        public float UnitSpeed { get; set; }

        [XmlElement( "moral" )]
        public float Moral { get; set; }

        [XmlElement( "game" )]
        public GameConfiguration Game { get; set; }

        [XmlElement( "coord" )]
        public CoordinateConfiguration Coord { get; set; }

        [XmlElement( "snob" )]
        public SnobConfiguration Snob { get; set; }

    }
}
