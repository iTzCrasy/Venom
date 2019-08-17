using System.ComponentModel;
using Venom.Data.Models;
using Venom.Repositories;
using Venom.Helpers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System;

namespace Venom.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _localUsername = "";

        #region Properties
        public string LocalUsername
        {
            get => _localUsername;
            set => SetProperty( ref _localUsername, value );
        }
        #endregion

        #region Menu Commands
        public ICommand OnClickRankingPlayer => new RelayCommand<object>( ClickRankingPlayer );
        private void ClickRankingPlayer( object param )
        {
            Console.WriteLine( param );
        }

        public ICommand OnClickRankingAlly => new RelayCommand<object>( ClickRankingAlly );
        private void ClickRankingAlly( object param )
        {
            Console.WriteLine( param );
        }
        #endregion
    }
}
