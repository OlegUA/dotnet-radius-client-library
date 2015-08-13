This library implements Remote Authentication Dial In User Service (RADIUS) protocol (RFC 2865) and RADIUS Accounting (RFC 2866) on .NET framework 4.0. Library written on C#.

Supports:
  * Access and accounting requests
  * PAP encryption only
  * Vendor specific attributes
  * Parsing response from RADIUS server


Last updates:
  * Examples added.


Usage example:
```
var radiusClient = new Client("10.25.1.1", 1813, "secret");
var request = new AccountingRequest("10.25.1.2", ServiceType.Framed, "username",
                                                AuthenticationType.Radius,
                                                StatusType.Start, 0, "10.25.1.101", "sessionid", radiusClient);
request.Packet.Attributes.Add(new StringAttribute(AttributeType.NasIdentifier, "BBSM"));
request.Packet.Attributes.Add(new NasPortTypeAttribute(NasPortType.Ethernet));
var response = radiusClient.Send(request, true, 2);
           
```