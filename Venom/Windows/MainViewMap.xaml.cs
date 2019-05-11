using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using Venom.Core;
//using System.Drawing;

namespace Venom.Windows
{
    /// <summary>
    /// Interaktionslogik für MainViewMap.xaml
    /// </summary>
    public partial class MainViewMap : UserControl
    {
        public MainViewMap()
        {
            InitializeComponent();

            var Rand = new Random( );

            for( var X = 0; X != 1000; X += 50 )
            {
                for( var Y = 0; Y != 1000; Y += 50 )
                {
                    var Continent = new Canvas
                    {
                        Width = 250,
                        Height = 250
                    };

                    var TestImage = new Image
                    {
                        Source = new BitmapImage( new Uri( "https://de161.die-staemme.de/page.php?page=topo_image&player_id=undefined&village_id=null&x=" + X + "&y=" + Y + "&church=3&political=0&war=0&watchtower=0&key=1148549741&cur=null&focus=1992&local_cache=0" ) ),
                        Width = 250,
                        Height = 250
                    };

                    Continent.SetValue( Canvas.TopProperty, Y * 5.0 + 0.0 );    //=> Position Y
                    Continent.SetValue( Canvas.LeftProperty, X * 5.0 + 0.0 );   //=> Position X
                    Continent.SetValue( Canvas.BottomProperty, 0.0 );
                    Continent.SetValue( Canvas.RightProperty, 0.0 );

                    Continent.Children.Add( TestImage );
                    Continent.Background = Brushes.Red;

                    var myBorder = new Border( );
                    myBorder.BorderBrush = Brushes.Red;
                    myBorder.BorderThickness = new Thickness( 2 );
                    myBorder.Margin = new Thickness( 11, 1, 0, 0 ); 

                    var Coords = new TextBlock( );
                    Coords.Text = "Data: " + X + ", " + Y;


                    Continent.Children.Add( myBorder );
                    Continent.Children.Add( Coords );
                    Minimap.Children.Add( Continent );

                }            
            }
        }

        public void Load()
        {
            var Rand = new Random( );

            for( var Test = 0; Test != 10; Test++ )
            {
                var Sector = new Canvas( );
                var Village = Rand.Next( 1, 6 );
                Debug.WriteLine( "Random: " + Village );

                Sector.Width = 265;
                Sector.Height = 190;

                for( var X = 0; X != 5; X++ )
                {
                    for( var Y = 0; Y != 5; Y++ )
                    {
                        var ImageT = new Image
                        {
                            Source = Map.GetInstance.GetVillageImage( EVillageSize.S6 ).ImageFile
                        };

                        ImageT.Height = 38;
                        ImageT.Width = 53;
                        ImageT.SetValue( Canvas.TopProperty, X * 38.0 );
                        ImageT.SetValue( Canvas.LeftProperty, Y * 53.0 );
                        Sector.Children.Add( ImageT );
                    }
                }

                if( Test > 0 )
                {
                    Sector.SetValue( Canvas.TopProperty, Test * 190.0 );
                    Sector.SetValue( Canvas.LeftProperty, Test * 265.0 );
                }

                MapRender.Children.Add( Sector );
            }
        }

        private void Minimap_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            var MinimapCanvas = ( UIElement )sender;
            _MinimapMouse = e.GetPosition( MinimapCanvas );
            Debug.WriteLine( _MinimapMouse.ToString( ) );
            MinimapCanvas.CaptureMouse( );
        }

        private void Minimap_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            var MinimapCanvas = ( UIElement )sender;
            _MinimapMouse = null;
            MinimapCanvas.ReleaseMouseCapture( );
        }

        private void Minimap_MouseMove( object sender, MouseEventArgs e )
        {
            if( _MinimapMouse != null )
            {
                var MinimapCanvas = ( UIElement )sender;
                var MinimapPoint = e.GetPosition( Minimap );

                Debug.WriteLine( "Updated!" );
            }
        }

        protected Point? _MinimapMouse;
    }
}
