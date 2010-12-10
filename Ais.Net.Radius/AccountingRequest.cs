using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ais.Net.Radius.Attributes;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Radius packet of accounting request type.
    /// </summary>
    public class AccountingRequest : Request
    {
        public AccountingRequest(string nasIpAddress, ServiceType serviceType, string userName,
            AuthenticationType authenticationType, StatusType statusType, uint delayTime, string clientIp, string sessionId, Client client)
            : base(PacketType.AccountingRequest, nasIpAddress, serviceType, userName)
        {
            Packet.Secret = client.Secret;
            Packet.Attributes.Add(new AuthenticationTypeAttribute(authenticationType));
            Packet.Attributes.Add(new StatusTypeAttribute(statusType));
            Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctDelayTime, delayTime));
            Packet.Attributes.Add(new IpAddressAttribute(AttributeType.FramedIpAddress, clientIp));
            Packet.Attributes.Add(new StringAttribute(AttributeType.AcctSessionId, sessionId));
        }
    }
}
