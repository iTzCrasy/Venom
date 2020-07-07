using System;
using System.ComponentModel.DataAnnotations;

namespace Venom.API.Database.Server.Entities
{
    public class Ally
    {
        [Required]
        public int Server { get; set; }

        [Required]
        public int AllyId { get; set; }

        [Required, MaxLength( 64 )]
        public string Name { get; set; }

        [Required]
        public string Tag { get; set; }

        [Required]
        public int Member { get; set; }

        [Required]
        public int Village { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int AllPoints { get; set; }

        [Required]
        public int Rank { get; set; }


        public long BashAtt { get; set; }

        public long BashDef { get; set; }

        public long BashAll { get; set; }
    }
}
