using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Venom.API.Models.Game
{
    public class AllyModel
    {
        public int World { get; set; }

        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public DateTime Date { get; set; } = DateTime.Now;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public int Members { get; set; }

        public int Villages { get; set; }

        public int Points { get; set; }

        public int AllPoints { get; set; }

        public int Rank { get; set; }
    }
}
