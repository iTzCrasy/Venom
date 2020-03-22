using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Venom.Helpers;

namespace Venom.Components.Windows.Start.Models
{
    public class ViewModelLogin : ViewModelBase
    {
        #region Properties
        private string _username = null;
        private string _password = "LeL";
        private bool _IsSaveCheck = false;
        private bool _IsLoginEnabled = false;
        private string _loginText = "Login";

        public string Username
        {
            get => _username;
            set
            {
                SetProperty( ref _username, value );
                IsLoginEnabled = !string.IsNullOrEmpty( Username ) && !string.IsNullOrEmpty( Password );
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty( ref _password, value );
                IsLoginEnabled = !string.IsNullOrEmpty( Username ) && !string.IsNullOrEmpty( Password );
            }
        }

        public bool IsSaveCheck
        {
            get => _IsSaveCheck;
            set => SetProperty( ref _IsSaveCheck, value );
        }

        public bool IsLoginEnabled
        {
            get => _IsLoginEnabled;
            set => SetProperty( ref _IsLoginEnabled, value );
        }

        public string LoginText
        {
            get => _loginText;
            set => SetProperty( ref _loginText, value );
        }
        #endregion

        public ViewModelLogin( )
        {
            //=> TODO: Get Network Injection

            //=> Load Settings
            IsSaveCheck = Properties.Settings.Default.SaveCheck;
            Username = Properties.Settings.Default.SaveUsername;
            Password = Properties.Settings.Default.SavePassword;
        }

        public async Task ViewLoaded( )
        {
        }

        /// <summary>
        /// Command Click Login
        /// </summary>
        public ICommand OnClickLogin => new RelayCommand<object>( async ( object param ) =>
        {
            //=> Disable Login Button
            IsLoginEnabled = false;
            LoginText = "Connecting..";

            var Result = await Network.Rest.Client.LoginAsync( Username, Password );
            if( Result == 0 )
            {
                //=> TODO: Error
                IsLoginEnabled = true;
                LoginText = "Connecting..";
            }
            else
            {
                //=> TODO: Open Main Window / Load
            }

            //=> Save Datas
            Properties.Settings.Default.SaveCheck = IsSaveCheck;
            Properties.Settings.Default.SaveUsername = IsSaveCheck ? Username : "";
            Properties.Settings.Default.SavePassword = IsSaveCheck ? Password : "";
            Properties.Settings.Default.Save( );
        } );       
    }
}
