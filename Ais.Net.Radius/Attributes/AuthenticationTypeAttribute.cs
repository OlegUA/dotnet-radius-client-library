namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Represents "Acct-Authentic" attribute in RFC 2866.  Possible causes described in AuthenticationType enumeration.
    /// </summary>
    public class AuthenticationTypeAttribute : IntegerAttribute
    {
        public AuthenticationType Value
        {
            get
            {
                return (AuthenticationType)BaseUintValue;
            }
            set
            {
                BaseUintValue = (uint)value;
            }
        }

        public AuthenticationTypeAttribute()
            :this(AuthenticationType.Unknown)
        {
            
        }

        public AuthenticationTypeAttribute(AuthenticationType value)
            : base(AttributeType.AcctAuthentic, (uint)value)
        {
        }

    }
}
