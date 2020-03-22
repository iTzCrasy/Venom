using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    //=> Formula Build Time: ( build_time * build_time_factor * build_level ) / speed
    //=>    Basic 161 main building from 20 to 21:
    //=>        562.5 * 1.2 * 21 = 14175 Seconds
    //=>        ( 562.5 * 1.2 * 21 ) / 2.0 = 7087.5 Seconds = 02:32:09
    //=> Formula Cost: cost * cost_factor 
    //=>    Basic 161 main building from 1 to 2:
    //=>        90 * 1.26 = 113.4 (113)
    //=>    Basic 161 main building from 2 to 3:
    //=>        113.4 * 1.26 = 142.8 (143)

    [Serializable]
    public class BuildingTypeConfiguration
    {
        [XmlElement( "max_level" )]
        public int MaxLevel { get; set; }

        [XmlElement( "min_level" )]
        public int MinLevel { get; set; }

        [XmlElement( "wood" )]
        public int CostWood { get; set; }

        [XmlElement( "stone" )]
        public int CostStone { get; set; }

        [XmlElement( "iron" )]
        public int CostIron { get; set; }

        [XmlElement( "pop" )]
        public int CostPop { get; set; }

        [XmlElement( "wood_factor" )]
        public float FactorWood { get; set; }

        [XmlElement( "stone_factor" )]
        public float FactorStone { get; set; }

        [XmlElement( "iron_factor" )]
        public float FactorIron { get; set; }

        [XmlElement( "pop_factor" )]
        public float FactorPop { get; set; }

        [XmlElement( "build_time" )]
        public float BuildTime { get; set; }

        [XmlElement( "build_time_factor" ) ]
        public float BuildTimeFactor { get; set; }


        
    }
}
