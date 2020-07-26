using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Venom.Components.Windows;
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
            //=> Load Settings
            Venom.Properties.Settings.Default.Reload( );

            //=> Set Main Window
            Current.MainWindow = new Components.Windows.MainWindow( );
            Current.MainWindow.Show( );
        }
    }
}
