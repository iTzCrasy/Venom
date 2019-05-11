using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Venom.Domain
{
    public class CommandExt : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;


        public CommandExt( Action<object> execute ) 
            : this( execute, null )
        {
        }

        public CommandExt( Action<object> Execute, Func<object, bool> CanExecute )
        {
            _execute = Execute ?? throw new ArgumentNullException( nameof( Execute ) );
            _canExecute = CanExecute ?? ( x => true );
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }



        public void Refresh( ) => CommandManager.InvalidateRequerySuggested( );

        public bool CanExecute( object Parameter ) => _canExecute( Parameter );

        public void Execute( object Parameter ) => _execute( Parameter );
    }
}
