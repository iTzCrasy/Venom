using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Venom.Windows
{
    /// <summary>
    /// Interaktionslogik für MainViewStatsAll.xaml
    /// </summary>
    public partial class MainViewStatsAll : UserControl
    {
        public MainViewStatsAll( )
        {
            InitializeComponent( );

            PlayerCount.DataContext = Core.Game.GetInstance.GetPlayerList( ).Count;
            VillageCount.DataContext = Core.Game.GetInstance.GetVillageList( ).Count;
            AllyCount.DataContext = Core.Game.GetInstance.GetAllyList( ).Count;
            BarbarVillages.DataContext = Core.Game.GetInstance.GetVillagesByPlayer( 0 ).Count;
            PlayerVillages.DataContext = Core.Game.GetInstance.GetVillageList( ).Count - Core.Game.GetInstance.GetVillagesByPlayer( 0 ).Count;
            BonusVillages.DataContext = Core.Game.GetInstance.GetVillagesBonusAll( ).Count;
            PlayerInAlly.DataContext = Core.Game.GetInstance.GetPlayerAllyAll( ).Count;
            PlayerWithoutAlly.DataContext = Core.Game.GetInstance.GetPlayerByAlly( 0 ).Count;
            ConquerCount.DataContext = Core.Game.GetInstance.GetConquerList( ).Count;
        }
    }
}
