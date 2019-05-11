using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Castle.Windsor;
using Venom.Core;
using Venom.Windows;
using Venom.Domain;
using Venom.Views;
using Venom.ViewModels;
using Venom.Game;
using Venom.Game.Resources;
using System.Diagnostics;
using Venom.Style;
using Venom.Views.First;
using MahApps.Metro.Controls.Dialogs;
using Venom.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Venom
{
    public partial class App : Application
    {
        //=> Global Instance
        public static App Instance => ( App )Current;

        //=> Properties
        private readonly WindsorContainer _container = new WindsorContainer();


        public Profile CurrentProfile => _container.Resolve<Profile>( );

        public Server CurrentServer => _container.Resolve<Server>( );


        private readonly ServiceCollection _collection = new ServiceCollection( );



        //=> Windows
        public StartWindow WindowStart =>
            _container.Resolve<StartWindow>( );

        public MainWindow WindowMain =>
            _container.Resolve<MainWindow>( );

        //=> Domains
        public ClipboardHandler ClipboardHandler =>
            _container.Resolve<ClipboardHandler>( );
        public TrayIcon TrayIcon =>
            _container.Resolve<TrayIcon>( );

        //=> Game
        public Profile Profile =>
            _container.Resolve<Profile>( );
        public Server Server =>
            _container.Resolve<Server>( );

        //=> Views
        public SelectServerView ViewSelectServer =>
            _container.Resolve<SelectServerView>( );
        public ViewTroupList ViewTroupList =>
            _container.Resolve<ViewTroupList>( );
        public RankingPlayerView ViewRankingPlayer =>
            _container.Resolve<RankingPlayerView>( );
        public RankingAllyView ViewRankingAlly =>
            _container.Resolve<RankingAllyView>( );

        //=> ViewModels
        public ViewModelStart ViewModelStart =>
            _container.Resolve<ViewModelStart>( );

        //public MainViewModel ViewModelMain =>
        //    _container.Resolve<MainViewModel>( );

        public ViewModelTroupList ViewModelTroupList =>
            _container.Resolve<ViewModelTroupList>( );
        public ViewModelRankingPlayer ViewModelRankingPlayer =>
            _container.Resolve<ViewModelRankingPlayer>( );
        public ViewModelRankingAlly ViewModelRankingAlly =>
            _container.Resolve<ViewModelRankingAlly>( );


        private void AppStartup( object sender, StartupEventArgs e )
		{
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //=> Setup Windows
            _container.Register( Castle.MicroKernel.Registration.Component.For<StartWindow>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<MainWindow>( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<LoadingWindow>( ) );

            //=> Setup Domains
            _container.Register( Castle.MicroKernel.Registration.Component.For<ClipboardHandler>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<TrayIcon>( ).LifestyleSingleton( ) );

            //=> Setup Game
            _container.Register( Castle.MicroKernel.Registration.Component.For<Profile>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<Server>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<GroupHandler>( ).LifestyleSingleton( ) );

            //=> Setup Views
            _container.Register( Castle.MicroKernel.Registration.Component.For<ServerSelection>( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<SelectServerView>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<RankingPlayerView>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<RankingAllyView>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewTroupList>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ConquerView>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewStart>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewPlaner>( ).LifestyleSingleton( ) );

            //=> Setup ViewModels
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelStart>( ).LifestyleSingleton( ) );


            // _container.Register( Castle.MicroKernel.Registration.Component.For<MainViewModel>( ) );


            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelRankingPlayer>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelRankingAlly>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelTroupList>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelConquer>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelHome>( ).LifestyleSingleton( ) );

            //=> Setup Resources
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceHandler>( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourcePlayer>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceAlly>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceBashpointAlly>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceBashpointPlayer>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceVillage>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceConquer>( ).LifestyleSingleton( ) );
            _container.Register( Castle.MicroKernel.Registration.Component.For<ResourceTroup>( ).LifestyleSingleton( ) );

            _container.Register( Castle.MicroKernel.Registration.Component.For<IDialogCoordinator>().ImplementedBy<DialogCoordinator>() );

            //=> Loading Server & Profiles
            Profile.Load( );
            Server.Load( );

            //WindowStart.Show( );

            ResourceManager.GetInstance.Initialize( );

            _container.Resolve<Profile>( ).Local = new ProfileData { Name = "Moralbasher", Server = "de161" };
            _container.Resolve<Server>( ).Load( "de161" );

            Start( );

            var dlgServer = new SelectServer( );
            dlgServer.ShowDialog( );



        }

        public async void Start()
        {
            var watch = new Stopwatch( );
            watch.Start( );

            var taskList = new List<Task>
            {
                _container.Resolve<ResourcePlayer>( ).InitializeAsync( ),   //=> Loading Player Resources
                _container.Resolve<ResourceAlly>( ).InitializeAsync( ),     //=> Loading Ally Resources
                _container.Resolve<ResourceVillage>( ).InitializeAsync( ),  //=> Loading Village Resources
                _container.Resolve<ResourceConquer>( ).InitializeAsync( ),  //=> Loading Conquer Resources
                _container.Resolve<ResourceBashpointAlly>( ).InitializeAsync( ),    //=> Loading Bashpoint Ally Resources
                _container.Resolve<ResourceBashpointPlayer>( ).InitializeAsync( ),  //=> Loading Bashpoint Player Resources

                _container.Resolve<ResourceTroup>( ).Load( ) //=> Loading Troup Saves
            };

            await Task.WhenAll( taskList );

            watch.Stop( );

            TrayIcon.ShowInfo( "Welcome to Venom!", "Loading finished in " + watch.ElapsedMilliseconds + "ms" );

            //WindowStart.Close( );   //=> Loading finished, close main window
            // WindowMain.Show( );     //=> Show main window
   
        }

        public new async void Shutdown()
        {
            var taskList = new List<Task>
            {
                _container.Resolve<ResourceTroup>( ).Save( )
            };

            await Task.WhenAll( taskList );

            Current.Shutdown( );
        }
    }
}
