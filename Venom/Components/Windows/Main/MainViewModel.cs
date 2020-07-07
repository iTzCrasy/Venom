using System.ComponentModel;
using Venom.Data.Models;
using Venom.Repositories;
using Venom.Helpers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System;
using System.Windows.Media;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using Venom.Data.Models.Configuration;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Linq;

namespace Venom.Components.Windows.Main
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public AccentColorMenuData( )
        {
            this.ChangeAccentCommand = new RelayCommand<object>( DoChangeTheme, o => true );
        }

        public ICommand ChangeAccentCommand { get; }

        protected virtual void DoChangeTheme( object sender )
        {
            ThemeManager.ChangeThemeColorScheme( Application.Current, Name );
        }
    }

    public class MainViewModel : ViewModelBase
    {
        #region Content Handling
        private object _content = "";

        public object Content
        {
            get => _content;
            set => SetProperty( ref _content, value );
        }
        #endregion

        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;
        private readonly IVillageRepository _villageRepo;
        private readonly IResourceRepository _resourceRepository;

        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AccentColorMenuData> AppThemes { get; set; }

        public AccentColorMenuData _selectedTheme;
        public AccentColorMenuData _selectedColor;

        private string _localUsername = "";
        private string _serverTime = "";
        private string _serverPing = "";

        public MainViewModel(
            IGameServerRepository serverRepo,
            IPlayerRepository playerRepo,
            IVillageRepository villageRepo,
            IResourceRepository resourceRepository
        )
        {
            _serverRepo = serverRepo;
            _playerRepo = playerRepo;
            _villageRepo = villageRepo;
            _resourceRepository = resourceRepository;

            AccentColors = ThemeManager.ColorSchemes
                .Select( a => new AccentColorMenuData { Name = a.Name, ColorBrush = a.ShowcaseBrush } )
                .ToList( );

            AppThemes = ThemeManager.Themes
                .GroupBy( x => x.BaseColorScheme )
                .Select( x => x.First( ) )
                .Select( a => new AccentColorMenuData( ) { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush } )
                .ToList( );

            Content = new Venom.Components.Views.WorldView( );
            //Content = new Venom.Components.Views.ServerListView( );

            //_selectedTheme.ColorBrush = Brushes.Black;
        }

        #region Properties
        public string LocalUsername
        {
            get => _localUsername;
            set => SetProperty( ref _localUsername, value );
        }

        public string ServerTime
        {
            get => _serverTime;
            set => SetProperty( ref _serverTime, value );
        }

        public string ServerPing
        {
            get => _serverPing;
            set => SetProperty( ref _serverPing, value );
        }

        public AccentColorMenuData CurrentTheme
        {
            get => _selectedTheme;
            set
            {
                SetProperty( ref _selectedTheme, value );
                ThemeManager.ChangeThemeBaseColor( Application.Current, value.Name );
            }
        }

        public AccentColorMenuData CurrentColorScheme
        {
            get => _selectedColor;
            set
            {
                SetProperty( ref _selectedColor, value );
                ThemeManager.ChangeThemeColorScheme( Application.Current, value.Name );
            }
        }

        #endregion

        #region Menu Commands
        public ICommand OnClickRankingPlayer => new RelayCommand<object>( ClickRankingPlayer );
        private void ClickRankingPlayer( object param )
        {
            Console.WriteLine( param );
        }

        public ICommand OnClickRankingAlly => new RelayCommand<object>( ClickRankingAlly );
        private void ClickRankingAlly( object param )
        {
            Console.WriteLine( param );
        }
        #endregion

        public ICommand OnExecuteMenu => new RelayCommand<object>( ExecuteMenu );
        private void ExecuteMenu( object param )
        {
            //Console.WriteLine( "ExecuteMenu" );
            //var dialog = new AddAccount( null );

            //DialogCoordinator.Instance.ShowMetroDialogAsync( this, dialog );
        }


        public async Task LoadWindow()
        {
            if( DesignerProperties.GetIsInDesignMode( new DependencyObject() ) )
            {
                return;
            }

            _resourceRepository.Initialize( );
            //await _worldManager.Initialize( ).ConfigureAwait( false );

            //List<double> test = new List<double>( );
            //test.Add( 90 );

            //double value = 90;
            //for( int i = 0; i != 29; i++ )
            //{
            //    test.Add( test[i] * 1.26 );
            //}

        }
    }
}
