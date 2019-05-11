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
using System.Windows.Shapes;
using Venom.ViewModels.Dialog;

namespace Venom.Dialogs
{
    public partial class SelectServer
    {


        public SelectServer( )
        {
            InitializeComponent( );

            DataContext = new SelectServerViewModel
            {
                ServerItems = new SelectServerItemViewModel[]
                {
                    new SelectServerItemViewModel
                    {
                        AccountName = "Moralbasher",
                        ServerName = "Server: 161",
                        LastUsed = DateTime.Now,
                    },

                    new SelectServerItemViewModel
                    {
                        AccountName = "Moralbasher",
                        ServerName = "Server: 161",
                        LastUsed = DateTime.Now,
                    },

                    new SelectServerItemViewModel
                    {
                        AccountName = "Moralbasher",
                        ServerName = "Server: 161",
                        LastUsed = DateTime.Now,
                    },

                    new SelectServerItemViewModel
                    {
                        AccountName = "Moralbasher",
                        ServerName = "Server: 161",
                        LastUsed = DateTime.Now,
                    },

                    new SelectServerItemViewModel
                    {
                        AccountName = "Moralbasher",
                        ServerName = "Server: 161",
                        LastUsed = DateTime.Now,
                    },
                },
            };
        }

        private void ListBoxItem_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {

        }
    }
}
