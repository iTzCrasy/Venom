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
using Venom.ViewModels;

namespace Venom.Utility
{
    public static class ContainerHelper
    {
        private static readonly ServiceCollection _container = new ServiceCollection( );

        public static IServiceProvider Provider { get; private set; }


        public static void PrepareContainer( )
        {
            _container.AddLogging( ( builder ) =>
            {
               builder.AddFile( "Logs/Venom-{Date}.txt", isJson: true );
            } );


            _container.AddSingleton<DataContext>( );
            _container.AddSingleton<Config>( );



            _container.AddScoped<IGameServerRepository, GameServerRepository>( );
            _container.AddScoped<IPlayerRepository, PlayerRepository>( );
            _container.AddScoped<IAllyRepository, AllyRepository>( );


            // view models
            _container.AddTransient<MainViewModel>( );
            _container.AddTransient<AddAccountViewModel>( );
            _container.AddTransient<AddEditAccountViewModel>( );
            _container.AddTransient<DialogLoadVenomViewModel>( );
           


            Provider = _container.BuildServiceProvider( );
        }
    }
}
