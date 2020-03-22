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
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using Windows.UI.Xaml.Controls;

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


            //=> TextServerTime
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
            TaskbarItemInfo.ProgressValue = 1.0;
            //=> TextServerPing
        }

        private void MyWindowsXAMLHost_ChildChanged( object sender, EventArgs e )
        {
            WindowsXamlHost windowsXamlHost = ( WindowsXamlHost )sender;

            var nav = ( Windows.UI.Xaml.Controls.NavigationView )windowsXamlHost.Child;

            nav.PaneDisplayMode = NavigationViewPaneDisplayMode.Top;

            var item = new NavigationViewItem( );
            item.Content = "Test 1";
            item.Tag = "t";
            nav.MenuItems.Add( item );

            var item0 = new NavigationViewItem( );
            item0.Content = "Test 2";
            item0.Tag = "t";
            nav.MenuItems.Add( item0 );

            var item1 = new NavigationViewItem( );
            item1.Content = "Test 3";
            item1.Tag = "t";
            nav.MenuItems.Add( item1 );

            var item2 = new NavigationViewItem( );
            item2.Content = "Test 4";
            item2.Tag = "t";
            nav.MenuItems.Add( item2 );

            var item3 = new NavigationViewItem( );
            item3.Content = "Test 5";
            item3.Tag = "t";
            nav.MenuItems.Add( item3 );

            //Windows.UI.Xaml.Controls.Button button =
            //    ( Windows.UI.Xaml.Controls.Button )windowsXamlHost.Child;

            //button.Content = "My UWP button";
        }

        //private void Button_Click( object sender, Windows.UI.Xaml.RoutedEventArgs e )
        //{
        //    MessageBox.Show( "My UWP button works" );
        //}

        private async void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
            //Process.Start( "cmd", "/C start" + " " + "http://google.de/" );

            //var dialog = new AddAccount( this );

            //await this.ShowMetroDialogAsync( dialog ).ConfigureAwait( true );

            //void onDialogClosed( object o, DialogStateChangedEventArgs args )
            //{
            //    DialogManager.DialogClosed -= onDialogClosed;


            //    //=> TODO: Loading Venom!
            //}
            //DialogManager.DialogClosed += onDialogClosed;

            //await Task.Delay( 1000 ).ConfigureAwait( false );

            //Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            //{
            //    this.HideMetroDialogAsync( dialog );
            //} ) );

            //for( var X = 0; X !< 1000; X += 10 )
            //{
            //    for( var Y = 0; Y < 1000; Y += 10 )
            //    {
            //        var Continent = new System.Windows.Controls.Canvas
            //        {
            //            Width = 250,
            //            Height = 250
            //        };

            //        var TestImage = new System.Windows.Controls.Image
            //        {
            //            Source = new BitmapImage( new Uri( "https://dsde.innogamescdn.com/asset/75f2721/graphic//map//version2/v5.png" ) ),
            //            //Source = new BitmapImage( new Uri( "https://de173.die-staemme.de/page.php?page=topo_image&player_id=undefined&village_id=null&x=" + X + "&y=" + Y + "&church=3&political=0&war=0&watchtower=0&key=1148549741&cur=null&focus=1992&local_cache=0" ) ),
            //            Width = 53,
            //            Height = 38
            //        };

            //        Continent.SetValue( System.Windows.Controls.Canvas.TopProperty, Y * 53 + 0.0 );    //=> Position Y
            //        Continent.SetValue( System.Windows.Controls.Canvas.LeftProperty, X * 38 + 0.0 );   //=> Position X
            //        Continent.SetValue( System.Windows.Controls.Canvas.BottomProperty, 0.0 );
            //        Continent.SetValue( System.Windows.Controls.Canvas.RightProperty, 0.0 );

            //        Continent.Children.Add( TestImage );
            //        Continent.Background = Brushes.Red;

            //        var myBorder = new System.Windows.Controls.Border( );
            //        myBorder.BorderBrush = Brushes.Red;
            //        myBorder.BorderThickness = new Thickness( 2 );
            //        myBorder.Margin = new Thickness( 11, 1, 0, 0 );

            //        var Coords = new System.Windows.Controls.TextBlock( );
            //        Coords.Text = "Data: " + X + ", " + Y;


            //        Continent.Children.Add( myBorder );
            //        Continent.Children.Add( Coords );
            //        paintCanvas.Children.Add( Continent );

            //    }
            //}
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

        private void Tile_Click( object sender, RoutedEventArgs e )
        {
            Console.WriteLine( "LoL" );
        }
    }
}
