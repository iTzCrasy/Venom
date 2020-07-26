using System;
using System.Collections.Generic;
using System.Text;

//=> 1 World = 99 (100) Continents
//=> 1 Continent = 99 (100) Sectors
//=> 1 Sector = 24 (25) Fields
//=> 1 Field = 1 Vilage / Grass / ...
//
//=> 1000x1000 = 1.000.000 Villages/Images

/*
var sector = Math.floor((y % 100) / 5) * 20 + Math.floor((x % 100) / 5);
var field = Math.floor( ( ( y % 100 ) % 5 ) * 5 + ( ( x % 100 ) % 5 ) );
field = (field< 10) ? '0' + field : field;
sector = ((sector< 100) && (sector > 9)) ? '0' + sector : ((sector< 10) ? '00' + sector : sector);
    */

namespace Venom.Data.Models
{
    public class World2
    {
        protected byte[] _decoration = new byte[1000000];

        public void Initialize()
        {
            throw new NotImplementedException( "World.Initialize" );
        }

        private bool LoadDecoration()
        {
            throw new NotImplementedException( "World.LoadDecoration" );
        }
    }

    public class Continent
    {
        // 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 
        // 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 
        // 20 | 21 | 22 | 23 | 24 | 25 | 26 | 27 | 28 | 29 
        // ...
        // 90 | 91 | 92 | 93 | 94 | 95 | 96 | 97 | 98 | 99 

        public Dictionary<int, Sector> _sectors;

        public int Number { get; set; } 
    }

    public class Sector
    {
        // 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 
        // 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 
        // 20 | 21 | 22 | 23 | 24 | 25 | 26 | 27 | 28 | 29 
        // ...
        // 90 | 91 | 92 | 93 | 94 | 95 | 96 | 97 | 98 | 99 

        public Dictionary<int, SectorField> _sectorFields;

        public int Number { get; set; }
    }

    public class SectorField
    {
        // 00 | 01 | 02 | 03 | 04
        // 05 | 06 | 07 | 08 | 09
        // 10 | 11 | 12 | 13 | 14
        // 15 | 16 | 17 | 18 | 19
        // 20 | 21 | 22 | 23 | 24

        //=> TODO: Villages / Background.

        public int Number { get; set; }
    }
}
