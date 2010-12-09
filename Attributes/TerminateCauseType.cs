namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates session termination cause the user has closed.
    /// </summary>
    public enum TerminateCauseType
    {
        Unknown = 0,
        UserRequest = 1,
        LostCarrier = 2,
        LostService = 3,
        IdleTimeout = 4,
        SessionTimeout = 5,
        AdminReset = 6,
        AdminReboot = 7,
        PortError = 8,
        NasError = 9,
        NasRequest = 10,
        NasReboot = 11,
        PortUnneeded = 12,
        PortPreempted = 13,
        PortSuspended = 14,
        ServiceUnavailable = 15,
        Callback = 16,
        UserError = 17,
        HostRequest = 18
    }
}
