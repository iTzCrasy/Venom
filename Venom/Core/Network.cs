using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Core
{
    public static class Network
    {
        public static string GetUrlRead( string Url )
        {
            using( var Web = new WebClient() )
            {
                return new StreamReader( Web.OpenRead( new Uri( Url ) ) ).ReadToEnd();
            }
        }
    }
}
