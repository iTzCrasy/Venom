using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models.World
{
    public class WorldBase
    {
        private int _number = 0;

        public int Number
        {
            get => _number;
            set => _number = value;
        }
    }
}
