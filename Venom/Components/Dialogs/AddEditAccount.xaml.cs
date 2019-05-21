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
using Venom.Utility;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace Venom.Components.Dialogs
{
    public partial class AddEditAccount : CustomDialog
    {
        public AddEditAccount( MetroWindow parentWindow )
            : base( parentWindow )
        {
            if( DesignerProperties.GetIsInDesignMode( this ) )
            {
                return;
            }

            DataContext = ContainerHelper.Provider
                .GetRequiredService<AddEditAccountViewModel>( );

            InitializeComponent( );
        }

     
    }
}
