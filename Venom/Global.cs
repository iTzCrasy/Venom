using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.Windsor;
using Venom.Game.Resources;

namespace Venom
{
    internal static class Global
    {
        private static readonly WindsorContainer _Container;

        static Global( )
        {
            _Container = new WindsorContainer( );
        }

        public static void Initialize( )
        {
            //=> Setup Windows
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Windows.StartWindow>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Windows.MainWindow>( ).LifestyleSingleton( ) );

            //=> Setup Views
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.TroupList>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.RankingPlayerView>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.RankingAllyView>( ).LifestyleSingleton( ) );

            //=> Setup Resources
            _Container.Register( Castle.MicroKernel.Registration.Component.For<PlayerResource>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<AllyResource>( ).LifestyleSingleton( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<BashpointAllyResource>( )
                .LifestyleSingleton( )
                );


            _Container.Register( Castle.MicroKernel.Registration.Component.For<Core.Game>( )
                .LifestyleSingleton( )
                );

        }

        public static void Shutdown( )
        {
            //=> TODO: Implement save here!
            System.Windows.Application.Current.Shutdown( );
        }

        public static void Start( )
        {
            WindowStart.Show( );    //=> Show first window
        }

        public static async void StartVenom( )
        {
            var Watch = new Stopwatch( );
            Watch.Start( );

            //=> TODO: Implement loading here!
            var resources = new IResource[]
            {
                PlayerResource,
                AllyResource,
                BashpointAllyResource,
            };

            var taskList = new List<Task>( );
            foreach( var i in resources )
            {
                taskList.Add( i.InitializeAsync( Game.GetSelectedServer() ) );
            }

            await Task.WhenAll( taskList );

            Watch.Stop( );
            Debug.WriteLine( "New Time: " + Watch.ElapsedMilliseconds );

            WindowStart.Close( );   //=> Loading finished, close main window
            WindowMain.Show( );     //=> Show main window
        }

        //=> Windows
        public static Windows.StartWindow WindowStart =>
            _Container.Resolve<Windows.StartWindow>( );

        public static Windows.MainWindow WindowMain =>
            _Container.Resolve<Windows.MainWindow>( );

        //=> Views
        public static Views.TroupList ViewTroupList =>
            _Container.Resolve<Views.TroupList>( );
        public static Views.RankingPlayerView ViewRankingPlayer =>
            _Container.Resolve<Views.RankingPlayerView>( );
        public static Views.RankingAllyView ViewRankingAlly =>
            _Container.Resolve<Views.RankingAllyView>( );

        //=> Resources
        public static PlayerResource PlayerResource =>
            _Container.Resolve<PlayerResource>( );
        public static AllyResource AllyResource =>
            _Container.Resolve<AllyResource>( );
        public static BashpointAllyResource BashpointAllyResource => 
            _Container.Resolve<BashpointAllyResource>( );

        //=> Game
        public static Core.Game Game =>
            _Container.Resolve<Core.Game>( );

    }
}
