namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates authentication type which may be included in an Accounting-Request.
    /// </summary>
    public enum AuthenticationType
    {
        Unknown = 0,
        Radius = 1,
        Local = 2,
        Remote = 3
    }
}
