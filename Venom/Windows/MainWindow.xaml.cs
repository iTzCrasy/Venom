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
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Fluent;
using Venom.Domain;
using System.Windows.Controls.Primitives;

namespace Venom.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IRibbonWindow
    {
        private IntPtr _hWnd;
        private HwndSource _hWndSource;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Instance.ViewModelMain;

            //=> Place Hook
            var interopHelper = new WindowInteropHelper( this );
            _hWndSource = HwndSource.FromHwnd( interopHelper.EnsureHandle() );
            _hWndSource.AddHook( WndProc );
            _hWnd = SetClipboardViewer( _hWndSource.Handle );
        }

        public RibbonTitleBar TitleBar
        {
            get => ( RibbonTitleBar )GetValue( TitleBarProperty );
            private set => SetValue( TitleBarPropertyKey, value );
        }

        // ReSharper disable once InconsistentNaming
        private static readonly DependencyPropertyKey TitleBarPropertyKey = DependencyProperty.RegisterReadOnly( nameof( TitleBar ), typeof( RibbonTitleBar ), typeof( MainWindow ), new PropertyMetadata( ) );

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TitleBar"/>.
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;

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
                    //App.Instance.ClipboardHandler.Parse( ); //=> Parse Clipboard.
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

        private void VenomMainMenu_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            MenuToggleButton.IsChecked = false;
        }
    }
}
