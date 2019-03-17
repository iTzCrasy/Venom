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
    /// Interaktionslogik für RankingAllyView.xaml
    /// </summary>
    public partial class RankingAllyView : UserControl
    {
        private readonly AllyResource _allyResource;

        public RankingAllyView( 
            AllyResource allyResource
            )
        {
            _allyResource = allyResource;

            InitializeComponent( );

            DataContext = new ViewModels.RankingAllyViewModel( )
            {
                AllyList = _allyResource.GetAllyList( ),
            };

            Allylist.Items.SortDescriptions
                .Add( new SortDescription( "Points", ListSortDirection.Descending ) );
        }
    }
}
