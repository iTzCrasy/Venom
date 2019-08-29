using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Venom.Helpers;
using Venom.Repositories;

namespace Venom.Components.Dialogs
{
    internal class DialogLoadVenomViewModel : ViewModelBase
    {
        private readonly IGameServerRepository _serverRepo;
        private readonly IPlayerRepository _playerRepo;


        private string _localUsername = "";

        #region Properties
        public string LocalUsername
        {
            get => _localUsername;
            set => SetProperty( ref _localUsername, value, nameof( LocalUsername ) );
        }
        #endregion


        public DialogLoadVenomViewModel(
            IGameServerRepository serverRepo,
            IPlayerRepository playerRepo
            )
        {
            _serverRepo = serverRepo;
            _playerRepo = playerRepo;
        }


        public async Task LoadVenom( )
        {
            if( DesignerProperties.GetIsInDesignMode( new DependencyObject( ) ) )
            {
                return;
            }

            Application.Current.Dispatcher.Invoke( new Action( ( ) =>
            {
                LocalUsername = "Moralbasher";
            } ) );

            
        }
    }
}
