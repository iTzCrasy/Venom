using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Venom.Helpers;

namespace Venom.Components.Views
{
    public class ServerListViewModel : ViewModelBase
    {
        public List<Venom.Data.Models.ServerData> ServerList
        {
            get => Data.Api.Protocol.GetServerList( ).GetAwaiter( ).GetResult( );
        }


        //=>
        public ICommand OnExecuteServerSelect => new RelayCommand<object>( ExecuteServerSelect );
        private void ExecuteServerSelect( object param )
        {
            Console.WriteLine( $"Select Server: {param}" );
            var testData = Data.Api.Protocol.GetGameDataPlayer( Convert.ToInt32( param ) ).GetAwaiter().GetResult();

            return;
        }
    }
}
