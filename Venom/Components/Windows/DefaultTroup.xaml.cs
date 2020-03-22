using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Venom.Components.Windows
{
    /// <summary>
    /// Interaction logic for DefaultTroup.xaml
    /// </summary>
    public partial class DefaultTroup 
    {
        public DefaultTroup( )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            InitializeComponent( );
        }
    }
}
