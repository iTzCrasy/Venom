using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using MahApps.Metro.Controls.Dialogs;
using RestSharp;
using Venom.Data;
using Venom.Data.Models;
using Venom.Helpers;
using Venom.Repositories;


namespace Venom.Components.Windows
{
    public class StartWindowViewModel : ViewModelBase
    {
        //=> Views
        private readonly Start.Views.ViewLogin _viewLogin;

        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;
        private readonly DataContext _context;

        private object _contentView = "";

        private ObservableCollection<GameServer> _gameServers;

        private GameServer _selectedServer = null;
        private List<string> _playerNames;
        private string _selectedPlayer = null;

        private bool _isPlayerSelectionEnabled = false;
        private bool _isLoginButtonEnabled = false;
        private bool _isDefaultChecked = false;

        #region Properties
        public object ContentView
        {
            get => _contentView;
            set => SetProperty( ref _contentView, value );
        }

        public ObservableCollection<GameServer> GameServers
        {
            get => _gameServers;
            set => SetProperty( ref _gameServers, value );
        }

        public GameServer SelectedServer
        {
            get => _selectedServer;
            set
            {
                _context.CurrentServer = value;
                LoadPlayerNames( value );
                SetProperty( ref _selectedServer, value );
            }
        }

        public List<string> PlayerNames
        {
            get => _playerNames;
            set => SetProperty( ref _playerNames, value );
        }

        public string SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                IsLoginButtonEnabled = string.IsNullOrEmpty( value ) ? false : true;
                SetProperty( ref _selectedPlayer, value );
            }
        }

        public bool IsPlayerSelectionEnabled { get => _isPlayerSelectionEnabled; set => SetProperty( ref _isPlayerSelectionEnabled, value ); }
        public bool IsLoginButtonEnabled { get => _isLoginButtonEnabled; set => SetProperty( ref _isLoginButtonEnabled, value ); }
        public bool IsDefaultChecked { get => _isDefaultChecked; set => SetProperty( ref _isDefaultChecked, value ); }
        #endregion


        public StartWindowViewModel( 
            IGameServerRepository serverRepo,
            IPlayerRepository playerRepo,
            DataContext dataContext,
            Start.Views.ViewLogin viewLogin )
        {
            _serverRepo = serverRepo;
            _playerRepo = playerRepo;
            _context = dataContext;


            //=> Views
            _viewLogin = viewLogin;
        }

        /// <summary>
        /// Window loaded, set first content!
        /// </summary>
        /// <returns></returns>
        public async Task WindowLoaded()
        {
            ContentView = new Views.ViewSearchingUpdates( );

            //var client = new RestClient( "http://google.de/" );
            //var request = new RestRequest( "api/patch", Method.GET );
            //var response = client.ExecuteAsync( request );
            //await response.ConfigureAwait( false );
            //Console.WriteLine( response.Result );
            //await Task.Delay( 3000 ).ConfigureAwait( true );
            await LoadGameServers( );
            //ContentView = new Views.ViewStartLogin( );
            ContentView = _viewLogin;
        }

        public async Task LoadGameServers( )
        {
            if( DesignerProperties.GetIsInDesignMode( new DependencyObject( ) ) )
            {
                return;
            }

            var servers = await _serverRepo.GetGameServersAsync( )
                .ConfigureAwait( true );

            GameServers = new ObservableCollection<GameServer>( servers );

            //IsDefaultChecked = Properties.Settings.Default.DefaultSave;

            //if( IsDefaultChecked && Properties.Settings.Default.DefaultServer != null )
            //{
            //    SelectedServer = DeserializeObject<GameServer>( Properties.Settings.Default.DefaultServer );
            //    SelectedPlayer = DeserializeObject<string>( Properties.Settings.Default.DefaultAccount );
            //}
        }

        public async Task LoadPlayerNames( /*const*/
          GameServer gameServer )
        {
            IsPlayerSelectionEnabled = false;

            var names = await _playerRepo.GetPlayerNamesAsync( )
                .ConfigureAwait( false );

            Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            {
                PlayerNames = names;
                IsPlayerSelectionEnabled = true;
            } ) );
        }

        public ICommand OnClickStart => new RelayCommand<object>( CmdClickStart );
        private void CmdClickStart( object param )
        {
            ContentView = new Views.ViewSearchingUpdates( );
            //=> Update Context!
            _context.CurrentPlayer = SelectedPlayer;
            _context.CurrentServer = SelectedServer;
        }
    }
}
