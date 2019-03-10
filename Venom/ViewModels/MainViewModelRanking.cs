using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels
{
    public class MainViewModelRanking : NotifyPropertyChangedExt
    {
        public MainViewModelRanking( int Type )
        {
            switch( Type )
            {
                case 0:
                    _CurrentView = new Views.RankingPlayer( );
                    break;
                case 1:
                    _CurrentView = new Views.RankingAllys( );
                    break;
            }
        }

        public object PlayerList => Core.Game.GetInstance.GetPlayerList( );
        public object AllyList => Core.Game.GetInstance.GetAllyList( );
        public object ConquerList => Core.Game.GetInstance.GetConquerList( );

        public object CurrentView
        {
            get => _CurrentView;
            set => SetProperty( ref _CurrentView, value );
        }

        protected object _CurrentView;
    }
}
