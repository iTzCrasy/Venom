using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class ViewModelConquer : NotifyPropertyChangedExt
    {
        private readonly ResourceConquer _resourceConquer;
        private readonly ResourcePlayer _resourcePlayer;

        private ICollectionView _conquerCollection;

        public ViewModelConquer( 
            ResourceConquer resourceConquer,
            ResourcePlayer resourcePlayer )
        {
            _resourceConquer = resourceConquer;
            _resourcePlayer = resourcePlayer;

            ConquerCollection = CollectionViewSource.GetDefaultView( _resourceConquer.GetConquerList().OrderByDescending( x => x.Time ) );
            //ConquerCollection.Filter = new Predicate<object>( Filter );
        }

        public ICollectionView ConquerCollection
        {
            get => _conquerCollection;
            set => SetProperty( ref _conquerCollection, value, "ConquerCollection" );
        }
    }
}
