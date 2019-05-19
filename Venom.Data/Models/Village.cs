using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models
{
    public class Village
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Owner { get; set; }

        public int Points { get; set; }

        public int Bonus { get; set; }
    }
}
