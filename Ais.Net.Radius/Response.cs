using System;
using System.Collections.Generic;
using System.Text;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Represents response from radius server, contains radius packet.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Represents radius packet include all data.
        /// </summary>
        public Packet Packet { get; private set; }

        protected Response()
        {
        }

        /// <summary>
        /// Parses data received from radius server.
        /// </summary>
        /// <param name="source">Data received from radius server</param>
        /// <returns>Radius response</returns>
        internal static Response Parse(byte[] source)
        {
            var result = new Response {Packet = Packet.Parse(source)};
            return result;
        }
    }
}
