using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Venom.Data.Models;
using Venom.Helpers;

namespace Venom.Components.ViewModels
{
    public class ServerSelectionViewModel : ViewModelBase
    {
        private readonly Data.Api.IApiClient _ApiClient;

        #region Properties
        private List<ServerData> _ServerList = new List<ServerData>( );
        private Visibility _IsVisible = Visibility.Hidden;
        public List<ServerData> ServerDefault
        {
            get => _ServerList;
            set => SetProperty( ref _ServerList, value );
        }

        public Visibility IsVisible
        {
            get => _IsVisible;
            set => SetProperty( ref _IsVisible, value );
        }
        #endregion

        public ServerSelectionViewModel( Data.Api.IApiClient ApiClient )
        {
            _ApiClient = ApiClient;
            _ApiClient.Initialize( );
        }

        public async Task OnLoaded()
        {
            await DialogHost.Show( new Dialogs.LoadingDialog( ), "Wasted", async ( sender, args ) =>
            {
                ServerDefault = await _ApiClient.FetchServerList( ).ConfigureAwait( true );
                args.Session.Close( );
            }, null ).ConfigureAwait( false );
            IsVisible = Visibility.Visible;
        }

        public RelayCommand<object> OnSelectServer
        {
            get
            {
                return new RelayCommand<object>( async ( obj ) =>
                {
                    IsVisible = Visibility.Hidden;
                    await DialogHost.Show( new Dialogs.LoadingDialog( ), "Wasted", async ( sender, args ) =>
                    {
                        await _ApiClient.FetchVillages( ( int )obj ).ConfigureAwait( true );
                        args.Session.Close( );
                    }, null ).ConfigureAwait( false );
                } );
            }
        }
    }
}

