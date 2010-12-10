namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates the type of the physical port of the NAS which is authenticating the user.
    /// </summary>
    public enum NasPortType
    {
        Async = 0,
        Sync = 1,
        IsdnSync = 2,
        IsdnAsyncV120 = 3,
        IsdnAsyncV110 = 4,
        Virtual = 5,
        Piafs = 6,
        HdlcClearChannel = 7,
        X25 = 8,
        X75 = 9,
        G3Fax = 10,
        SdslSymmetricDsl = 11,
        AdslCapAsymmetricDslCarrierlessAmplitudePhaseModulation = 12,
        AdslDmtAsymmetricDslDiscreteMultiTone = 13,
        IdslIsdnDigitalSubscriberLine = 14,
        Ethernet = 15,
        XDslDigitalSubscriberLineOfUnknownType = 16,
        Cable = 17,
        WirelessOther = 18,
        WirelessIeee80211 = 19
    }
}
