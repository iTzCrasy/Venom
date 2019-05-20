using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;
using Venom.Repositories;
using Venom.Helpers;

namespace Venom.Components.Dialogs
{
    internal class AddEditAccountViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _repo;


        private GameServer _selectedServer;


        public ObservableCollection<GameServer> GameServers { get; set; }

        public GameServer SelectedServer
        {
            get => _selectedServer;
            set => Set( ref _selectedServer, value, nameof( SelectedServer ) );
        }

        public bool IsFetching { get; set; }


        public AddEditAccountViewModel( 
            IGameServerRepository repo 
            )
        {
            _repo = repo;
            GameServers = new ObservableCollection<GameServer>( );
        }


        public async Task LoadGameServers( )
        {
            var servers = await _repo.GetGameServersAsync( )
                .ConfigureAwait( true );

            GameServers = new ObservableCollection<GameServer>( servers );
        }

    }
}
