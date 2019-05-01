using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Venom.Game;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class ViewModelTroupList : NotifyPropertyChangedExt
    {
        private readonly Profile _profile;
        private readonly Server _server;
        private readonly ResourceHandler _resourceHandler;
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceVillage _resourceVillage;

        private ICollectionView _troupCollection;

        public ViewModelTroupList( 
            Profile profile,
            Server server,
            ResourcePlayer resourcePlayer,
            ResourceVillage resourceVillage,
            ResourceHandler resourceHandler )
        {
            _profile = profile;
            _server = server;
            _resourcePlayer = resourcePlayer;
            _resourceVillage = resourceVillage;
            _resourceHandler = resourceHandler;

            //TroupCollection = CollectionViewSource.GetDefaultView( _resourceVillage.GetVillagesByPlayer( _resourcePlayer.GetPlayerByName( _profile.Local.Name ) ).OrderBy( x => x.Id ) );
            TroupCollection = CollectionViewSource.GetDefaultView( _resourceHandler.CreateTroupList( ) );
        }

        public ICollectionView TroupCollection
        {
            get => _troupCollection;
            set => SetProperty( ref _troupCollection, value, "TroupCollection" );
        }

        public Visibility ArcherVisibility
        {
            get => _server.Local.Config.Archer.Equals( 1 ) ? Visibility.Visible : Visibility.Hidden;
        }

        public void Update()
        {
            TroupCollection = CollectionViewSource.GetDefaultView( _resourceHandler.CreateTroupList( ) );
            TroupCollection.Refresh( );
        }
    }
}

