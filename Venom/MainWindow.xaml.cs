using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Venom.Dialogs;
using Venom.ViewModels;

namespace Venom
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow( )
        {
            InitializeComponent( );

            DataContext = CreateDataContext( );
        }

        private object CreateDataContext( )
        {
            return new MainViewModel
            {
                LocalUsername = "asdasdsad",

                TroupList = new TroupListViewModel( ),
            };
        }



        private void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
            //ResourceManager.GetInstance.LoadResources( ( status ) =>
            //{

            //} )
            //.ContinueWith( ( task ) =>
            //{
            //    Dispatcher.BeginInvoke( new Action( ( ) =>
            //    {
            //        lblBackgroundTasks.Text = "done in...";
            //    } ) );
            //} );
        }


        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            ;
        }




        public void ShowSetupPlayerDialog( )
        {
            var dialog = new SetupPlayer( this );

            DialogManager.ShowMetroDialogAsync( this, dialog, null );
        }
    }
}
