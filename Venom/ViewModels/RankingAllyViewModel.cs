using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class RankingAllyViewModel : NotifyPropertyChangedExt
    {
        public IEnumerable<AllyData> AllyList { get; set; }


        public RankingAllyViewModel( )
        {
        }
    }
}
