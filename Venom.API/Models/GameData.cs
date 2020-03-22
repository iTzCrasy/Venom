using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Venom.API.Models
{
    public class GameData
    {
        //=> Server ID (100, 101, ..)
        public string Server { get; set; }

        public DateTime Date { get; set; }

        public string Player { get; set; }
        public string Ally { get; set; }
        public string Village { get; set; }

        //=> Server Datas
        //public Data.Models.Player Player { get; set; }
        //public Data.Models.Ally Ally { get; set; }
        //public Data.Models.Village Village { get; set; }
        //public Data.Models.Conquered Conquered { get; set; }
    }
}
