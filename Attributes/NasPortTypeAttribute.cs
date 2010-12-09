namespace Ais.Net.Radius.Attributes
{
    public class NasPortTypeAttribute : IntegerAttribute
    {
        public NasPortType Value
        {
            get
            {
                return (NasPortType)BaseUintValue;
            }
            set
            {
                BaseUintValue = (uint)value;
            }
        }

        public NasPortTypeAttribute()
            : this(NasPortType.Async)
        {

        }

        public NasPortTypeAttribute(NasPortType value)
            : base(AttributeType.NasPortType, (uint)value)
        {
        }
    }
}
