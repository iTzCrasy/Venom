using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venom.Components.Dialogs;
using Venom.Data;
using Venom.Repositories;

namespace Venom.Utility
{
    public static class ContainerHelper
    {
        private static readonly ServiceCollection _container = new ServiceCollection( );

        private static readonly IServiceProvider _provider;


        public static IServiceProvider Provider
            => _provider;


#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ContainerHelper( )
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            _container.AddLogging( ( builder ) =>
            {
                builder.AddFile( "Logs/myapp-{Date}.txt", isJson: true );
            } );



            _container.AddSingleton<DataContext>( );


            _container.AddScoped<IGameServerRepository, GameServerRepository>( );


            // view models
            _container.AddTransient<AddEditAccountViewModel>( );





            _provider = _container.BuildServiceProvider( );
        }
    }
}
