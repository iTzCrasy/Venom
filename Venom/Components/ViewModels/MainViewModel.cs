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
using MaterialDesignThemes.Wpf;
using Venom.Helpers.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace Venom.Components.ViewModels
{
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
        private Visibility _navigationVisibility = Visibility.Hidden;

        public Visibility IsNavigationVisible
        {
            get => _navigationVisibility;
            set => SetProperty( ref _navigationVisibility, value );
        }

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
        }

        public async Task LoadWindow()
        {
            if( DesignerProperties.GetIsInDesignMode( new DependencyObject() ) )
            {
                return;
            }

            MessengerInstance.Register<GenericMessage<int>>( this, GlobalActionHandler );

            Content = new Views.LoadingView( );

            _resourceRepository.Initialize( );

            Content = new Views.ServerSelection( );

            if( Properties.Settings.Default.IsFirstStart )
            {
                //=> TODO: Handle First Start (tutorial, ..)
            }
            else
            {
            }
        }

        public void GlobalActionHandler( GenericMessage<int> genericMessage )
        {
            if( genericMessage.Content == 1 )
            {
                Content = new Views.Dashboard( );
                IsNavigationVisible = Visibility.Visible;
            }
        }

        public RelayCommand<UserControl> SwitchContent
        {
            get
            {
                return new RelayCommand<UserControl>( async ( obj ) =>
                {
                    await DialogHost.Show( new Dialogs.LoadingDialog( ), "Wasted", async ( sender, args ) =>
                    {
                                Content = obj;
                        args.Session.Close( );
                    }, null ).ConfigureAwait( false );
                } );
            }
        }
    }
}
