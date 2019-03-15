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
using Venom.Core;

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

            PlayerCount.DataContext = Game.GetInstance.GetPlayerList( ).Count;
            VillageCount.DataContext = Game.GetInstance.GetVillageList( ).Count;
            AllyCount.DataContext = Game.GetInstance.GetAllyList( ).Count;
            BarbarVillages.DataContext = Game.GetInstance.GetVillagesByPlayer( 0 ).Count;
            PlayerVillages.DataContext = Game.GetInstance.GetVillageList( ).Count - Game.GetInstance.GetVillagesByPlayer( 0 ).Count;
            BonusVillages.DataContext = Game.GetInstance.GetVillagesBonusAll( ).Count;
            PlayerInAlly.DataContext = Game.GetInstance.GetPlayerAllyAll( ).Count;
            PlayerWithoutAlly.DataContext = Game.GetInstance.GetPlayerByAlly( 0 ).Count;
            ConquerCount.DataContext = Game.GetInstance.GetConquerList( ).Count;
        }
    }
}
