using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Venom.ViewModels
{
    public class NotifyPropertyChangedExt : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>( ref T field, T newValue, [CallerMemberName]string propertyName = null )
        {
            if( EqualityComparer<T>.Default.Equals( field, newValue ) )
            {
                return;
            }

            field = newValue;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}
