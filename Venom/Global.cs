using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.Windsor;

namespace Venom
{
    internal static class Global
    {
        private static readonly WindsorContainer _Container;

        static Global()
        {
            _Container = new WindsorContainer( );
        }

        public static void Initialize()
        {
            //=> Setup Windows
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Windows.StartWindow>( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Windows.MainWindow>( ) );

            //=> Setup Views
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.TroupList>( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.RankingPlayerView>( ) );
            _Container.Register( Castle.MicroKernel.Registration.Component.For<Views.RankingAllyView>() );
        }

        public static void Shutdown()
        {
            //=> TODO: Implement save here!
            System.Windows.Application.Current.Shutdown( );
        }

        public static void Start()
        {
            WindowStart.Show( );    //=> Show first window
        }

        public static void StartVenom()
        {
            //=> TODO: Implement loading here!

            WindowStart.Close( );   //=> Loading finished, close main window
            WindowMain.Show( );     //=> Show main window
        }

        public static Windows.StartWindow WindowStart =>
            _Container.Resolve<Windows.StartWindow>( );

        public static Windows.MainWindow WindowMain =>
            _Container.Resolve<Windows.MainWindow>( );

        public static Views.TroupList ViewTroupList =>
            _Container.Resolve<Views.TroupList>( );
        public static Views.RankingPlayerView ViewRankingPlayer =>
            _Container.Resolve<Views.RankingPlayerView>( );
        public static Views.RankingAllyView ViewRankingAlly =>
            _Container.Resolve<Views.RankingAllyView>( );
    }
}
