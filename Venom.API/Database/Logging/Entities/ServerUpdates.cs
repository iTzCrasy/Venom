using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Venom.API.Database.Logging.Entities
{
    public class ServerUpdates
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Server { get; set; }

        [Required]
        public DateTimeOffset UpdateTime { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }
    }
}
