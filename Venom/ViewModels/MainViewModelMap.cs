using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Venom.ViewModels
{
    public class MainViewModelMap : NotifyPropertyChangedExt
    {
        public MainViewModelMap()
        {
            //MapFocusX = -2600;
            //MapFocusY = -2200;
        }

        public double MapFocusX
        {
            get => _MapFocusX;
            set => SetProperty( ref _MapFocusX, value );
        }
        public double MapFocusY
        {
            get => _MapFocusY;
            set => SetProperty( ref _MapFocusY, value );
        }

        protected double _MapFocusX;
        protected double _MapFocusY;
        protected UIElementCollection _MapChilds;
    }
}
