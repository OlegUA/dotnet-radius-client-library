using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Ais.Net.Radius
{
    public class Client
    {
        #region Fields

        private readonly IPAddress _ip;
        private readonly ushort _port;
        private readonly string _secret;
        private byte[] _receiveBytes;
        private bool _messageReceived;

        #endregion

        #region Properties

        /// <summary>
        /// Radius shared secret.
        /// </summary>
        public string Secret
        {
            get { return _secret; }
        }

        /// <summary>
        /// UDP port of connection to radius server.
        /// </summary>
        public ushort Port
        {
            get { return _port; }
        }

        /// <summary>
        /// IP Address of radius serve.
        /// </summary>
        public IPAddress Ip
        {
            get { return _ip; }
        }

        /// <summary>
        /// Gets or sets a value that specifies the amount of time after which a synchronous Send call will time out.
        /// </summary>
        public int SendTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the amount of time after which a synchronous Receive call will time out.
        /// </summary>
        public int ReceiveTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the Time To Live (TTL) value of Internet Protocol (IP) packets sent by the Socket.
        /// </summary>
        public short Ttl { get; set; }

        #endregion

        #region Constructor

        public Client(string ip, ushort port, string secret)
        {
            _ip = IPAddress.Parse(ip);
            _port = port;
            _secret = secret;
        }

        public Client(IPAddress ip, ushort port, string secret)
        {
            _ip = ip;
            _port = port;
            _secret = secret;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends request to radius server.
        /// </summary>
        /// <param name="request">Request to radius server.</param>
        /// <param name="asyncReceive">If true - packet sends asynchronously.</param>
        /// <returns>Response from radius server.</returns>
        public Response Send(Request request, bool asyncReceive)
        {
            _receiveBytes = null;
            _messageReceived = false;
            var client = new UdpClient { Client = { SendTimeout = SendTimeout, ReceiveTimeout = ReceiveTimeout, Ttl = Ttl } };
            client.Connect(_ip, _port);
            var packet = request.Packet.Assemble();
            client.Send(packet, packet.Length);
            var point = new IPEndPoint(IPAddress.Any, 0);

            if (asyncReceive)
            {
                var state = new UdpState { Client = client, EndPoint = point };
                client.BeginReceive(ReceiveCallback, state);
                var waitCounter = 0;
                const int sleepTime = 50;

                while (!_messageReceived)
                {
                    Thread.Sleep(sleepTime);
                    waitCounter += sleepTime;

                    if (waitCounter > ReceiveTimeout)
                        throw new SocketException(10060);
                }
            }
            else
            {
                Thread.Sleep(100);
                _receiveBytes = client.Receive(ref point);
            }
            var result = Response.Parse(_receiveBytes);
            client.Close();
            return result;
        }

        /// <summary>
        /// Sends request packet several times.
        /// </summary>
        /// <param name="request">Request to radius server.</param>
        /// <param name="asyncReceive">If true - packet sends asynchronously.</param>
        /// <param name="numberOfTries">Number of tries to send packet.</param>
        /// <returns>Response from radius server.</returns>
        public Response Send(Request request, bool asyncReceive = true, int numberOfTries = 2)
        {
            Response result = null;
            Exception ex = null;

            for (var i = 0; i < numberOfTries; i++)
            {
                try
                {
                    result = Send(request, asyncReceive);
                }
                catch (Exception e)
                {
                    ex = e;
                    continue;
                }

                break;
            }

            if (result == null && ex != null)
                throw ex;

            return result;
        }

        protected void ReceiveCallback(IAsyncResult ar)
        {
            var client = ((UdpState)(ar.AsyncState)).Client;
            var endPoint = ((UdpState)(ar.AsyncState)).EndPoint;
            var exceptionOccured = false;

            try
            {
                _receiveBytes = client.EndReceive(ar, ref endPoint);
            }
            catch (Exception)
            {
                exceptionOccured = true;
            }
            _messageReceived = !exceptionOccured;
        }

        #endregion

        #region UdpState class

        /// <summary>
        /// Internal class for callback data.
        /// </summary>
        private class UdpState
        {
            public UdpClient Client { get; set; }
            public IPEndPoint EndPoint { get; set; }
        }

        #endregion
    }
}
