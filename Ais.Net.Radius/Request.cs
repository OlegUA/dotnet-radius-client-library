using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ais.Net.Radius.Attributes;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Represents request to radius server. Simple class to send all types of requests.
    /// </summary>
    public class Request
    {
        private readonly Packet _packet;

        public Request(PacketType packetType, string nasIpAddress, ServiceType serviceType, string userName)
        {
            _packet = new Packet(packetType);
            _packet.Attributes.Add(new IpAddressAttribute(AttributeType.NasIpAddress, nasIpAddress));
            _packet.Attributes.Add(new ServiceTypeAttribute(serviceType));
            _packet.Attributes.Add(new StringAttribute(AttributeType.UserName, userName));
        }

        /// <summary>
        /// Represents radius packet include all data.
        /// </summary>
        public Packet Packet
        {
            get
            {
                return _packet;
            }
        }
    }
}
