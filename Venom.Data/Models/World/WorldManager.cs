using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models.World
{
    public class WorldManager
    {
        private readonly Dictionary<int, Continent> _continentList = new Dictionary<int, Continent>();

        public Continent GetContinentByCoord( int x, int y )
        {
            decimal fx = x / 100;
            decimal fy = y / 100;

            Continent output;
            return _continentList.TryGetValue( ( int )( Math.Floor( fy ) + Math.Floor( fx ) ), out output ) ? output : null;
        }
    }
}
