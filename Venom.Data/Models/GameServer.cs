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
    }
}
