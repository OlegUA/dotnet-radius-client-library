using System.Text;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// This string attribute uses UTF8 encoding for string.
    /// </summary>
    public class TextAttribute : StringAttribute
    {
        public TextAttribute()
        {

        }
        public TextAttribute(AttributeType attributeType, string value)
            : base(attributeType, value)
        {
        }

        protected override byte[] ValueToByteArray()
        {
            return Encoding.UTF8.GetBytes(Value);
        }
    }
}
