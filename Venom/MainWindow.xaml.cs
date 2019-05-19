using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Venom.Dialogs;
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
            ShowSetupPlayerDialog( );
        }


        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            ;
        }




        public void ShowSetupPlayerDialog( )
        {
            var dialog = new SetupPlayer( this, new SetupPlayerDialogSettings( ) );

            DialogManager.ShowMetroDialogAsync( this, dialog, null );
        }

        private void UserNameButton_Click( object sender, RoutedEventArgs e )
        {
            settingsFlyout.IsOpen = !settingsFlyout.IsOpen;
        }



    }
}
