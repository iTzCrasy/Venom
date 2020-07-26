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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Venom.Components.ViewModels;
using Venom.Utility;

namespace Venom.Components.Views
{
    /// <summary>
    /// Interaction logic for ServerSelection.xaml
    /// </summary>
    public partial class ServerSelection : UserControl
    {
        public ServerSelection( )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            DataContext = ContainerHelper.Provider.GetRequiredService<ServerSelectionViewModel>( );

            InitializeComponent( );
        }
    }
}
