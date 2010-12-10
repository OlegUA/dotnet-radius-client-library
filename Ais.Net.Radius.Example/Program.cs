using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ais.Net.Radius.Attributes;

namespace Ais.Net.Radius.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Settings
            const string serverIp = "192.168.1.1";
            const string serverSecret = "secretsecretsecret";
            const string userName = "user";
            const string password = "123456";
            const string nasIp = "192.168.20.1";
            const string clientIp = "192.168.20.45";
            const string sessionId = "1234567899874654321";

            var radiusClient = new Client(serverIp, 1645, serverSecret)
                                   {
                                       SendTimeout = 5000,
                                       ReceiveTimeout = 5000,
                                       Ttl = 50
                                   };

            var request = new AccessRequest(nasIp, ServiceType.Framed, userName, password, radiusClient);
            request.Packet.Attributes.Add(new StringAttribute(AttributeType.NasIdentifier, "BBSM"));
            request.Packet.Attributes.Add(new NasPortTypeAttribute(NasPortType.Ethernet));
            request.Packet.Attributes.Add(new StringAttribute(AttributeType.AcctSessionId, sessionId));
            request.Packet.Attributes.Add(new IpAddressAttribute(AttributeType.FramedIpAddress, clientIp));
            var response = radiusClient.Send(request, true);

            var accountingStartRequest = new AccountingRequest(nasIp, ServiceType.Framed, userName, AuthenticationType.Radius,
                                                          StatusType.Start, 0, clientIp, sessionId, radiusClient);
            request.Packet.Attributes.Add(new StringAttribute(AttributeType.NasIdentifier, "BBSM"));
            request.Packet.Attributes.Add(new NasPortTypeAttribute(NasPortType.Ethernet));
            var accountingStartResponse = radiusClient.Send(accountingStartRequest, true);

            var accountingUpdateRequest = new AccountingRequest(nasIp, ServiceType.Framed, userName, AuthenticationType.Radius,
                                    StatusType.InterimUpdate, 0, clientIp, sessionId, radiusClient);
            request.Packet.Attributes.Add(new StringAttribute(AttributeType.NasIdentifier, "BBSM"));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctInputOctets, 5000));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctOutputOctets, 2000));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctSessionTime, 50));
            var accountingUpdateResponse = radiusClient.Send(accountingUpdateRequest, true);

            var accountingStopRequest = new AccountingRequest(nasIp, ServiceType.Framed, userName, AuthenticationType.Radius,
                                    StatusType.Stop, 0, clientIp, sessionId, radiusClient);
            request.Packet.Attributes.Add(new StringAttribute(AttributeType.NasIdentifier, "BBSM"));
            request.Packet.Attributes.Add(new NasPortTypeAttribute(NasPortType.Ethernet));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctInputOctets, 40000));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctOutputOctets, 20000));
            request.Packet.Attributes.Add(new IntegerAttribute(AttributeType.AcctSessionTime, 200));
            request.Packet.Attributes.Add(new TerminateCauseAttribute(TerminateCauseType.UserRequest));
            var accountingStopResponse = radiusClient.Send(accountingStopRequest, true);

        }
    }
}
