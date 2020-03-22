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


        //=> Extensions
        //public ICollection<PlayerNames> PlayerNames { get; set; }
        public DateTime? LastUpdate { get; set; } //=> Last time on ther server
        public DateTime? FirstUpdate { get; set; } //=> First time on the server

    }

    public class PlayerNames
    {
        public string Name { get; set; } //=> Old Name
        public DateTime Changed { get; set; } //=> Name Change Date
    }
}
