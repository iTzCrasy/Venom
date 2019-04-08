using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venom.Game.Resources;

namespace Venom.Game
{
    public class ResourceCenter
    {
        private readonly ResourcePlayer _resourcePlayer;

        public ResourceCenter()
        {
            //_resourcePlayer = new ResourcePlayer( );
        }

        public ResourcePlayer Player => _resourcePlayer;
    }
}
