namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Represents "Service-Type" attribute. Possible types list described in ServiceType enumeration.
    /// </summary>
    public class ServiceTypeAttribute : IntegerAttribute
    {
        public ServiceType Value
        {
            get
            {
                return (ServiceType)BaseUintValue;
            }
            set
            {
                BaseUintValue = (uint)value;
            }
        }

        public ServiceTypeAttribute()
            : this(ServiceType.Unknown)
        {
        }
        public ServiceTypeAttribute(ServiceType value)
            : base(AttributeType.ServiceType, (uint)value)
        {
        }
    }
}
