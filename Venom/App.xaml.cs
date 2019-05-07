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

namespace Venom
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        //=> Global Instance
        public static App Instance => ( App )Current;

        //=> Propertys
        private readonly WindsorContainer _Container = new WindsorContainer();

        private void AppStartup( object sender, StartupEventArgs e )
		{
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //=> Setup Windows
            _Container.Register( Castle.MicroKernel.Registration.Component.For<StartWindow>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<MainWindow>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<LoadingWindow>( ) );

            //=> Setup Domains
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ClipboardHandler>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<TrayIcon>( ).LifestyleSingleton( ) );

            //=> Setup Game
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Profile>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Server>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<GroupHandler>( ).LifestyleSingleton( ) );

            //=> Setup Views
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ServerSelection>( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<SelectServerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingPlayerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingAllyView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewTroupList>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ConquerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewStart>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewPlaner>( ).LifestyleSingleton( ) );

            //=> Setup ViewModels
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelStart>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<MainViewModel>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelRankingPlayer>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelRankingAlly>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelTroupList>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelConquer>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ViewModelHome>( ).LifestyleSingleton( ) );

            //=> Setup Resources
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceHandler>( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourcePlayer>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceAlly>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceBashpointAlly>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceBashpointPlayer>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceVillage>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceConquer>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ResourceTroup>( ).LifestyleSingleton( ) );

            //=> Loading Server & Profiles
            Profile.Load( );
            Server.Load( );

            //WindowStart.Show( );

            ResourceManager.GetInstance.Initialize( );

            _Container.Resolve<Profile>( ).Local = new ProfileData { Name = "Moralbasher", Server = "de161" };
            _Container.Resolve<Server>( ).Load( "de161" );
            _Container.Resolve<LoadingWindow>( ).Show( );
            //Start( );
        }

        public async void Start()
        {
            var Watch = new Stopwatch( );
            Watch.Start( );

            var taskList = new List<Task>( );
            taskList.Add( _Container.Resolve<ResourcePlayer>( ).InitializeAsync( ) );   //=> Loading Player Resources
            taskList.Add( _Container.Resolve<ResourceAlly>( ).InitializeAsync( ) );     //=> Loading Ally Resources
            taskList.Add( _Container.Resolve<ResourceVillage>( ).InitializeAsync( ) );  //=> Loading Village Resources
            taskList.Add( _Container.Resolve<ResourceConquer>( ).InitializeAsync( ) );  //=> Loading Conquer Resources
            taskList.Add( _Container.Resolve<ResourceBashpointAlly>( ).InitializeAsync( ) );    //=> Loading Bashpoint Ally Resources
            taskList.Add( _Container.Resolve<ResourceBashpointPlayer>( ).InitializeAsync( ) );  //=> Loading Bashpoint Player Resources

            taskList.Add( _Container.Resolve<ResourceTroup>( ).Load( ) ); //=> Loading Troup Saves

            await Task.WhenAll( taskList );

            Watch.Stop( );

            TrayIcon.ShowInfo( "Welcome to Venom!", "Loading finished in " + Watch.ElapsedMilliseconds + "ms" );

            //WindowStart.Close( );   //=> Loading finished, close main window
            WindowMain.Show( );     //=> Show main window
        }

        public new async void Shutdown()
        {
            var taskList = new List<Task>( );

            taskList.Add( _Container.Resolve<ResourceTroup>( ).Save( ) );

            await Task.WhenAll( taskList );

            Current.Shutdown( );
        }

        //=> Windows
        public StartWindow WindowStart =>
            _Container.Resolve<StartWindow>( );

        public MainWindow WindowMain =>
            _Container.Resolve<MainWindow>( );

        //=> Domains
        public ClipboardHandler ClipboardHandler =>
            _Container.Resolve<ClipboardHandler>( );
        public TrayIcon TrayIcon =>
            _Container.Resolve<TrayIcon>( );

        //=> Game
        public Profile Profile =>
            _Container.Resolve<Profile>( );
        public Server Server =>
            _Container.Resolve<Server>( );

        //=> Views
        public SelectServerView ViewSelectServer =>
            _Container.Resolve<SelectServerView>( );
        public ViewTroupList ViewTroupList =>
            _Container.Resolve<ViewTroupList>( );
        public RankingPlayerView ViewRankingPlayer =>
            _Container.Resolve<RankingPlayerView>( );
        public RankingAllyView ViewRankingAlly =>
            _Container.Resolve<RankingAllyView>( );

        //=> ViewModels
        public ViewModelStart ViewModelStart =>
            _Container.Resolve<ViewModelStart>( );
        public MainViewModel ViewModelMain =>
            _Container.Resolve<MainViewModel>( );
        public ViewModelTroupList ViewModelTroupList =>
            _Container.Resolve<ViewModelTroupList>( );
        public ViewModelRankingPlayer ViewModelRankingPlayer =>
            _Container.Resolve<ViewModelRankingPlayer>( );
        public ViewModelRankingAlly ViewModelRankingAlly =>
            _Container.Resolve<ViewModelRankingAlly>( );
    }
}
