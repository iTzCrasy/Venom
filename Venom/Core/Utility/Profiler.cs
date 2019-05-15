using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Core.Utility
{
    public class Profiler
    {
        public static TimeSpan Profile( Action action )
        {
            var watch = new Stopwatch( );

            watch.Start( );
            {
                action( );
            }
            watch.Stop( );

            return watch.Elapsed;
        }
    }
}
