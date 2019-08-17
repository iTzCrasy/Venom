using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Venom.Components.Dialogs;
using Venom.Utility;
using Venom.ViewModels;

namespace Venom
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
            //ShowSetupPlayerDialog( );
            var dialog = new AddAccount( this );
            await this.ShowMetroDialogAsync( dialog ).ConfigureAwait( false );

            void onDialogClosed( object o, DialogStateChangedEventArgs args )
            {
                DialogManager.DialogClosed -= onDialogClosed;
                //=> TODO: Handle
            }
            DialogManager.DialogClosed += onDialogClosed;



            //=> TODO: Loading Venom here!

            await Task.Delay( 1000 ).ConfigureAwait( false );

            //Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            //{
            //    this.HideMetroDialogAsync( dialog );
            //} ) );
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

        private async void UserNameButton_Click( object sender, RoutedEventArgs e )
        {
            var mySettings = new MetroDialogSettings( )
            {
            };


        }
    }
}
