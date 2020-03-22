using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Venom.Helpers
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _targetExecuteMethod;
        private readonly Func<T, bool> _targetCanExecuteMethod;


        // Beware - should use weak references if command instance lifetime is longer
        // than lifetime of UI objects that get hooked up to command
        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged;


        public RelayCommand( Action<T> executeMethod )
        {
            _targetExecuteMethod = executeMethod;
        }

        public RelayCommand( Action<T> executeMethod, Func<T, bool> canExecuteMethod )
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }


        public void RaiseCanExecuteChanged( ) => CanExecuteChanged?.Invoke( this, EventArgs.Empty );

        public bool CanExecute( object parameter )
        {
            if( _targetCanExecuteMethod != null )
            {
                var tparam = ( T )parameter;
                return _targetCanExecuteMethod( tparam );
            }

            return _targetExecuteMethod != null;
        }


        public void Execute( object parameter )
        {
	        _targetExecuteMethod?.Invoke( ( T )parameter );
        }
    }
}
