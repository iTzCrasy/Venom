using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fluent;
using Venom.Domain;

namespace Venom.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Instance.ViewModelMain;
        }

        public ICommand TestCommand => new CommandExt( _ => Debug.WriteLine( "Test" ) ); // MainContent.Content = new MainViewStatsRankPlayer()

        public RibbonTitleBar TitleBar
        {
            get => ( RibbonTitleBar )GetValue( TitleBarProperty );
            private set => SetValue( TitleBarPropertyKey, value );
        }

        // ReSharper disable once InconsistentNaming
        private static readonly DependencyPropertyKey TitleBarPropertyKey = DependencyProperty.RegisterReadOnly( nameof( TitleBar ), typeof( RibbonTitleBar ), typeof( MainWindow ), new PropertyMetadata( ) );

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TitleBar"/>.
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;
    }
}
