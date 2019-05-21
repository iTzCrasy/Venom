using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;
using Venom.Repositories;
using Venom.Helpers;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;

namespace Venom.Components.Dialogs
{
    internal class AddEditAccountViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;


        private ObservableCollection<GameServer> _gameServers;

        private GameServer _selectedServer;
        private bool _isActiveServer = false;

        private List<string> _playerNames;
        private bool _isActivePlayer = false;



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
                IsActivePlayer = true;
                LoadPlayerNames( value );

                SetProperty( ref _selectedServer, value );
            }
        }

        public bool IsActiveServer
        {
            get => _isActiveServer;
            set => SetProperty( ref _isActiveServer, value );
        }

        public List<string> PlayerNames
        {
            get => _playerNames;
            set => SetProperty( ref _playerNames, value );
        }

        public bool IsActivePlayer { get => _isActivePlayer; set => SetProperty( ref _isActivePlayer, value ); }
        #endregion



        public AddEditAccountViewModel(
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


            IsActiveServer = true;
            {
                var servers = await _serverRepo.GetGameServersAsync( )
                    .ConfigureAwait( true );

                GameServers = new ObservableCollection<GameServer>( servers );
            }
            IsActiveServer = false;
        }


        public async Task LoadPlayerNames( /*const*/ GameServer gameServer )
        {
            var names = await _playerRepo.GetPlayerNamesAsync( gameServer )
                .ConfigureAwait( false );

            Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            {
                PlayerNames = names;
                IsActivePlayer = false;
            } ) );
        }
    }
}
