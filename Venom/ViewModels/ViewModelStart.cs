using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using MaterialDesignThemes.Wpf;
using Venom.Domain;
using Venom.Game;

namespace Venom.ViewModels
{
    public class ViewModelStart : NotifyPropertyChangedExt
    {
        private readonly Profile _profile;
        private readonly Server _server;
        private object _currentContent;
        private string _username;
        private ProfileData _selectedProfile;
        private ServerData _selectedServer;

        public ICommand CmdClickStart => new CommandExt( ClickStart );
        public ICommand CmdClickAdd => new CommandExt( ClickAdd );
        public ICommand CmdClickDel => new CommandExt( ClickDel );

        public ViewModelStart( 
            Profile profile, 
            Server server )
        {
            _profile = profile;
            _server = server;
            _currentContent = App.Instance.ViewSelectServer;
        }

        public ObservableCollection<ProfileData> ProfileList
        {
            get => new ObservableCollection<ProfileData>( _profile.GetList( ) );
        }

        public IEnumerable<ServerData> ServerList
        {
            get => _server.GetList( );
        }

        public object CurrentContent
        {
            get => _currentContent;
            set => SetProperty( ref _currentContent, value );
        }

        public string Username
        {
            get => _username;
            set => SetProperty( ref _username, value );
        }

        public ProfileData SelectedProfile
        {
            get => _selectedProfile;
            set => SetProperty( ref _selectedProfile, value );
        }

        public ServerData SelectedServer
        {
            get => _selectedServer;
            set => SetProperty( ref _selectedServer, value );
        }

        /// <summary>
        /// Clicking Start
        /// </summary>
        /// <param name="param"></param>
        public void ClickStart( object param ) => DialogHost.Show( new Dialogs.ProgressDialog( ), OnClickStart );
        private void OnClickStart( object sender, DialogOpenedEventArgs eventArgs )
        {
            _profile.Local = SelectedProfile;
            _server.Load( SelectedProfile.Server );
            App.Instance.Start( );
        }

        /// <summary>
        /// Clicking Add Profile
        /// </summary>
        /// <param name="param"></param>
        public void ClickAdd( object param ) => DialogHost.Show( new Dialogs.AddProfileDialog( ), OnClickAdd );
        private void OnClickAdd( object sender, DialogClosingEventArgs eventArgs )
        {
            //=> Pressed Cancel
            if( Equals( eventArgs.Parameter, false ) )
            {
                return;
            }

            Debug.WriteLine( "Selected Server: " + SelectedServer.Id );
            Debug.WriteLine( "Selected Player: " + Username );

            //eventArgs.Cancel( ); //=> Cancel Close
            //eventArgs.Session.UpdateContent( new Dialogs.ProgressDialog( ) );
            App.Instance.Profile.Add( Username, SelectedServer.Id );
            App.Instance.Profile.Save( );
            UpdateProperty( "ProfileList" );
            //Application.Current.Dispatcher.BeginInvoke( DispatcherPriority.Normal, new Action( ( ) => eventArgs.Session.Close( ) ) );
        }

        /// <summary>
        /// Clicking del Profile on current selected profile
        /// </summary>
        /// <param name="param"></param>
        public void ClickDel( object param )
        {
            Debug.Assert( SelectedProfile is ProfileData );

            App.Instance.Profile.Remove( SelectedProfile );
            App.Instance.Profile.Save( );
            UpdateProperty( "ProfileList" );
        }
    }
}
