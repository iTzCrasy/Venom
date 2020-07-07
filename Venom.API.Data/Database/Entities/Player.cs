using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venom.API.Data.Database.Entities
{
    public class Player
    {
        [Key]
        public int Key { get; set; }

        [Required]
        public int Server { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        public int Ally { get; set; }

        [Required]
        public int Villages { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Rank { get; set; }

        //public ICollection<Bashpoints> Bashpoints { get; set; }
    }
}
