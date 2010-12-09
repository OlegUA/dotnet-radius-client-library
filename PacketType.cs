using System;
using System.Collections.Generic;
using System.Text;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Type of radius packet.
    /// </summary>
    public enum PacketType
    {
        Unknown = 0,
        AccessRequest = 1,
        AccessAccept = 2,
        AccessReject = 3,
        AccountingRequest = 4,
        AccountingResponse = 5,
        AccessChallenge = 11,
        StatusServer = 12,
        StatusClient = 13,
        Reserved = 255,
    }
}
