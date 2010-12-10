using System;
using System.Text;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Represents vendor-specific attribute. More information in RFC 2865.
    /// </summary>
    public class VendorSpecificAttribute : Attribute
    {
        protected const byte VendorIdLength = 4;
        protected const byte TypeLength = 1;
        public int VendorId { get; set; }
        public byte Type { get; set; }
        public string Value { get; set; }

        public VendorSpecificAttribute()
            : this(0, 0, string.Empty)
        {
            
        }

        public VendorSpecificAttribute(int vendorId, byte type, string value)
            : base(AttributeType.VendorSpecific)
        {
            VendorId = vendorId;
            Type = type;
            Value = value;
        }

        protected override byte[] ValueToByteArray()
        {
            var valueArray = Encoding.ASCII.GetBytes(Value);

            var result = new byte[VendorIdLength + TypeLength + 1 + valueArray.Length];
            var vendorIdArray = BitConverter.GetBytes(VendorId);
            Array.Copy(vendorIdArray, 0, result, 0, vendorIdArray.Length);
            result[4] = Type;
            result[5] = (byte)(valueArray.Length + 2);
            Array.Copy(valueArray, 0, result, 6, valueArray.Length);
            return result;
        }
    }
}
