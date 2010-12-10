using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ais.Net.Radius.Attributes;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Radius packet of access request type.
    /// </summary>
    public class AccessRequest : Request
    {
        public AccessRequest(string nasIpAddress, ServiceType serviceType, string userName, string password, Client client) 
            : base(PacketType.AccessRequest, nasIpAddress, serviceType, userName)
        {
            Packet.Attributes.Add(new PasswordAttribute(client.Secret, Packet.RequestAuthenticator, password));
        }
    }
}
