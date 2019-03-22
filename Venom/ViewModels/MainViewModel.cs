using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Venom.Domain;
using Venom.Game;

namespace Venom.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedExt
    {
        private readonly Profile _profile;
        private readonly Server _server;

        public MainViewModel( 
            Profile profile,
            Server server )
        {
            _profile = profile;
            _server = server;

            MenuItems = new[]
            {
                new MainMenuItem( "Truppenliste", App.Instance.ViewTroupList, "/Venom;component/Assets/Images/map2.png" ),
                new MainMenuItem( "Rangliste Stämme", App.Instance.ViewRankingAlly, "/Venom;component/Assets/Images/unit_axe.png" ),
                new MainMenuItem( "Rangliste Spieler", App.Instance.ViewRankingPlayer, "/Venom;component/Assets/Images/unit_snob.png" ),
            };
        }

        public string CurrentUser
        {
            get => _profile.Local.Name;
        }

        public MainMenuItem[] MenuItems { get; }
}

    public class MainMenuItem : NotifyPropertyChangedExt
    {
        private string _title;
        private object _content;
        private string _image;
        public MainMenuItem( string title, object content, string image )
        {
            Title = title;
            Content = content;
            Image = image;
        }

        public string Title
        {
            get => _title;
            set => SetProperty( ref _title, value );
        }

        public object Content
        {
            get => _content;
            set => SetProperty( ref _content, value );
        }

        public string Image
        {
            get => _image;
            set => SetProperty( ref _image, value );
        }
    }
}
