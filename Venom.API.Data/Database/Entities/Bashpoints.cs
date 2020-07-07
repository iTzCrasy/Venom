using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venom.API.Data.Database.Entities
{
    public enum BashpointTypes : byte
    {
        All = 0,
        Attack = 1,
        Defense = 2
    }

    public class Bashpoints
    {
        [Required]
        public BashpointTypes Type { get; set; }

        [Required]
        public int Kills { get; set; }
    }
}
