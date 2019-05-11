using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Game.Resources
{
    public enum PlanerType
    {
        Att = 0,
        Def = 1,
        Sup = 2,
        Off = 3
    }

    public class PlanerData
    {
        public int IdSender;
        public int IdTarget;
        public long TimeSend;
        public long TimeHit;
    }

    public class ResourcePlaner<PlanerType> 
    {
        private readonly Server _server;

        public ResourcePlaner( Server server )
        {
            _server = server;
        }
    }
}
