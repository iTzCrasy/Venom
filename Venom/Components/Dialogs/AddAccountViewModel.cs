using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Venom.Data.Models;
using Venom.Helpers;
using Venom.Repositories;

namespace Venom.Components.Dialogs
{
    internal class AddAccountViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;


        private ObservableCollection<GameServer> _gameServers;

        private GameServer _selectedServer = null;
        private List<string> _playerNames;
        private string _selectedPlayer = null;

        private bool _isPlayerSelectionEnabled = false;
        private bool _isProgressVisible = false;
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

                SetProperty( ref _selectedPlayer, value );
            }
        }

        public bool IsPlayerSelectionEnabled { get => _isPlayerSelectionEnabled; set => SetProperty( ref _isPlayerSelectionEnabled, value ); }
        public bool IsProgressVisible { get => _isProgressVisible; set => SetProperty( ref _isProgressVisible, value ); }
        public bool IsLoginButtonEnabled { get => _isLoginButtonEnabled; set => SetProperty( ref _isLoginButtonEnabled, value ); }
        public bool IsDefaultChecked { get => _isDefaultChecked; set => SetProperty( ref _isDefaultChecked, value ); }
        #endregion



        public AddAccountViewModel(
            IGameServerRepository serverRepo,
            IPlayerRepository playerRepo
            )
        {
            _serverRepo = serverRepo;
            _playerRepo = playerRepo;
        }


        public async Task LoadGameServers( )
        {
            if( DesignerProperties.GetIsInDesignMode( new DependencyObject( ) ) )
            {
                return;
            }


            IsProgressVisible = true;
            {
                var servers = await _serverRepo.GetGameServersAsync( )
                    .ConfigureAwait( true );

                GameServers = new ObservableCollection<GameServer>( servers );
            }
            IsProgressVisible = false;
        }


        public async Task LoadPlayerNames( /*const*/ GameServer gameServer )
        {
            IsProgressVisible = true;

            var names = await _playerRepo.GetPlayerNamesAsync( gameServer )
                .ConfigureAwait( false );

            Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            {
                PlayerNames = names;
                IsPlayerSelectionEnabled = true;
            } ) );

            IsProgressVisible = false;
        }



        public ICommand OnClickStart => new RelayCommand<object>( CmdClickStart );
        private void CmdClickStart( object param )
        {
            Properties.Settings.Default.DefaultSave = IsDefaultChecked; //=> Save datas?

            if( IsDefaultChecked )
            {
                Properties.Settings.Default.DefaultAccount = SelectedPlayer;
                Properties.Settings.Default.DefaultServer = SelectedServer.Id;
            }
            else
            {
                Properties.Settings.Default.DefaultAccount = "";
                Properties.Settings.Default.DefaultServer = "";
            }

            Properties.Settings.Default.Save( );
        }
    }
}
