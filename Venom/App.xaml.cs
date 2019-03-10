using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Venom
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            Core.Game.GetInstance.LoadServerList( );

            var Win = new Windows.StartWindow
            {
                DataContext = new Windows.ViewModels.StartViewSelectServerModel( )
            };
            Win.Show( );
        }
    }
}
