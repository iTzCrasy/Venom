using System;
using System.ComponentModel.DataAnnotations;

namespace Venom.API.Database.Server.Entities
{
    public class Village
    {
        [Required]
        public int Server { get; set; }

        [Required]
        public int VillageId { get; set; }

        [Required, MaxLength( 64 )]
        public string Name { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [Required]
        public int Owner { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Bonus { get; set; }
    }
}
