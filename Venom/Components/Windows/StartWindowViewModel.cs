using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Helpers;

namespace Venom.Components.Windows
{
    public class StartWindowViewModel : ViewModelBase
    {
        private object _contentView = "";

        public object ContentView
        {
            get => _contentView;
            set => SetProperty( ref _contentView, value );
        }

        //=> Window Loaded!
        public async Task WindowLoaded()
        {
            ContentView = new Views.ViewSearchingUpdates( );
            await Task.Delay( 3000 ).ConfigureAwait( true );
            ContentView = new Views.ViewPlayerSelection( );
        }
    }
}
