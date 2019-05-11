using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels
{
    public class LoadingWindowViewModel : NotifyPropertyChangedExt
    {
        private string _currentText = "";

        public LoadingWindowViewModel()
        {

        }

        public string CurrentText
        {
            get => _currentText;
            set => SetProperty( ref _currentText, value );
        }
    }
}
