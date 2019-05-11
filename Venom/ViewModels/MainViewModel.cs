using System.ComponentModel;

namespace Venom.ViewModels
{
    public class MainMenuItem
    {
        public string Group { get; set; }

        public string Title { get; set; }

        public object Content { get; set; }

        public string Image { get; set; }

        public string ToolTip { get; set; }
    }


    public class MainViewModel : NotifyPropertyChangedExt
    {
        public ICollectionView MenuCollection { get; set; }

        public string LocalUsername { get; set; }
    }
}
