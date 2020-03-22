using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Venom.Data.Models
{
    [Serializable]
    [DebuggerDisplay( "{Id} : {Url}" )]
    public class GameServer
    {
        public string Id { get; set; }

        public Uri Url { get; set; }

        public int PlayerCount { get; set; }

        public int AllyCount { get; set; }

        public int VillageCount { get; set; }

        public int VillageBarbCount { get; set; }
    }
}
