using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Ally { get; set; }

        public int Villages { get; set; }

        public int Points { get; set; }

        public int Rank { get; set; }

    }
}
