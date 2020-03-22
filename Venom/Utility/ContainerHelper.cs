using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venom.Components.Dialogs;
using Venom.Components.Views;
using Venom.Components.Windows;
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
            _container.AddSingleton<LocalStorage>( );



            _container.AddScoped<IGameServerRepository, GameServerRepository>( );
            _container.AddScoped<IPlayerRepository, PlayerRepository>( );
            _container.AddScoped<IAllyRepository, AllyRepository>( );
            _container.AddScoped<IVillageRepository, VillageRepository>( );


            //=> ViewModels
            _container.AddTransient<MainViewModel>( );
            _container.AddTransient<AddAccountViewModel>( );
            _container.AddTransient<AddEditAccountViewModel>( );
            _container.AddTransient<DialogLoadVenomViewModel>( );
            _container.AddTransient<ViewPlayerSelectionViewModel>( );

            //=> Start Window
            _container.AddTransient<Venom.Components.Windows.StartWindow>( );
            _container.AddTransient<Venom.Components.Windows.StartWindowViewModel>( );
            _container.AddTransient<Venom.Components.Windows.Start.StartViewModel>( );
            _container.AddTransient<Venom.Components.Windows.Start.Views.ViewLoading>( );
            _container.AddTransient<Venom.Components.Windows.Start.Views.ViewLogin>( );
            _container.AddTransient<Venom.Components.Windows.Start.Models.ViewModelLogin>( );


            Provider = _container.BuildServiceProvider( );
        }
    }
}
