using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Game.Resources
{
    public class ResourceGroup : IResource
    {
        public ResourceGroup()
        {

        }

        public async Task InitializeAsync( )
        {
            //=> TODO: Load Groups from File!
        }

        public void ParseGroups( string data )
        {

        }
    }

    public class GroupData
    {
        public int Village { get; set; }
        public int Count { get; set; }
        public List<string> Groups { get; set; }
    }
}
