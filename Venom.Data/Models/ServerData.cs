using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models
{
    public class ServerData
    {
        public int Id { get; set; }

        public int World { get; set; }

        public string Url { get; set; }

        public int PlayerCount { get; set; } = 0;
        public int AllyCount { get; set; } = 0;
        public int VillageCount { get; set; } = 0;
    }
}
