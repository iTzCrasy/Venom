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
        public CommandExt( Action<object> Execute ) : this( Execute, null )
        {
        }

        public CommandExt( Action<object> Execute, Func<object, bool> CanExecute )
        {
            _Execute = Execute ?? throw new ArgumentNullException( nameof( Execute ) );
            _CanExecute = CanExecute ?? ( x => true );
        }

        public bool CanExecute( object Parameter ) => _CanExecute( Parameter );
        public void Execute( object Parameter ) => _Execute( Parameter );

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

        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
    }
}
