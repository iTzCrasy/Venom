using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;
using Venom.Helpers;
using Venom.Repositories;

namespace Venom.Components.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _gameServerRepository;
        private readonly IVillageRepository _villageRepository;


        private ServerData _currentServer;
        public ServerData CurrentServer
        {
            get => _currentServer;
            set => SetProperty( ref _currentServer, value );
        }

        public DashboardViewModel(
            IGameServerRepository gameServerRepository,
            IVillageRepository villageRepository )
        {
            _gameServerRepository = gameServerRepository;
            _villageRepository = villageRepository;
        }

        public async Task LoadWindow( )
        {
            CurrentServer = await _gameServerRepository.GetServerAsync( Properties.Settings.Default.SelectedServer ).ConfigureAwait( true );                       
        }
    }
}
