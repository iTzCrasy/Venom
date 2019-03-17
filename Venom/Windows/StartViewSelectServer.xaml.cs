using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using Venom.Core;

namespace Venom.Windows
{
	/// <summary>
	/// Interaktionslogik für WelcomeView.xaml
	/// </summary>
	public partial class StartViewSelectServer : UserControl
	{
		public StartViewSelectServer( )
		{
			InitializeComponent( );

			Profiles.Load( );
			Userlist.ItemsSource = Profiles.GetProfileList( );
		}

		public string SwapClipboardHtmlText( string replacementHtmlText )
		{
			string returnHtmlText = null;
			if( Clipboard.ContainsText( TextDataFormat.Html ) )
			{
				returnHtmlText = Clipboard.GetText( TextDataFormat.Html );
				Clipboard.SetText( replacementHtmlText, TextDataFormat.Html );
			}
			return returnHtmlText;
		}

		private void ButtonAddProfileClick( object sender, RoutedEventArgs e )
		{
			DialogHost.Show( new Dialogs.AddProfileDialog( ), OnAddProfileAsync );
		}

		private async void OnAddProfileAsync( object sender, DialogClosingEventArgs eventArgs )
		{
			//=> Pressed Cancel
			if( !Equals( eventArgs.Parameter, true ) )
			{
				return;
			}

			var Dialog = ( Dialogs.AddProfileDialog )eventArgs.Session.Content;
			var SelectedServer = Dialog.Servers.Text.Trim( );
			var Username = Dialog.Username.Text.Trim( );

			//=> Check the fields
			if( string.IsNullOrEmpty( SelectedServer ) ||
				string.IsNullOrEmpty( Username ) )
			{
				//=> TODO: Handle error!
				return;
			}

			eventArgs.Cancel( ); //=> Cancel Close
			eventArgs.Session.UpdateContent( new Dialogs.ProgressDialog( ) );
   //         Core.Game.GetInstance.SetSelectedServer( SelectedServer );
			//await Core.Game.GetInstance.Load( false );

			//var Player = Core.Game.GetInstance.GetPlayer( Username );
			//if( Player.Equals( default( GamePlayers ) ) )
			//{
			//	Debug.WriteLine( "Player Not Valid" );
			//	eventArgs.Session.UpdateContent( Dialog );
			//	return;
			//}

			Profiles.AddProfile( Username, SelectedServer );

			Userlist.Items.Refresh( );
			Userlist.UpdateLayout( );
			Profiles.SaveProfiles( );

			Dispatcher.Invoke( ( ) => eventArgs.Session.Close( ) );
		}

		private void UserlistSelected( object sender, RoutedEventArgs e )
		{
			ButtonStart.IsEnabled = ( sender as ListView ).SelectedItem == null ? false : true;
		}

		private void ButtonStartClick( object sender, RoutedEventArgs e )
		{
			DialogHost.Show( new Dialogs.ProgressDialog( ), OnStartAsync );
		}

		private async void OnStartAsync( object sender, DialogOpenedEventArgs eventArgs )
		{
			Debug.Assert( Userlist.SelectedItem is Profile );

			var Profile = ( Profile )Userlist.SelectedItem;
            //Core.Game.GetInstance.SetSelectedServer( Profile.Server );
            //Core.Game.GetInstance.SetSelectedPlayer( Profile.Name );
            Global.Game.SetSelectedServer( Profile.Server );

			var Watch = new Stopwatch( );
			Watch.Start( );
			//await Core.Game.GetInstance.Load( true );
			Watch.Stop( );
			Debug.WriteLine( "Loaded Data Finished: " + Watch.ElapsedMilliseconds );

			await Map.GetInstance.Load( );

			//Dispatcher.Invoke( ( ) => eventArgs.Session.Close( ) );
            Global.StartVenom( );
		}

		private void DeleteUserClick( object sender, RoutedEventArgs e )
		{
			Debug.Assert( Userlist.SelectedItem is Profile );

			var Profile = ( Profile )Userlist.SelectedItem;
			Profiles.RemoveProfie( Profile.Name, Profile.Server );
			Profiles.SaveProfiles( );
			Userlist.Items.Refresh( );
			Userlist.UpdateLayout( );
		}
	}
}
