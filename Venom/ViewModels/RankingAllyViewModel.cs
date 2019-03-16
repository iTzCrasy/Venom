using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels
{
    public class RankingAllyViewModel : NotifyPropertyChangedExt
    {
        public object AllyList => Core.Game.GetInstance.GetAllyList( );

        public RankingAllyViewModel( )
        {
        }
    }
}
