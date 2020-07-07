using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Venom.Helpers;

namespace Venom.Components.Views
{
    public class WorldViewModel : ViewModelBase
    {
        public List<object> _listWorld = new List<object>( );


        public List<object> TestList 
        {
            get => _listWorld;
            set
            {

            }
        }

        public async Task Loaded( )
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 9,
                Height = 9,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            _listWorld.Add( ellipse );
        }
    }
}
