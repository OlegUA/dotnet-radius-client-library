using System;
using System.Text;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Simple string radius attribute.
    /// </summary>
    [Serializable]
    public class StringAttribute : Attribute
    {
        public string Value { get; set; }

        public StringAttribute()
            : this(null)
        {

        }

        public StringAttribute(string value)
            : this(AttributeType.Unknown, value)
        {

        }

        public StringAttribute(AttributeType attributeType, string value)
            : base(attributeType)
        {
            Value = value;
        }


        protected override byte[] ValueToByteArray()
        {
            return Value == null ? null : Encoding.ASCII.GetBytes(Value);
        }
    }
}
