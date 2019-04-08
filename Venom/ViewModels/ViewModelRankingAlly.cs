using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Venom.Domain;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class ViewModelRankingAlly : NotifyPropertyChangedExt
    {
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourceBashpointAlly _resourceBashpointAlly;

        private string _filterString;
        private ICollectionView _allyCollection;

        public ViewModelRankingAlly( 
            ResourceAlly resourceAlly,
            ResourceBashpointAlly resourceBashpointAlly )
        {
            _resourceAlly = resourceAlly;
            _resourceBashpointAlly = resourceBashpointAlly;

            //AllyCollection = CollectionViewSource.GetDefaultView( _resourceAlly.GetAllyList( ).OrderByDescending( x => x.Points ) );
            //AllyCollection.Filter = new Predicate<object>( Filter );
            FillList( );
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

        public void FillList()
        {
            var allyList = _resourceAlly.GetAllyList( );
            var bashList = _resourceBashpointAlly.GetBashpointAllList( );

            var testQuery = from a in allyList
                            join ba in bashList on a.Id equals ba.Value.Id
                            select new { a.Id, a.Points, a.Tag, ba.Value.Kills };

            var query = allyList.Join( bashList,
                x => x.Id,
                y => y.Value.Id,
                ( x, y ) => new { x.Name, x.AllPoints, y.Value.Kills } );

            AllyCollection = CollectionViewSource.GetDefaultView( testQuery.OrderByDescending( x => x.Points ) );
        }
    }
}

//[TestMethod]
//public void TestJoinData( )
//{
//    var res = from o in Orders
//              join Employee e in Employees
//                  on o.EmpId equals e.EmpId
//              join Book b in Books
//                  on o.BookId equals b.BookId
//              orderby e.EmpId
//              select new { o.OrderId, e.Name, b.Title, b.Price };

//    Assert.AreEqual( "Test1", res.First( ).Name );

//}
