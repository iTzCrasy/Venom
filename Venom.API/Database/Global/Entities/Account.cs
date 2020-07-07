using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venom.API.Database.Global.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength( 64 )]
        public string Username { get; set; }

        [Required, MaxLength( 64 )]
        public string Password { get; set; }

        [Required, MaxLength( 64 )]
        public string Mail { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public DateTime LicenseDate { get; set; }

        [Required]
        public bool Blocked { get; set; } = false;

        [Required]
        public string LicenseKey { get; set; }
    }
}
