using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Resources
{
    
    public class AccountEntry
    {

    }


    public class UserSettings
    {
        public bool AutoRefresh { get; set; }

        public IEnumerable<AccountEntry> AccountList { get; set; }
    }
}
