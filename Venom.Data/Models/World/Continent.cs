using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models.World
{
    public class Continent : WorldBase
    {
        private Dictionary<int, Sector> _sectorList;

        public Sector GetSectorByCoord( int x, int y )
        {
            decimal fx = x % 100 / 5;
            decimal fy = y % 100 / 5;

            Sector output;
            return _sectorList.TryGetValue( ( int )( Math.Floor( fy ) + Math.Floor( fx ) * 20 ), out output ) ? output : null;
        }
    }
}
