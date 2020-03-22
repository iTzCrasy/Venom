using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using Venom.Helpers;

namespace Venom.Components.Windows.Start
{
    public class StartViewModel : ViewModelBase
    {
        #region Properties
        private readonly Views.ViewLogin _viewLogin;
        private readonly Views.ViewLoading _viewLoading;

        private object _content = "";
        private string _loadingText = "";


        public object Content
        {
            get => _content;
            set => SetProperty( ref _content, value );
        }

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty( ref _loadingText, value );
        }
        #endregion


        public StartViewModel( 
            Views.ViewLoading viewLoading,
            Views.ViewLogin viewLogin )
        {
            _viewLoading = viewLoading;
            _viewLogin = viewLogin;
        }

        public async Task WindowLoaded()
        {
            Content = _viewLoading;
            LoadingText = "Searching for updates";
            //// Show...
            //ProgressDialogController controller = await MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance.ShowProgressAsync( this, "Login", "Message" );
            //controller.SetIndeterminate( );

            //await Task.Delay( 2000 ).ConfigureAwait( true );

            //=> TODO: Check for Updates!
            //Content = _viewLogin;
        }

        public void SetState( int state )
        {
            switch( state )
            {
                case 1:
                {
                    Content = _viewLoading;
                    LoadingText = "Searching for updates";
                }
                break;
                case 2:
                {
                    Content = _viewLoading;
                    LoadingText = "Login";
                }
                break;
                case 3:
                {
                    
                }
                break;
            }
        }
    }
}
