using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Venom.Data.Models;
using Venom.Helpers;
using Venom.Repositories;

namespace Venom.Components.ViewModels
{
    public class ServerSelectionViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _gameServerRepository;
        private readonly IVillageRepository _villageRepository;

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

        public ServerSelectionViewModel( 
            IGameServerRepository gameServerRepository,
            IVillageRepository villageRepository )
        {
            _gameServerRepository = gameServerRepository;
            _villageRepository = villageRepository;

        }

        public async Task OnLoaded()
        {
            await DialogHost.Show( new Dialogs.LoadingDialog( ), "Wasted", async ( sender, args ) =>
            {
                ServerDefault = await _gameServerRepository.GetGameServersAsync( ).ConfigureAwait( true );
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
                        Properties.Settings.Default.SelectedServer = ( int )obj;
                        await _villageRepository.GetVillagesAsync( ( int )obj ).ConfigureAwait( true );
                        args.Session.Close( );
                        MessengerInstance.Send( new GenericMessage<int>( 1 ) );
                    }, null ).ConfigureAwait( false );
                } );
            }
        }
    }
}

