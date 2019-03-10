using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        //=> Statics
        public ICommand CmdViewStatsAll => new CommandExt( _ => MainView = new Windows.MainViewStatsAll( ) );
        public ICommand CmdViewStatsRankPlayer => new CommandExt( _ => MainView = new Windows.MainViewStatsRankPlayer() );
        public ICommand CmdViewStatsRankAlly => new CommandExt( _ => MainView = new Windows.MainViewStatsRankAlly( ) );

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

        private string _CurrentUser;
        private object _MainView;
        
    }
}
