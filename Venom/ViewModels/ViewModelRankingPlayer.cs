using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Venom.Domain;
using Venom.Game.Resources;


namespace Venom.ViewModels
{
    public class ViewModelRankingPlayer : NotifyPropertyChangedExt
    {
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourceBashpointPlayer _resourceBashpointPlayer;

        private string _filterString;
        private ICollectionView _playerCollection;

        public ViewModelRankingPlayer( 
            ResourcePlayer resourcePlayer,
            ResourceAlly resourceAlly,
            ResourceBashpointPlayer resourceBashpointPlayer )
        {
            _resourcePlayer = resourcePlayer;
            _resourceAlly = resourceAlly;
            _resourceBashpointPlayer = resourceBashpointPlayer;

            FillList( );
            //PlayerCollection = CollectionViewSource.GetDefaultView( _resourcePlayer.GetPlayerList( ).OrderByDescending( x => x.Points ));
            //PlayerCollection.Filter = new Predicate<object>( Filter );
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

        private bool FilterContains( string str, string substring )
        {
            if( str == null )
                return false;

            if( substring == null )
                return false;

            return str.ToLower().Contains( substring.ToLower() );
        }

        private bool Filter( object obj )
        {
            var data = obj as PlayerData;
            if( data != null )
            {
                if( !string.IsNullOrEmpty( _filterString ) )
                {
                    return FilterContains( data.Name, _filterString ) || FilterContains( data.AllyString, _filterString );
                }
                return true;
            }
            return false;
        }

        public void FillList( )
        {
            var playerList = _resourcePlayer.GetPlayerList( );
            var allyList = _resourceAlly.GetAllyList( );
            var bashAttList = _resourceBashpointPlayer.GetBashpointAttList( );

            var query = from p in playerList
                        join a in allyList on p.Ally equals a.Id
                        join att in bashAttList on p.Id equals att.Id
                        select new { p.Rank, p.Name, a.Tag, p.Villages, p.Points, p.PointsVillage, att.Kills };

            PlayerCollection = CollectionViewSource.GetDefaultView( query.OrderByDescending( x => x.Points ) );
        }
    }
}
