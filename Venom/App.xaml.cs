using System;
using System.Threading.Tasks;
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

        private async void OnStartup( object sender, StartupEventArgs e )
        {
            //=> TODO: Handle Full Startup here!
            //var windowStart = new Components.Windows.StartWindow( );
            //windowStart.Show( );

            var windowMain = new MainWindow( );
            windowMain.Show( );
        }
    }
}
