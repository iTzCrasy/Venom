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
using System.Windows.Threading;
using System.Windows.Shell;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Venom.Components.Windows.Main
{
    public partial class MainWindow
    {
        private IntPtr _hWnd;
        private HwndSource _hWndSource;

        private const int size_x = 38;
        private const int size_y = 53;
        private const int space = 0;
        private bool[,] villages = new bool[50,50];
        private RectangleGeometry rectangleGeometry;

        public MainWindow()
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            DataContext = ContainerHelper.Provider.GetRequiredService<MainViewModel>( );

            InitializeComponent( );

            var interopHelper = new WindowInteropHelper( this );
            _hWndSource = HwndSource.FromHwnd( interopHelper.EnsureHandle( ) );
            _hWndSource.AddHook( WndProc );
            _hWnd = SetClipboardViewer( _hWndSource.Handle );
        }

        private async void MainWindow_Loaded( object sender, RoutedEventArgs e )
        {
                
        }

        private double _hOff = 1;

        public void DrawRectangles( ZoomableCanvas MyCanvas )
        {
            var rand = new Random( );

            Nullable<Point> dragPoint = null;


            MouseEventHandler mouseMove = ( sender, args ) => 
            {
                var element = ( UIElement )sender;

                if( element.IsMouseCaptured )
                {
                    //ScrollViewerTest.ScrollToVerticalOffset( _hOff + ( dragPoint.Value.Y - args.GetPosition( ScrollViewerTest ).Y ) );
                    //ScrollViewerTest.ScrollToHorizontalOffset( _hOff + ( dragPoint.Value.X - args.GetPosition( ScrollViewerTest ).X ) );
                }
            };

            MouseButtonEventHandler mouseDown = ( sender, args ) => 
            {
                var element = ( UIElement )sender;
                dragPoint = args.GetPosition( element );
                element.CaptureMouse( );
            };

            MouseButtonEventHandler mouseUp = ( sender, args ) => 
            {
                var element = ( UIElement )sender;
                dragPoint = null;
                element.ReleaseMouseCapture( );
            };

            Action<UIElement> enableDrag = ( element ) => 
            {
                element.MouseDown += mouseDown;
                element.MouseMove += mouseMove;
                element.MouseUp += mouseUp;
            };

            var images = new Brush[]
            {
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v1.png", UriKind.Relative ) ) ),
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v2.png", UriKind.Relative ) ) ),
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v3.png", UriKind.Relative ) ) ),
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v4.png", UriKind.Relative ) ) ),
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v5.png", UriKind.Relative ) ) ),
                new ImageBrush( new BitmapImage( new Uri( $"Resource\\Images\\Villages\\Version 3\\v6.png", UriKind.Relative ) ) ),
            };

            for( int j = 0; j < villages.GetLength( 1 ); j++ )
            {
                for( int i = 0; i < villages.GetLength( 0 ); i++ )
                {                                              
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size_x,
                        Width = size_y,
                    };

                    Ellipse ellipse = new Ellipse
                    {
                        Width = 9,
                        Height = 9,
                        Fill = Brushes.Red,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    rectangle.Fill = images[ rand.Next( 0, 5 ) ];

                    Canvas.SetTop( rectangle, j * ( size_x + space ) );
                    Canvas.SetLeft( rectangle, i * ( size_y + space ) );

                    Canvas.SetTop( ellipse, j * ( size_x + space ) );
                    Canvas.SetLeft( ellipse, i * ( size_y + space ) );

                    //MyCanvas.Children.Add( rectangle );
                    //MyCanvas.Children.Add( ellipse );
                }
            }

            MyCanvas.Height = 38 * 1000;
            MyCanvas.Width = 53 * 1000;

            enableDrag( MyCanvas );
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


        private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            switch( msg )
            {
                case WM_CHANGECBCHAIN:
                    if( wParam == _hWnd )
                    {
                        _hWnd = lParam;
                    }
                    else if( _hWnd != IntPtr.Zero )
                    {
                        SendMessage( _hWnd, msg, wParam, lParam );
                    }
                    break;

                case WM_DRAWCLIPBOARD:
                    var data = Clipboard.GetText( TextDataFormat.Html );
                    SendMessage( _hWnd, msg, wParam, lParam );
                    break;
            }

            return IntPtr.Zero;
        }

        internal const int WM_DRAWCLIPBOARD = 0x0308;
        internal const int WM_CHANGECBCHAIN = 0x030D;
        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern IntPtr SetClipboardViewer( IntPtr hWndNewViewer );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern bool ChangeClipboardChain( IntPtr hWndRemove, IntPtr hWndNewNext );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern IntPtr SendMessage( IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam );
    }
}
