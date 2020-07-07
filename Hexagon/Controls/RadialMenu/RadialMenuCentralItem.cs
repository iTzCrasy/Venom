using System.Windows;
using System.Windows.Controls;

namespace Hexagon.Controls
{
    public class RadialMenuCentralItem : Button
    {
        static RadialMenuCentralItem( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( RadialMenuCentralItem ), new FrameworkPropertyMetadata( typeof( RadialMenuCentralItem ) ) );
        }
    }
}
