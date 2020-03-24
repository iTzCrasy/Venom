using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venom.Components;
using Venom.Components.Windows;
using Venom.Data;
using Venom.Repositories;

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
            _container.AddSingleton<LocalStorage>( );

            //=> Repositories
            _container.AddScoped<IGameServerRepository, GameServerRepository>( );
            _container.AddScoped<IPlayerRepository, PlayerRepository>( );
            _container.AddScoped<IAllyRepository, AllyRepository>( );
            _container.AddScoped<IVillageRepository, VillageRepository>( );

            //=> Start Window
            _container.AddTransient<Components.Windows.Start.StartWindow>( );
            _container.AddTransient<Components.Windows.Start.StartViewModel>( );
            _container.AddTransient<Components.Windows.Start.Views.ViewLoading>( );
            _container.AddTransient<Components.Windows.Start.Views.ViewLogin>( );
            _container.AddTransient<Components.Windows.Start.Models.ViewModelLogin>( );

            //=> Main Window
            _container.AddTransient<Components.Windows.Main.MainViewModel>( );

            Provider = _container.BuildServiceProvider( );
        }
    }
}
