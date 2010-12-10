using System;
using System.Net;
using System.Text;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Base class for all radius attributes in packet.
    /// </summary>
    public abstract class Attribute
    {
        #region Constants

        protected const byte TechLength = 2;

        #endregion

        #region Fields

        private readonly AttributeType _attributeType;
        private byte[] _valueArray;
        
        #endregion

        #region Constructor

        protected Attribute(AttributeType attributeType)
        {
            _attributeType = attributeType;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Type of the attribute. Possible values described in AttributeType enumeration.
        /// </summary>
        public AttributeType AttributeType
        {
            get
            {
                return _attributeType;
            }
        }

        /// <summary>
        /// Attribute length.
        /// </summary>
        public byte Length
        {
            get
            {
                return Convert.ToByte(ValueArray.Length + TechLength);
            }
        }


        /// <summary>
        /// Value of an attribute represented in array of octets.
        /// </summary>
        public byte[] ValueArray
        {
            get
            {
                return _valueArray ?? (_valueArray = ValueToByteArray());
            }
        }

        protected abstract byte[] ValueToByteArray();

        #endregion

        #region Methods

        /// <summary>
        /// Assembles radius attribute and prepares it for sending.
        /// </summary>
        public byte[] Assemble()
        {
            var result = new byte[Length];
            result[0] = (byte)_attributeType;
            result[1] = Length;
            Array.Copy(ValueArray, 0, result, 2, ValueArray.Length);
            return result;
        }

        /// <summary>
        /// Parses data received from radius server and creates Attribute.
        /// </summary>
        public static Attribute Parse(byte[] source)
        {
            if (source.Length < 3)
                return null;

            Attribute result;
            var attributeType = AttributeType.Unknown;

            foreach (int value in Enum.GetValues(typeof(AttributeType)))
                if (value == source[0])
                    attributeType = (AttributeType)value;

            var sourceValue = new byte[source.Length - 2];
            Array.Copy(source, 2, sourceValue, 0, sourceValue.Length);

            //TODO: Add other attribute types
            switch (attributeType)
            {
                case AttributeType.NasIpAddress:
                    result = new IpAddressAttribute(attributeType, new IPAddress(sourceValue));
                    break;
                case AttributeType.ServiceType:
                    result = new ServiceTypeAttribute((ServiceType)BitConverter.ToInt32(sourceValue, 0));
                    break;
                case AttributeType.SessionTimeout:
                    result = new IntegerAttribute(attributeType, BitConverter.ToUInt32(sourceValue, 0));
                    break;
                case AttributeType.AcctAuthentic:
                    result =
                        new AuthenticationTypeAttribute(
                            (AuthenticationType)BitConverter.ToUInt32(sourceValue, 0));
                    break;
                case AttributeType.AcctStatusType:
                    result = new StatusTypeAttribute((StatusType)BitConverter.ToUInt32(sourceValue, 0));
                    break;
                default:
                    result = new StringAttribute(attributeType, Encoding.UTF8.GetString(sourceValue));
                    break;
            }

            return result;
        }

        #endregion
    }
}
