using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Venom.Helpers
{
    public static class MvvmBehaviors
    {
        public static string GetLoadedMethodName( DependencyObject obj )
        {
            return ( string )obj.GetValue( LoadedMethodNameProperty );
        }

        public static void SetLoadedMethodName( DependencyObject obj, string value )
        {
            obj.SetValue( LoadedMethodNameProperty, value );
        }

        // Using a DependencyProperty as the backing store for LoadedMethodName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached( "LoadedMethodName", typeof( string ), typeof( MvvmBehaviors ), new PropertyMetadata( null, OnLoadedMethodNameChanged ) );



        private static void OnLoadedMethodNameChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if( d is FrameworkElement element )
            {
                element.Loaded += ( s, ev ) =>
                {
                    var viewModel = element.DataContext;
                    if( viewModel != null )
                    {
                        return;
                    }

                    var methodInfo = viewModel.GetType( ).GetMethod( e.NewValue.ToString( ) );
                    methodInfo?.Invoke( viewModel, null );
                };
            }
        }
    }
}
