using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Shapes;
using System.Windows.Controls;
using Venom.Helpers;
using Venom.Utility;

namespace Venom.Components.Windows.Start
{
    public class StartViewModel : ViewModelBase
    {
        #region Properties
        private object _content = "";

        public object Content
        {
            get => _content;
            set => SetProperty( ref _content, value );
        }
        #endregion


        public StartViewModel()
        {
        }

        public async Task WindowLoaded( )
        {
            Content = new Views.ViewLoading( )
            {

            };
           /* await Task.Delay( 3000 ).ConfigureAwait( true );*/ //=> TODO: Searching for Updates
            //Content = new Views.ViewLogin( )
            //{
            //    DataContext = ContainerHelper.Provider.GetRequiredService<Models.ViewModelLogin>( )
            //};
        }
    }
}
