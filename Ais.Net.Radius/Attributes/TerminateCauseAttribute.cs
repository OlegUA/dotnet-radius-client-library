namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Represents "Acct-Terminate-Cause" attribute in RFC 2866.  Possible causes described in TerminateCauseType enumeration.
    /// </summary>
    public class TerminateCauseAttribute : IntegerAttribute
    {
        public TerminateCauseType Value
        {
            get
            {
                return (TerminateCauseType)BaseUintValue;
            }
            set
            {
                BaseUintValue = (uint)value;
            }
        }

        public TerminateCauseAttribute()
            : this(TerminateCauseType.Unknown)
        {
            
        }

        public TerminateCauseAttribute(TerminateCauseType value)
            : base(AttributeType.AcctTerminateCause, (uint)value)
        {
        }
    }
}
