using System;
using System.Collections.Generic;
using System.Text;


namespace Venom.Components.World
{
    public class WorldRenderHelper
    {
        #region Framerate
        private int _frameCount = 0;
        private int _frameRate = 0;
        private DateTime _frameTime = DateTime.Now;

        public void CalculateFramerate()
        {
            _frameCount++;

            if( ( DateTime.Now - _frameTime ).TotalSeconds >= 1 )
            {
                _frameRate = _frameCount;
                _frameCount = 0;
                _frameTime = DateTime.Now;
            }
        }

        public int GetFramerate() => _frameRate;
        #endregion
    }
}
