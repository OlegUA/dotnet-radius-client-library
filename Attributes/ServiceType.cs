namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates type of service the user has requested.
    /// </summary>
    public enum ServiceType
    {
        Unknown = 0,
        Login = 1,
        Framed = 2,
        CallbackLogin = 3,
        CallbackFramed = 4,
        Outbound = 5,
        Administrative = 6,
        NasPrompt = 7,
        AuthenticateOnly = 8,
        CallbackNasPrompt = 9,
        CallCheck = 10,
        CallbackAdministrative = 11
    }
}
