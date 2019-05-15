using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels
{
    public class TroupListViewModel : ViewModelBase
    {
        public IEnumerable<string> BrushResources { get; private set; }


        public TroupListViewModel( )
        {
            BrushResources = new List<string> {
                "asdf", "AAAA", "Test1234", "Test2222"
            };
        }

    }
}
