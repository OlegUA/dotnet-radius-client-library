using System.Net;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates simple IP address.
    /// </summary>
    public class IpAddressAttribute : Attribute
    {
        public string Value { get; set; }

        public IpAddressAttribute()
            : this(AttributeType.Unknown, IPAddress.Any)
        {
            
        }

        public IpAddressAttribute(AttributeType attributeType, IPAddress value)
            : base(attributeType)
        {
            Value = value.ToString();
        }

        public IpAddressAttribute(AttributeType attributeType, string value)
            : base(attributeType)
        {
            Value = value;
        }

        protected override byte[] ValueToByteArray()
        {
            return IPAddress.Parse(Value).GetAddressBytes();
        }
    }
}
