using System.ComponentModel;

namespace Venom.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string LocalUsername { get; set; }

        public TroupListViewModel TroupList { get; set; }

        public string CurrentStatusText { get; set; }
    }
}
