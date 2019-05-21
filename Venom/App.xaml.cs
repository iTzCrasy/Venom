using System.Windows;
using Venom.Utility;

namespace Venom
{
    public partial class App : Application
    {
        public App( )
        {
            ContainerHelper.PrepareContainer( );
        }
    }
}
