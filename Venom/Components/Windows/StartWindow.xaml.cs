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
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Venom.Utility;

namespace Venom.Components.Windows
{
    /// <summary>
    /// Interaction logic for WindowCheckUpdates.xaml
    /// </summary>
    public partial class StartWindow 
    {
        public StartWindow( Start.StartViewModel model )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            //DataContext = ContainerHelper.Provider.GetRequiredService<StartWindowViewModel>( );
            DataContext = model;
            InitializeComponent( );
        }
    }
}
