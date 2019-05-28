using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Venom.Components.Dialogs;
using Venom.ViewModels;

namespace Venom
{
    public partial class MainWindow
    {
		
        public MainWindow( )
        {
            InitializeComponent( );
        }




        private void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
            //ShowSetupPlayerDialog( );
        }


        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            ;
        }




        public void ShowSetupPlayerDialog( )
        {
            var dialog = new AddEditAccount( this );
            DialogManager.ShowMetroDialogAsync( this, dialog );
        }

        private void ToggleFlyout( int index )
        {
            if( !( Flyouts.Items[index] is Flyout flyout ) )
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void SettingsButton_Click( object sender, RoutedEventArgs e )
        {
            ToggleFlyout( 0 );
        }
    }
}
