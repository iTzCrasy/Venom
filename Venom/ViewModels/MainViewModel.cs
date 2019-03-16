using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Venom.Domain;

namespace Venom.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedExt
    {
        public MainViewModel( )
        {
            CurrentUser = "Moralbasher";
        }

        //=> Main
        public ICommand CmdViewMainMap => new CommandExt( _ => MainView = new Windows.MainViewMap( ) );
        public ICommand CmdViewTroupList => new CommandExt( _ => MainView = Global.ViewTroupList );


        //=> Statics
        public ICommand CmdViewStatsAll => new CommandExt( _ => MainView = new Windows.MainViewStatsAll( ) );
        public ICommand CmdViewMainRankingPlayer => new CommandExt( _ => MainView = Global.ViewRankingPlayer );
        public ICommand CmdViewMainRankingAllys => new CommandExt( _ => MainView = Global.ViewRankingAlly );

        public ICommand TestCommand => new CommandExt( OnTestCommand );

        private void OnTestCommand( object O )
        {
        }

        public object MainView
        {
            get => _MainView;
            set => SetProperty( ref _MainView, value );
        }

        public string CurrentUser
        {
            get => _CurrentUser;
            set => SetProperty( ref _CurrentUser, value  );
        }

        public List<Core.GamePlayers> PlayerList
        {
            get => Core.Game.GetInstance.GetPlayerList( );
        }

        protected string _CurrentUser;
        protected object _MainView;
    }
}
