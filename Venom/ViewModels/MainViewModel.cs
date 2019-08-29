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

namespace Venom.ViewModels
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public AccentColorMenuData( )
        {
            this.ChangeAccentCommand = new RelayCommand<object>( DoChangeTheme, o => true );
        }

        public ICommand ChangeAccentCommand { get; }

        protected virtual void DoChangeTheme( object sender )
        {
            ThemeManager.ChangeThemeColorScheme( Application.Current, this.Name );
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme( object sender )
        {
            ThemeManager.ChangeThemeBaseColor( Application.Current, this.Name );
        }
    }

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
