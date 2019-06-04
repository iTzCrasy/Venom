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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Venom.Utility;

namespace Venom.Components.Dialogs
{
    /// <summary>
    /// Interaktionslogik für LoadVenom.xaml
    /// </summary>
    public partial class DialogLoadVenom : CustomDialog
    {
        public DialogLoadVenom( MetroWindow parentWindow )
          : base( parentWindow )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            DataContext = ContainerHelper.Provider
                .GetRequiredService<DialogLoadVenomViewModel>( );

            InitializeComponent( );
        }
    }
}
