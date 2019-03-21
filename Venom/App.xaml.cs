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

            //=> Setup Domains
            _Container.Register( Castle.MicroKernel.Registration.Component.For<ClipboardHandler>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<TrayIcon>( ).LifestyleSingleton( ) );

            //=> Setup Game
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Profile>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Server>( ).LifestyleSingleton( ) );

            //=> Setup Views
            _Container.Register( Castle.MicroKernel.Registration.Component.For<SelectServerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingPlayerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingAllyView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<TroupList>( ).LifestyleSingleton( ) );

            //=> Setup ViewModels
            _Container.Register( Castle.MicroKernel.Registration.Component.For<StartViewModel>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<MainViewModel>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingPlayerViewModel>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<RankingAllyViewModel>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<TroupListViewModel>( ).LifestyleSingleton( ) );

            //=> Setup Resources
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

            WindowStart.Show( );

            ResourceManager.GetInstance.Initialize( );
        }

        public async void Start()
        {
            var Watch = new Stopwatch( );
            Watch.Start( );

            var resources = new IResource[]
            {
                ResourcePlayer,
                ResourceAlly,
                ResourceVillage,
                ResourceConquer,
            };

            var taskList = new List<Task>( );
            foreach( var i in resources )
            {
                taskList.Add( i.InitializeAsync() );
            }

            await Task.WhenAll( taskList );

            var bashpoints = new IResource[]
            {
                ResourceBashpointAlly,
                ResourceBashpointPlayer,
            };

            var taskListBashpoints = new List<Task>( );
            foreach( var i in bashpoints )
            {
                taskListBashpoints.Add( i.InitializeAsync() );
            }

            await Task.WhenAll( taskListBashpoints );

            Watch.Stop( );

            TrayIcon.ShowInfo( "Welcome to Venom!", "Loading finished in " + Watch.ElapsedMilliseconds + "ms" );

            WindowStart.Close( );   //=> Loading finished, close main window
            WindowMain.Show( );     //=> Show main window
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
        public TroupList ViewTroupList =>
            _Container.Resolve<TroupList>( );
        public RankingPlayerView ViewRankingPlayer =>
            _Container.Resolve<RankingPlayerView>( );
        public RankingAllyView ViewRankingAlly =>
            _Container.Resolve<RankingAllyView>( );

        //=> ViewModels
        public StartViewModel ViewModelStart =>
            _Container.Resolve<StartViewModel>( );
        public MainViewModel ViewModelMain =>
            _Container.Resolve<MainViewModel>( );
        public TroupListViewModel ViewModelTroupList =>
            _Container.Resolve<TroupListViewModel>( );
        public RankingPlayerViewModel ViewModelRankingPlayer =>
            _Container.Resolve<RankingPlayerViewModel>( );
        public RankingAllyViewModel ViewModelRankingAlly =>
            _Container.Resolve<RankingAllyViewModel>( );

        //=> Resources
        public ResourcePlayer ResourcePlayer =>
            _Container.Resolve<ResourcePlayer>( );
        public ResourceAlly ResourceAlly =>
            _Container.Resolve<ResourceAlly>( );
        public ResourceBashpointAlly ResourceBashpointAlly =>
            _Container.Resolve<ResourceBashpointAlly>( );
        public ResourceBashpointPlayer ResourceBashpointPlayer =>
            _Container.Resolve<ResourceBashpointPlayer>( );
        public ResourceVillage ResourceVillage =>
            _Container.Resolve<ResourceVillage>( );
        public ResourceConquer ResourceConquer =>
            _Container.Resolve<ResourceConquer>( );
    }
}
