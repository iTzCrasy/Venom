using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Venom.Data.Models
{
    public class ServerData
    {
        public int World { get; set; }
        public string Url { get; set; }
        public DateTimeOffset LastUpdate { get; set; }

        public int Ping
        {
            get => TestPing( );
        }

        private int TestPing()
        {
            Ping pingSender = new Ping( );
            PingOptions options = new PingOptions( );

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes( data );
            int timeout = 120;
            PingReply reply = pingSender.Send( Url, timeout, buffer, options );
            if( reply.Status == IPStatus.Success )
            {
                Console.WriteLine( "Address: {0}", reply.Address.ToString( ) );
                Console.WriteLine( "RoundTrip time: {0}", reply.RoundtripTime );
                Console.WriteLine( "Time to live: {0}", reply.Options.Ttl );
                Console.WriteLine( "Don't fragment: {0}", reply.Options.DontFragment );
                Console.WriteLine( "Buffer size: {0}", reply.Buffer.Length );

                return reply.Options.Ttl;
            }

            return 0;
        }
    }
}
