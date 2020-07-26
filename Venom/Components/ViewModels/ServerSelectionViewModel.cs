using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;
using Venom.Helpers;

namespace Venom.Components.ViewModels
{
    public class ServerSelectionViewModel : ViewModelBase
    {
        private readonly Data.Api.IApiClient _ApiClient;
        private List<ServerData> _ServerList = new List<ServerData>( );

        public ServerSelectionViewModel( Data.Api.IApiClient ApiClient )
        {
            _ApiClient = ApiClient;
            _ApiClient.Initialize( );
        }

        public List<ServerData> ServerDefault
        {
            get => _ServerList;
            set => SetProperty( ref _ServerList, value );
        }

        public async Task OnLoaded()
        {
            ServerDefault = await _ApiClient.FetchServerList( ).ConfigureAwait( true );
        }
    }
}
