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

        private bool FilterContains( string str, string substring )
        {
            if( str == null )
                return false;

            if( substring == null )
                return false;

            return str.ToLower().Contains( substring.ToLower() );
        }

        private static void GetPropertyValues( Object obj )
        {
            Type t = obj.GetType( );
            Console.WriteLine( "Type is: {0}", t.Name );
            PropertyInfo[] props = t.GetProperties( );
            Console.WriteLine( "Properties (N = {0}):",
                              props.Length );
            //foreach( var prop in props )
            //    if( prop.GetIndexParameters( ).Length == 0 )
            //        Console.WriteLine( "   {0} ({1}): {2}", prop.Name,
            //                          prop.PropertyType.Name,
            //                          prop.GetValue( obj ) );
            //    else
            //        Console.WriteLine( "   {0} ({1}): <Indexed>", prop.Name,
            //                          prop.PropertyType.Name );

        }

        private void GetPropertyValue( object obj, string prop )
        {
            obj.GetType( ).GetProperties( ).Single( p => p.Name.Equals( prop ) ).GetValue( obj );
        }

        private bool Filter( object obj )
        {
            var data = obj as PlayerData;
            if( obj != null )
            {
                if( !string.IsNullOrEmpty( _filterString ) )
                {
                    try
                    {
                        object test = "";
                        GetPropertyValue( obj, "Name" );
                        Console.WriteLine( $"Name --> {obj.ToString()}" );
                       
                    }
                    catch( NullReferenceException e )
                    {
                        Console.WriteLine( "MyProperty does not exist in MyClass." + e.Message );
                    }
                    //return FilterContains( GetPropertyValue( obj, "Name" ), _filterString ) || FilterContains( GetPropertyValue( obj, "AllyString" ).ToString(), _filterString );
                }
                return true;
            }
            return false;
        }
    }
}


/*
 *         try
        {
            // Get Type object of MyClass.
            Type myType=typeof(MyClass);       
            // Get the PropertyInfo by passing the property name and specifying the BindingFlags.
            PropertyInfo myPropInfo = myType.GetProperty("MyProperty", BindingFlags.Public | BindingFlags.Instance);
            // Display Name propety to console.
            Console.WriteLine("{0} is a property of MyClass.", myPropInfo.Name);
        }
        catch(NullReferenceException e)
        {
            Console.WriteLine("MyProperty does not exist in MyClass." +e.Message);
        }
*/
