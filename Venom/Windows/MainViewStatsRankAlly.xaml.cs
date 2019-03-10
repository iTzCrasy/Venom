using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Core;

namespace Venom.Windows
{
    /// <summary>
    /// Interaktionslogik für MainViewStatsRankAlly.xaml
    /// </summary>
    public partial class MainViewStatsRankAlly : UserControl
    {
        public MainViewStatsRankAlly( )
        {
            InitializeComponent( );

            Allylist.ItemsSource = Game.GetInstance.GetAllyList( );
            Allylist.Items.SortDescriptions.Add( new SortDescription( "Points", ListSortDirection.Descending ) );
        }

    }
}
