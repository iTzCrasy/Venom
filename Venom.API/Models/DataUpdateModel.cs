using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Venom.API.Models
{
    public class DataUpdateModel
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime NextPlayer { get; set; } = DateTime.Now;

        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime NextAlly { get; set; } = DateTime.Now;

        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime NextVillages { get; set; } = DateTime.Now;
    }
}
