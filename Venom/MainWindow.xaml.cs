using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Interop;
using System.ComponentModel;
using Venom.ViewModels;
using Venom.Util;
using Venom.Views;
using Venom.Views.First;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Venom.Dialogs;
using Venom.Core;
using System.Windows.Threading;

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
            ResourceManager.GetInstance.LoadResources( ( status ) =>
            {

            } )
            .ContinueWith( ( task ) =>
            {
                Dispatcher.BeginInvoke( new Action( ( ) =>
                {
                    lblBackgroundTasks.Text = "done in...";
                } ) );
            } );
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
