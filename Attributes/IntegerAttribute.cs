namespace Ais.Net.Radius.Attributes
{
    public class IntegerAttribute : Attribute
    {
        public uint BaseUintValue { get; set; }

        public IntegerAttribute()
            : base(AttributeType.Unknown)
        {
            
        }

        public IntegerAttribute(AttributeType attributeType, uint value)
            : base(attributeType)
        {
            BaseUintValue = value;
        }


        protected override byte[] ValueToByteArray()
        {
            return BitConverter.GetBytes(BaseUintValue); ;
        }
    }
}
