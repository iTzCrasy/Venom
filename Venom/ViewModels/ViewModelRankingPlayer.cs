using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Venom.Domain;

using Venom.Game;
using Venom.Game.Resources;


namespace Venom.ViewModels
{
    public class ViewModelRankingPlayer : NotifyPropertyChangedExt
    {
        private readonly ResourceHandler _resourceHandler;

        private string _filterString;
        private ICollectionView _playerCollection;

        public ViewModelRankingPlayer( ResourceHandler resourceHandler )
        {
            _resourceHandler = resourceHandler;

            PlayerCollection = CollectionViewSource.GetDefaultView( _resourceHandler.CreatePlayerRanking( ) );
            PlayerCollection.Filter = new Predicate<object>( Filter );
        }

        public string FilterString
        {
            get => _filterString;
            set
            {
                SetProperty( ref _filterString, value );
                FilterCollection( );
            }
        }

        public ICollectionView PlayerCollection
        {
            get => _playerCollection;
            set => SetProperty( ref _playerCollection, value, "PlayerCollection" );
        }

        private void FilterCollection( )
        {
            if( _playerCollection != null )
            {
                Task.Factory.StartNew( ( ) =>
                {
                    App.Current.Dispatcher.Invoke( new Action( () => _playerCollection.Refresh( ) ) );
                } );
            }
        }

        private bool Filter( object obj )
        {
            if( string.IsNullOrEmpty( _filterString ) || string.IsNullOrWhiteSpace( _filterString ) )
            {
                return true;
            }
            return obj.ToString( ).ToLower().Contains( _filterString.ToLower() );
        }
    }
}

