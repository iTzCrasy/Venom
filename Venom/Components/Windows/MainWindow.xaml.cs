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
using Venom.Components.ViewModels;
using MaterialDesignThemes.Wpf;

namespace Venom.Components.Windows
{
    public partial class MainWindow
    {
        private IntPtr _hWnd;
        private HwndSource _hWndSource;

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

        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            ;
        }

        protected override void OnRender( DrawingContext drawingContext )
        {
           
            base.OnRender( drawingContext );
        }

        private async void UserNameButton_Click( object sender, RoutedEventArgs e )
        {
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

        private async void Button_Click( object sender, RoutedEventArgs e )
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show( new Dialogs.LoadingDialog( ), "Wasted" ).ConfigureAwait( false );
        }
    }
}
