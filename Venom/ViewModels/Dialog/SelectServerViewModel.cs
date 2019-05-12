using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels.Dialog
{
    public class SelectServerItemViewModel
    {
        public string AccountName { get; set; }

        public string ServerName { get; set; }

        public DateTime LastUsed { get; set; }

        public bool IsActive { get; set; }
    }

    public class SelectServerViewModel
    {
        public SelectServerItemViewModel[] ServerItems { get; set; }
    }
}
