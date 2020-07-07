using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Venom.Components.World;

namespace Venom.Components.Views
{
    /// <summary>
    /// Interaction logic for WorldView.xaml
    /// </summary>
    public partial class WorldView : System.Windows.Controls.UserControl
    {
        public SharpDX.Direct2D1.Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }

        public Bitmap[] _Image;

        private float viewX = 0.0f;
        private float viewY = 0.0f;

        private bool mouseMoving = false;
        private WorldRender _worldRender = null;

        public WorldView( )
        {
            InitializeComponent( );
            DataContext = new WorldViewModel( );

            RenderZone.Paint += RenderControlPaint;
            RenderZone.Resize += RenderControlResize;
            //RenderZone.MouseDown += MouseDown;
            //RenderZone.MouseMove += MouseMove;
            //RenderZone.MouseUp += MouseUp;

            _worldRender = new WorldRender( );
            _worldRender.Initialize( RenderZone.Handle, new Size2( RenderZone.ClientSize.Width, RenderZone.ClientSize.Height ) );
        }

        ~WorldView( )
        {
            RenderTarget2D.Dispose( );
        }

  
        private void MouseUp( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            Point mouseDownLocation = new Point( e.X, e.Y );

            string eventString = null;
            switch( e.Button )
            {
                case MouseButtons.Left:
                {
                    mouseMoving = false;
                }
                break;
                case MouseButtons.Right:
                    eventString = "R";
                    break;
                case MouseButtons.Middle:
                    eventString = "M";
                    break;
                case MouseButtons.XButton1:
                    eventString = "X1";
                    break;
                case MouseButtons.XButton2:
                    eventString = "X2";
                    break;
                case MouseButtons.None:
                default:
                    break;
            }
        }

        private void MouseDown( object sender, MouseEventArgs e )
        {
            Point mouseDownLocation = new Point( e.X, e.Y );

            string eventString = null;
            switch( e.Button )
            {
                case MouseButtons.Left:
                {
                    Debug.WriteLine( $"Mouse Clicked! {e.X / _Image[0].PixelSize.Width} | {e.Y / _Image[0].PixelSize.Height}" );
                    mouseMoving = true;
                }
                break;
                case MouseButtons.Right:
                    eventString = "R";
                    break;
                case MouseButtons.Middle:
                    eventString = "M";
                    break;
                case MouseButtons.XButton1:
                    eventString = "X1";
                    break;
                case MouseButtons.XButton2:
                    eventString = "X2";
                    break;
                case MouseButtons.None:
                default:
                    break;
            }
        }

        private void MouseMove( object sender, MouseEventArgs e )
        {
            //Point location = new Point( e.X, e.Y );

            //if( mouseMoving )
            //{
            //    Debug.WriteLine( $"Mouse Move!! {e.X / _Image[0].PixelSize.Width} | {e.Y / _Image[0].PixelSize.Height}" );

            //    viewX = e.X;
            //    viewY = e.Y;
            //}
        }

        private void RenderControlResize( object sender, System.EventArgs e )
        {
            _worldRender.Resize( new Size2( RenderZone.ClientRectangle.Size.Width, RenderZone.ClientRectangle.Size.Height ) );
        }

        void RenderControlPaint( object sender, PaintEventArgs e )
        {
        }
    }
}
