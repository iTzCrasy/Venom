using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Venom.Domain;

using Venom.Game;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class ViewModelRankingAlly : NotifyPropertyChangedExt
    {
        private readonly ResourceHandler _resourceHandler;

        private string _filterString;
        private ICollectionView _allyCollection;

        public ViewModelRankingAlly( ResourceHandler resourceHandler )
        {
            _resourceHandler = resourceHandler;

            AllyCollection = CollectionViewSource.GetDefaultView( _resourceHandler.CreateAllyRanking() );
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

        public ICollectionView AllyCollection
        {
            get => _allyCollection;
            set => SetProperty( ref _allyCollection, value, "AllyCollection" );
        }

        private void FilterCollection()
        {
            if( _allyCollection != null )
            {
                _allyCollection.Refresh( );
            }
        }

        private bool Filter( object obj )
        {
            var data = obj as AllyData;
            if( data != null )
            {
                if( !string.IsNullOrEmpty( _filterString ) )
                {
                    return data.Name.Contains( _filterString ) || data.Tag.Contains( _filterString );
                }
                return true;
            }
            return false;
        }
    }
}
