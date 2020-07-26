using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Venom.API.Database.Server.Entities;

namespace Venom.API.Database.Global.Entities
{
    public class ServerData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int World { get; set; }

        [Required, MaxLength( 32 )]
        public string Url { get; set; }

        [Required]
        public bool IsValid { get; set; } = true;

        [Required]
        public DateTimeOffset Register { get; set; } = DateTimeOffset.Now;

        public long? Size { get; set; } = 0;

        public int PlayerCount { get; set; } = 0;
        public int AllyCount { get; set; } = 0;
        public int VillageCount { get; set; } = 0;
    }
}
