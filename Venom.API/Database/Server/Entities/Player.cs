using System;
using System.ComponentModel.DataAnnotations;

namespace Venom.API.Database.Server.Entities
{
    public class Player
    {
        [Required]
        public int Server { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required, MaxLength( 64 )]
        public string Name { get; set; }

        [Required]
        public int Ally { get; set; }

        [Required]
        public int Villages { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Rank { get; set; }


        public long BashAtt { get; set; }

        public long BashDef { get; set; }

        public long BashAll { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
