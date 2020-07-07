using System;
using System.Collections.Generic;
using System.Text;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace Venom.Components.World
{
    public enum Colors : int
    {
        Red,
        Green,
        Blue,
        White,
        Black,
    }

    public class WorldConstants
    {
        #region Properties
        //=> Default Text Format
        private TextFormat _defaultTextFormat;
        public TextFormat DefaultTextFormat => _defaultTextFormat;

        //=> Default Colors
        private Dictionary<Colors, SolidColorBrush> _defaultColors;
        public SolidColorBrush DefaultColor( Colors Color )
        {
            SolidColorBrush ret;
            _defaultColors.TryGetValue( Color, out ret );
            return ret;
        }
        #endregion




        public WorldConstants()
        {
        }

        public void Initialize( SharpDX.DirectWrite.Factory Factory, WindowRenderTarget RenderTarget )
        {
            _defaultTextFormat = new TextFormat( Factory, "Trebuchet MS", FontWeight.Regular, 0, 12 );

            //=> Default Colors
            _defaultColors.Add( Colors.Red, new SolidColorBrush( RenderTarget, new RawColor4( 255, 0, 0, 255 ) ) );
            _defaultColors.Add( Colors.Green, new SolidColorBrush( RenderTarget, new RawColor4( 0, 255, 0, 0 ) ) );
            _defaultColors.Add( Colors.Black, new SolidColorBrush( RenderTarget, new RawColor4( 0, 0, 0, 255 ) ) );
        }
    }
}
