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

namespace Venom.Windows
{
    public partial class MainWindow
    {
        private IntPtr _hWnd;

        private readonly HwndSource _hWndSource;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = CreateDataContext( );

            var interopHelper = new WindowInteropHelper( this );
            _hWndSource = HwndSource.FromHwnd( interopHelper.EnsureHandle() );
            _hWndSource.AddHook( WndProc );
            _hWnd = WinApi.SetClipboardViewer( _hWndSource.Handle );
        }


        private object CreateDataContext( )
        {
            var menuItems = new[]
            {
                //new MainMenuItem()
                //{
                //    Group = "Start",
                //    Title = "Start",
                //    Image = "",
                //    Content = new ViewStart(),
                //},

                new MainMenuItem()
                {
                    Group = "Start",
                    Title = "Server Auswahl",
                    Image = "",
                    Content = new ServerSelection(),
                },

                new MainMenuItem()
                {
                    Group = "Allgemein",
                    Title = "Karte",
                    Image = "/Venom;component/Assets/Images/map2.png",
                    Content = null
                },

                new MainMenuItem()
                {
                    Group = "Allgemein",
                    Title = "Truppenliste",
                    Image = "",
                    Content = new ViewTroupList(),
                },

                new MainMenuItem()
                {
                    Group = "Ranglisten",
                    Title = "Rangliste Spieler",
                    Image = "",
                    Content = new RankingPlayerView(),
                },

                new MainMenuItem()
                {
                    Group = "Ranglisten",
                    Title = "Rangliste Stämme",
                    Image = "",
                    Content = new RankingAllyView(),
                },

                //new MainMenuItem()
                //{
                //    Group = "Ranglisten",
                //    Title = "Eroberungen",
                //    Image = "/Venom;component/Assets/Images/unit_snob.png",
                //    Content = new ConquerView(),
                //},

                new MainMenuItem()
                {
                    Group = "Planer",
                    Title = "Angriffsplaner",
                    Image = "/Venom;component/Assets/Images/unit_axe.png",
                    Content = new ViewPlaner(),
                }
            };

            var menuCollection = CollectionViewSource.GetDefaultView( menuItems );
            menuCollection.GroupDescriptions.Add( new PropertyGroupDescription( "Group" ) );



            return new MainViewModel
            {
                LocalUsername = App.Instance.Profile.Local.Name,

                MenuCollection = menuCollection,
            };
        }


        private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            switch( msg )
            {
                case WindowMessage.WM_CHANGECBCHAIN:
                    if( wParam == _hWnd )
                    {
                        _hWnd = lParam;
                    }
                    else if( _hWnd != IntPtr.Zero )
                    {
                        WinApi.SendMessage( _hWnd, msg, wParam, lParam );
                    }
                    break;

                case WindowMessage.WM_DRAWCLIPBOARD:
                    App.Instance.ClipboardHandler.Parse( );

                    WinApi.SendMessage( _hWnd, msg, wParam, lParam );
                    break;
            }

            return IntPtr.Zero;
        }

        private void Window_Closing( object sender, CancelEventArgs e )
        {
            App.Instance.Shutdown( );
        }

        private void HamburgerMenuControl_ItemInvoked( object sender, MahApps.Metro.Controls.HamburgerMenuItemInvokedEventArgs e )
        {
            HamburgerMenuControl.Content = e.InvokedItem;

            if( HamburgerMenuControl.IsPaneOpen )
            {
                HamburgerMenuControl.IsPaneOpen = false;
            }
        }
    }
}
