using System.ComponentModel;
using Venom.Data.Models;
using Venom.Repositories;
using Venom.Helpers;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
