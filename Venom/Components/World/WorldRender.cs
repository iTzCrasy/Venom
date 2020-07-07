using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using Venom.Utility;

namespace Venom.Components.World
{
    public class WorldRender : WorldRenderHelper
    {
        #region Properties
        public SharpDX.Direct2D1.Factory _factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory _factoryWrite { get; private set; }
        public WindowRenderTarget _renderTarget { get; private set; }
        public Bitmap[] _images;
        #endregion

        #region Defaults
        //=> Default Text Format
        private TextFormat _defaultTextFormat;
        private SharpDX.Direct2D1.SolidColorBrush _defaultMarkerBrush;
        private SharpDX.Direct2D1.SolidColorBrush _defaultMarkerOutlineBrush;
        #endregion

        public bool Initialize( IntPtr H, Size2 S )
        {
            _factory2D = new SharpDX.Direct2D1.Factory( );
            _factoryWrite = new SharpDX.DirectWrite.Factory( );

            var properties = new HwndRenderTargetProperties
            {
                Hwnd = H,
                PixelSize = S,
                PresentOptions = PresentOptions.Immediately
            };

            _renderTarget = new WindowRenderTarget( _factory2D, new RenderTargetProperties( ), properties )
            {
                AntialiasMode = AntialiasMode.PerPrimitive,
                TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype
            };

            _images = new Bitmap[]
{
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v1.png" ),
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v2.png" ),
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v3.png" ),
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v4.png" ),
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v5.png" ),
                LoadFromFile( _renderTarget, $"Resource\\Images\\Villages\\Version 1\\v6.png" ),
            };

            _defaultTextFormat = new TextFormat( _factoryWrite, "", SharpDX.DirectWrite.FontWeight.Regular, 0, 12 );
            _defaultMarkerBrush = new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 1.0f, 0.0f, 0.0f ), 1.0f ) );
            _defaultMarkerOutlineBrush = new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 0.0f, 0.0f, 0.0f ), 1.0f ) );

            CompositionTarget.Rendering += OnRender;

            return true;
        }

        public void Resize( Size2 S )
        {
            _renderTarget.Resize( S );
        }

        protected void OnRender( object sender, EventArgs e )
        {
            CalculateFramerate( );

            _renderTarget.BeginDraw( );
            _renderTarget.Clear( new RawColor4( 255, 255, 255, 255 ) );

            DrawVillages( );


            DrawDebugInfo( );

            _renderTarget.EndDraw( );
        }

        /// <summary>
        /// Render Villages on the Map.
        /// </summary>
        private void DrawVillages()
        {
            for( int x = 0; x != 40; x++ )
            {
                for( int y = 0; y != 40; y++ )
                {
                    var image = _images[3];

                    var RenderX = x * image.PixelSize.Width;
                    var RenderY = y * image.PixelSize.Height;

                    //=> TODO: Draw Decoration if no village is there
                    _renderTarget.DrawBitmap( image, new RectangleF( RenderX, RenderY, image.PixelSize.Width, image.PixelSize.Height ), 1.0f, BitmapInterpolationMode.Linear );

                    //=> TODO: Select Village Marker!
                    var villageMarker = new Ellipse( new RawVector2( RenderX + 4, RenderY + 4 ), 4, 4 );
                    _renderTarget.FillEllipse( villageMarker, _defaultMarkerBrush );
                    _renderTarget.DrawEllipse( villageMarker, _defaultMarkerOutlineBrush );
                }
            }
        }

        /// <summary>
        /// Render some Debug Informations (FPS,..)
        /// </summary>
        private void DrawDebugInfo()
        {
            //=> FPS
            var fps = GetFramerate( );
            _renderTarget.FillRectangle( new RawRectangleF( 0, 0, 100, 25 ), new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 0.0f, 0.0f, 0.0f ), 0.8f ) ) );
            _renderTarget.DrawText( $"Framerate:", _defaultTextFormat, new RectangleF( 2, 2, 100, 12 ), new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 1.0f, 1.0f, 1.0f ), 0.8f ) ) );
            if( fps >= 30 )
            {
                _renderTarget.DrawText( $"{fps}", _defaultTextFormat, new RectangleF( 65, 2, 100, 12 ), new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 0.0f, 1.0f, 0.0f ), 0.8f ) ) );
            }
            else
            {
                _renderTarget.DrawText( $"{fps}", _defaultTextFormat, new RectangleF( 65, 2, 100, 12 ), new SharpDX.Direct2D1.SolidColorBrush( _renderTarget, new Color4( new Color3( 1.0f, 0.0f, 0.0f ), 0.8f ) ) );
            }
        }


        //=> TODO: Remove & Rework!!
        private Bitmap LoadFromFile( RenderTarget renderTarget, string file )
        {
            using( var bitmap = ( System.Drawing.Bitmap )System.Drawing.Image.FromFile( file ) )
            {
                var sourceArea = new System.Drawing.Rectangle( 0, 0, bitmap.Width, bitmap.Height );
                var bitmapProperties = new BitmapProperties( new SharpDX.Direct2D1.PixelFormat( Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied ) );
                var size = new SharpDX.Size2( bitmap.Width, bitmap.Height );

                int stride = bitmap.Width * sizeof( int );
                using( var tempStream = new DataStream( bitmap.Height * stride, true, true ) )
                {
                    var bitmapData = bitmap.LockBits( sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb );

                    for( int y = 0; y < bitmap.Height; y++ )
                    {
                        int offset = bitmapData.Stride * y;
                        for( int x = 0; x < bitmap.Width; x++ )
                        {
                            byte B = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte G = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte R = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte A = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            int rgba = R | ( G << 8 ) | ( B << 16 ) | ( A << 24 );
                            tempStream.Write( rgba );
                        }
                    }
                    bitmap.UnlockBits( bitmapData );
                    tempStream.Position = 0;

                    return new Bitmap( renderTarget, size, tempStream, stride, bitmapProperties );
                }
            }
        }
    }
}
