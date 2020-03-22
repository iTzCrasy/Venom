using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Venom.Data;
using Venom.Data.Models;
using Venom.Helpers;
using Venom.Repositories;

namespace Venom.Components.Views
{
    internal class ViewPlayerSelectionViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;
        private readonly DataContext _context;

        private ObservableCollection<GameServer> _gameServers;

        private GameServer _selectedServer = null;
        private List<string> _playerNames;
        private string _selectedPlayer = null;

        private bool _isPlayerSelectionEnabled = false;
        private bool _isLoginButtonEnabled = false;
        private bool _isDefaultChecked = false;

        #region Properties
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
                IsLoginButtonEnabled = false; //=> Reset Login Button
                IsPlayerSelectionEnabled = false;
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
                IsLoginButtonEnabled = true; //=> Enable Login Button
                _context.CurrentPlayer = value;
                SetProperty( ref _selectedPlayer, value );
            }
        }

        public bool IsPlayerSelectionEnabled { get => _isPlayerSelectionEnabled; set => SetProperty( ref _isPlayerSelectionEnabled, value ); }
        public bool IsLoginButtonEnabled { get => _isLoginButtonEnabled; set => SetProperty( ref _isLoginButtonEnabled, value ); }
        public bool IsDefaultChecked { get => _isDefaultChecked; set => SetProperty( ref _isDefaultChecked, value ); }
        #endregion


        public ViewPlayerSelectionViewModel(
         IGameServerRepository serverRepo,
         IPlayerRepository playerRepo,
         DataContext dataContext
         )
        {
            _serverRepo = serverRepo;
            _playerRepo = playerRepo;
            _context = dataContext;
        }

        /// <summary>
        /// View loaded
        /// </summary>
        /// <returns></returns>
        public async Task ViewLoaded( )
        {
            //await Task.Delay( 3000 ).ConfigureAwait( true );
            await LoadGameServers( );
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
            //=> Update Context!
            _context.CurrentPlayer = SelectedPlayer;
            _context.CurrentServer = SelectedServer;
        }
    }
}
