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

        private Task ShowPopup<TPopup>( TPopup popup ) where TPopup : Window
        {
            var task = new TaskCompletionSource<object>( );
            popup.Owner = Current.MainWindow;
            popup.Closed += ( s, a ) => task.SetResult( null );
            popup.Show( );
            popup.Focus( );
            return task.Task;
        }

        private async void OnStartup( object sender, StartupEventArgs e )
        {
            //=> Load Settings
            Venom.Properties.Settings.Default.Reload( );

            //=> Set Main Window
            Current.MainWindow = new Components.Windows.Main.MainWindow( );
            ContainerHelper.Provider.GetRequiredService<Components.Windows.Start.StartWindow>( ).Show( );
            await Task.Delay( 3000 ).ConfigureAwait( true );
            Current.MainWindow.Show( );
            ContainerHelper.Provider.GetRequiredService<Components.Windows.Start.StartWindow>( ).Hide( );

            //Current.MainWindow.Show( );

            //var windowStart = new Components.Windows.StartWindow( );
            //windowStart.Show( );

            //await Task.Delay( 3000 ).ConfigureAwait( true );

            //Application.Current.Dispatcher.Invoke( ( Action )delegate
            //{
            //    var windowMain = new MainWindow( );
            //    windowMain.Show( );
            //} );
        }
    }
}
public static class AppEx
{
    public static T FindWindowOfType<T>( this Application app ) where T : Window
    {
        return app.Windows.OfType<T>( ).FirstOrDefault( );
    }
}
