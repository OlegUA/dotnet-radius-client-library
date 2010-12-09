namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Indicates whether Accounting-Request packet marks the beginning of the user service (Start) or the end (Stop).
    /// </summary>
    public class StatusTypeAttribute : IntegerAttribute
    {
        public StatusType Value
        {
            get
            {
                return (StatusType)BaseUintValue;
            }
            set
            {
                BaseUintValue = (uint)value;
            }
        }

        public StatusTypeAttribute()
            : this(StatusType.Unknown)

        {
            
        }
        public StatusTypeAttribute(StatusType value)
            : base(AttributeType.AcctStatusType, (uint)value)
        {
        }

    }
}
