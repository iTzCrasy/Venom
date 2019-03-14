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

namespace Venom.Dialogs
{
    /// <summary>
    /// Interaktionslogik für AddProfileDialog.xaml
    /// </summary>
    public partial class AddProfileDialog : UserControl
    {
        public AddProfileDialog()
        {
            InitializeComponent();
        }

        private void AddProfileDialogLoaded( object sender, RoutedEventArgs eventArgs )
        {
            Servers.ItemsSource = Game.GetInstance.GetServerList( );
            Servers.DisplayMemberPath = "ID";
            Servers.SelectedValuePath = "ID";
            Servers.Items.SortDescriptions.Add( new SortDescription( "ID", ListSortDirection.Ascending ) );
        }
    }
}
