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
using Venom.Game.Resources;

namespace Venom.Views
{
    /// <summary>
    /// Interaktionslogik für MainViewStats.xaml
    /// </summary>
    public partial class RankingPlayerView : UserControl
    {
        private readonly PlayerResource _playerResource;

        public RankingPlayerView(
            PlayerResource playerResource )
        {
            _playerResource = playerResource;

            InitializeComponent();

            DataContext = new ViewModels.RankingPlayerViewModel( )
            {
                PlayerList = _playerResource.GetPlayerList()
            };

            Playerlist.Items.SortDescriptions
                .Add( new SortDescription( "Points", ListSortDirection.Descending ) );
        }
    }
}
