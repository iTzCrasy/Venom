using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using Venom.Domain;
using Venom.Game;
using Venom.Views;

namespace Venom.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedExt
    {
        private readonly Profile _profile;
        private readonly Server _server;

        private ICollectionView _menuCollection;

        public MainViewModel( 
            Profile profile,
            Server server,
            ViewStart viewStart,
            ViewTroupList viewTroupList,
            RankingPlayerView viewRankingPlayer,
            RankingAllyView viewRankingAlly,
            ConquerView viewConquer,
            ViewPlaner viewPlaner )
        {
            _profile = profile;
            _server = server;

            var MenuItems = new[]
            {
                new MainMenuItem()
                {
                    Group = "Start",
                    Title = "Start",
                    Image = "",
                    Content = viewStart
                },
                new MainMenuItem()
                {
                    Group = "",
                    Title = "Truppenliste",
                    Image = "",
                    Content = viewTroupList
                },
                new MainMenuItem()
                {
                    Group = "Ranglisten",
                    Title = "Rangliste Spieler",
                    Image = "",
                    Content = viewRankingPlayer
                },
                new MainMenuItem()
                {
                    Group = "Ranglisten",
                    Title = "Rangliste Stämme",
                    Image = "",
                    Content = viewRankingAlly
                },
                new MainMenuItem()
                {
                    Group = "Ranglisten",
                    Title = "Eroberungen",
                    Image = "/Venom;component/Assets/Images/unit_snob.png",
                    Content = viewConquer
                },
                new MainMenuItem()
                {
                    Group = "Planer",
                    Title = "Angriffsplaner",
                    Image = "",
                    Content = viewPlaner
                }
            }; 

            MenuCollection = CollectionViewSource.GetDefaultView( MenuItems );
            MenuCollection.GroupDescriptions.Add( new PropertyGroupDescription( "Group" ) );
        }

        public ICollectionView MenuCollection
        {
            get => _menuCollection;
            set => SetProperty( ref _menuCollection, value );
        }
    }

    public class MainMenuItem : NotifyPropertyChangedExt
    {
        private string _title;
        private object _content;
        private string _image;
        private string _group;

        public string Group
        {
            get => _group;
            set => SetProperty( ref _group, value );
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
