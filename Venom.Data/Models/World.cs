using System;
using System.Collections.Generic;
using System.Text;

//=> 1 World = 99 (100) Continents
//=> 1 Continent = 99 (100) Sectors
//=> 1 Sector = 24 (25) Fields
//=> 1 Field = 1 Vilage / Grass / ...
//
//=> 1000x1000 = 1.000.000 Villages

namespace Venom.Data.Models
{
    public class World
    {
    }

    public class Continent
    {
        // 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 
        // 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 
        // 20 | 21 | 22 | 23 | 24 | 25 | 26 | 27 | 28 | 29 
        // ...
        // 90 | 91 | 92 | 93 | 94 | 95 | 96 | 97 | 98 | 99 

        public int Number { get; set; } 
    }

    public class Sector
    {
        // 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 
        // 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 
        // 20 | 21 | 22 | 23 | 24 | 25 | 26 | 27 | 28 | 29 
        // ...
        // 90 | 91 | 92 | 93 | 94 | 95 | 96 | 97 | 98 | 99 

        public int Number { get; set; }
    }

    public class SectorField
    {
        // 00 | 01 | 02 | 03 | 04
        // 05 | 06 | 07 | 08 | 09
        // 10 | 11 | 12 | 13 | 14
        // 15 | 16 | 17 | 18 | 19
        // 20 | 21 | 22 | 23 | 24

        public int Number { get; set; }
    }
}
