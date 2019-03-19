using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Venom.Domain;
using Venom.Game;

namespace Venom.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedExt
    {
        private readonly Profile _profile;
        private readonly Server _server;
        protected object _MainView;

        //=> Main
        public ICommand CmdViewMainMap => new CommandExt( _ => MainView = new Windows.MainViewMap( ) );
        public ICommand CmdViewTroupList => new CommandExt( _ => MainView = App.Instance.ViewTroupList );

        //=> Statics
        public ICommand CmdViewStatsAll => new CommandExt( _ => MainView = new Windows.MainViewStatsAll( ) );
        public ICommand CmdViewMainRankingPlayer => new CommandExt( _ => MainView = App.Instance.ViewRankingPlayer );
        public ICommand CmdViewMainRankingAllys => new CommandExt( _ => MainView = App.Instance.ViewRankingAlly );

        public MainViewModel( 
            Profile profile,
            Server server )
        {
            _profile = profile;
            _server = server;

           // MainView = App.Instance.ViewRankingPlayer; //=> Set Default View.
        }

        public object MainView
        {
            get => _MainView;
            set => SetProperty( ref _MainView, value, "MainView" );
        }

        public string CurrentUser
        {
            get => _profile.Local.Name;
        }
    }
}
