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
using Microsoft.Extensions.DependencyInjection;
using Venom.Utility;

namespace Venom.Components.Views
{
    /// <summary>
    /// Interaction logic for ViewPlayerSelection.xaml
    /// </summary>
    public partial class ViewPlayerSelection : UserControl
    {
        public ViewPlayerSelection( )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            InitializeComponent( );
        }
    }
}
