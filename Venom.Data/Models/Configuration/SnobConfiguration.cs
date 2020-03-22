using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    //=> Formula gold: AG * (AG+1) / Rise Factor
    //=>    1046 * 1047 / 2 = 547582
    //=>    547582 Gold Coins needed for 1046 AG

    //=> Formula gold cost: Gold * Coin_Wood
    //=>    1046 * 28000 = 15332268000
    //=>    1046 Gold Coins cost 15.332.268.000 Wood

    //=> Basic Wood:    28.000 per Cold
    //=> Basic Stone:   30.000 per Gold 
    //=> Basic Iron:    25.000 per Gold

    [Serializable]
    [XmlType( "snob" )]
    public class SnobConfiguration
    {
        [XmlElement( "gold" )] 
        public bool Gold { get; set; } //=> Gold? true : false

        [XmlElement( "rise" )]
        public int Rise { get; set; } //=> Rise Factor

        [XmlElement( "max_dist" )]
        public int MaxDist { get; set; } //=> Limit Walking Distance in fields.

        [XmlElement( "coin_wood" )]
        public int CoinWood { get; set; } //=> Wood cost per Gold Coin

        [XmlElement( "coin_stone" )]
        public int CoinStone { get; set; } //=> Stone cost per Gold Coin

        [XmlElement( "coin_iron" )]
        public int CoinIron { get; set; } //=> Iron cost per Gold Coin

        //[XmlElement( "no_barb_conquer" )]
        //public bool NoBarbConquer { get; set; } //=> Barbarian Conquer Allowed?
    }
}
