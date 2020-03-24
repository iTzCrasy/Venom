using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Venom.Utility;
using Venom.Helpers;
using Venom.Repositories;
using Venom.Data.Models;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Shell;

namespace Venom.Components.Windows.Main
{
    public partial class MainWindow
    {
		
        public MainWindow( )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            DataContext = ContainerHelper.Provider.GetRequiredService<MainViewModel>( );

            InitializeComponent( );
        }

        private async void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
                
        }
    

        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            ;
        }

        private void ToggleFlyout( int index )
        {
            if( !( Flyouts.Items[index] is MahApps.Metro.Controls.Flyout flyout ) )
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void SettingsButton_Click( object sender, RoutedEventArgs e )
        {
            ToggleFlyout( 0 );
        }

        private async void UserNameButton_Click( object sender, RoutedEventArgs e )
        {
            var mySettings = new MetroDialogSettings( )
            {
            };
        }
    }
}
