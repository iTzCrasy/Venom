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


        public Profile CurrentProfile => null;

        public Server CurrentServer => null;



        //=> Windows

        public MainWindow WindowMain =>
            null;

        //=> Domains
        public ClipboardHandler ClipboardHandler =>
            null;
        public TrayIcon TrayIcon =>
            null;

        //=> Game
        public Profile Profile =>
            null;
        public Server Server =>
            null;

        //=> Views
        public ViewTroupList ViewTroupList =>
            null;
        public RankingPlayerView ViewRankingPlayer =>
            null;
        public RankingAllyView ViewRankingAlly =>
            null;

        //=> ViewModels
        public ViewModelStart ViewModelStart =>
            null;

        //public MainViewModel ViewModelMain =>
        //    _container.Resolve<MainViewModel>( );

        public ViewModelTroupList ViewModelTroupList =>
            null;
        public ViewModelRankingPlayer ViewModelRankingPlayer =>
            null;
        public ViewModelRankingAlly ViewModelRankingAlly =>
            null;



    }
}
