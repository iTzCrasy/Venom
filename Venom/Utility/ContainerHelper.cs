using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venom.Data;
using Venom.Repositories;
using Venom.Components.ViewModels;

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
                //builder.AddFile( "Logs/Venom-{Date}.txt", isJson: true );
            } );


            _container.AddSingleton<DataContext>( );

            //=> Network
            _container.AddScoped<Data.Api.IApiClient, Data.Api.ApiClient>( );

            //=> View Models
            _container.AddTransient<MainViewModel>( );
            _container.AddTransient<ServerSelectionViewModel>( );

            //=> Repositories
            _container.AddScoped<IGameServerRepository, GameServerRepository>( );
            _container.AddScoped<IPlayerRepository, PlayerRepository>( );
            _container.AddScoped<IAllyRepository, AllyRepository>( );
            _container.AddScoped<IVillageRepository, VillageRepository>( );
            _container.AddScoped<IResourceRepository, ResourceRepository>( );


            Provider = _container.BuildServiceProvider( );
        }
    }
}
