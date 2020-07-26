using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Venom.Helpers
{
    public static class MvvmBehaviors
    {
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached( "LoadedMethodName", 
                typeof( string ), typeof( MvvmBehaviors ), new PropertyMetadata( null, OnLoadedMethodNameChanged ) );


        public static string GetLoadedMethodName( DependencyObject obj )
        {
            return ( string )obj.GetValue( LoadedMethodNameProperty );
        }

        public static void SetLoadedMethodName( DependencyObject obj, string value )
        {
            obj.SetValue( LoadedMethodNameProperty, value );
        }


        /// <summary>
        /// Bypass Loaded Method 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLoadedMethodNameChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if( d is FrameworkElement element )
            {
                element.Loaded += ( s, ev ) =>
                {
                    var viewModel = element.DataContext;
                    if( viewModel == null )
                    {
                        throw new Exception( $"Failed to find ViewModel {e}!" );
                    }
                    var methodInfo = viewModel.GetType( ).GetMethod( e.NewValue.ToString( ) );
                    if( methodInfo == null )
                    {
                        throw new Exception( $"Failed to find Method {e.NewValue.ToString( )}!" );
                    }
                    methodInfo?.Invoke( viewModel, null );
                };
            }
        }
    }
}
