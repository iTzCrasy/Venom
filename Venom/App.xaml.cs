using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Venom.Core;
using Venom.Windows;
using Venom.Windows.ViewModels;

namespace Venom
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
		private void AppStartup( object sender, StartupEventArgs e )
		{
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Global.Initialize( ); //=> Setup Global 
            Global.Start( ); //=> Start Venom

			ResourceManager.GetInstance.Initialize( );

			Game.GetInstance.LoadServerList( );

            //var startWindow = new StartWindow
            //{
            //    DataContext = new StartViewSelectServerModel( )
            //};
			
            //startWindow.Show( );
        }
    }
}
