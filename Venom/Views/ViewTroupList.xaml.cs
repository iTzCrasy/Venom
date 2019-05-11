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

using Venom.Game;

namespace Venom.Views
{
    public partial class ViewTroupList : UserControl
    {
        public ViewTroupList( )
        {
            InitializeComponent( );

            DataContext = App.Instance.ViewModelTroupList;

            if( App.Instance.CurrentServer.Local.Config.Archer == false )
            {
                ViewTroups.Columns.Remove( ColumnArcher );
                ViewTroups.Columns.Remove( ColumnMArcher );
            }
        }
    }
}

