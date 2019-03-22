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

namespace Venom.Views
{
	/// <summary>
	/// Interaktionslogik für WelcomeView.xaml
	/// </summary>
	public partial class SelectServerView : UserControl
	{
		public SelectServerView( )
		{
			InitializeComponent( );
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

		private void Userlist_Selected( object sender, RoutedEventArgs e )
		{
			ButtonStart.IsEnabled = ( sender as ListView ).SelectedItem == null ? false : true;
		}

        private void Userlist_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
        }
    }
}
