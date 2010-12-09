using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Attribute = Ais.Net.Radius.Attributes.Attribute;

namespace Ais.Net.Radius
{
    /// <summary>
    /// This class contains all data fields of radius packet. Represents data format which described in RFC 2865.
    /// </summary>
    public class Packet
    {
        #region Constants

        private const byte RequestAuthenticatorLength = 16;
        //It's evil! I know.
        private const byte PacketLengthLength = 2;
        private const byte IdentifierLength = 1;
        //If this const rename to CodeLength? Visual Studio 2010 momentally crashes. -(
        private const byte PacketCodeLength = 1;
        private const byte MinPacketLength =
            PacketCodeLength + IdentifierLength + PacketLengthLength + RequestAuthenticatorLength;

        #endregion

        #region Fields

        private byte _id;
        private PacketType _code;
        private byte[] _requestAuthenticator;
        private readonly List<Attribute> _attributes = new List<Attribute>();
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of a packet. Random value.
        /// </summary>
        public byte Id
        {
            get
            {
                if (_id == 0)
                    GenerateId();
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Length of a packet.
        /// </summary>
        public ushort Length
        {
            get
            {
                ushort result = 0;

                foreach (var attribute in Attributes)
                    result += attribute.Length;

                result += PacketCodeLength + IdentifierLength + PacketLengthLength;
                result += (ushort)RequestAuthenticator.Length;
                return result;
            }
        }

        /// <summary>
        /// Represents Authenticator field in radius packet. The Authenticator field is sixteen (16) octets.
        /// </summary>
        public byte[] RequestAuthenticator
        {
            get
            {
                if (_requestAuthenticator == null)
                    GenerateRequestAuthenticator();
                return _requestAuthenticator;
            }
        }

        /// <summary>
        /// Type of radius packet.
        /// </summary>
        public PacketType PacketType
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        /// <summary>
        /// Collection of radius attributes in packet
        /// </summary>
        public List<Attribute> Attributes
        {
            get
            {
                return _attributes;
            }
        }

        /// <summary>
        /// Represents shared secret value.
        /// </summary>
        public string Secret { get; set; }

        #endregion

        #region Constructor

        public Packet()
        {
            _code = PacketType.Unknown;
        }

        public Packet(PacketType code)
        {
            _code = code;
        }

        #endregion

        #region Private methods

        private void GenerateId()
        {
            _id = GenerateRandomByte();
        }

        private void GenerateRequestAuthenticator()
        {
            _requestAuthenticator = new byte[RequestAuthenticatorLength];

            for (var i = 0; i < RequestAuthenticatorLength; i++)
                _requestAuthenticator[i] = Convert.ToByte(_random.Next(1, 99));
        }

        private void GenerateRequestAuthenticator(byte[] packet)
        {
            if (string.IsNullOrEmpty(Secret))
                throw new Exception("Secret not set");

            var result = new byte[packet.Length + Secret.Length];
            var secret = Encoding.ASCII.GetBytes(Secret);
            Array.Copy(packet, 0, result, 0, packet.Length);
            Array.Copy(secret, 0, result, packet.Length, secret.Length);

            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(result);
            Array.Copy(hash, 0, packet, PacketCodeLength + IdentifierLength + PacketLengthLength,
                       RequestAuthenticator.Length);
        }

        private byte GenerateRandomByte()
        {
            return Convert.ToByte(_random.Next(0, 255));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Assembles radius packet and prepares it for sending.
        /// </summary>
        internal byte[] Assemble()
        {
            var offset = 0;
            var result = new byte[Length];
            result[offset] = (byte)_code;
            offset += PacketCodeLength;
            result[offset] = Id;
            offset += IdentifierLength;
            var packetLengthArray = BitConverter.GetBytes(Length);
            Array.Copy(packetLengthArray, 0, result, offset, packetLengthArray.Length);
            offset += PacketLengthLength;
            Array.Copy(RequestAuthenticator, 0, result, offset, RequestAuthenticator.Length);
            offset += RequestAuthenticator.Length;

            //Assemble attributes
            foreach (var attributeArray in Attributes.Select(attribute => attribute.Assemble()))
            {
                Array.Copy(attributeArray, 0, result, offset, attributeArray.Length);
                offset += attributeArray.Length;
            }

            //The Request Authenticator field in Accounting-Request packets contains a one-
            //way MD5 hash calculated over a stream of octets consisting of the
            //Code + Identifier + Length + 16 zero octets + request attributes +
            //shared secret (where + indicates concatenation).  The 16 octet MD5
            //hash value is stored in the Authenticator field of the
            //Accounting-Request packet.
            //(C) RFC 2866
            if (PacketType == PacketType.AccountingRequest)
            {
                var ra = new byte[RequestAuthenticatorLength];
                Array.Copy(ra, 0, result, 4, ra.Length);
                GenerateRequestAuthenticator(result);
            }

            return result;
        }

        /// <summary>
        /// Parses data received from radius server and creates Packet.
        /// </summary>
        /// <param name="source">Data received from radius server</param>
        /// <returns>Radius packet</returns>
        //TODO: Implement check of Request Autheticator in packet.
        public static Packet Parse(byte[] source)
        {
            Packet result = null;
            var offset = 0;

            if (source.Length < MinPacketLength)
                return null;

            var code = source[offset];
            offset += PacketCodeLength;

            foreach (var value in Enum.GetValues(typeof(PacketType)).Cast<int>().Where(value => value == code))
                result = new Packet((PacketType)value);

            if (result == null)
                result = new Packet();

            result._id = source[offset];
            offset += IdentifierLength + PacketLengthLength;
            result._requestAuthenticator = new byte[RequestAuthenticatorLength];
            Array.Copy(source, offset, result._requestAuthenticator, 0, RequestAuthenticatorLength);

            offset += RequestAuthenticatorLength;

            while (offset + 1 < source.Length)
            {
                var attributeLength = source[offset + 1];

                if (attributeLength < 3)
                    break;

                var attribyteArray = new byte[attributeLength];
                Array.Copy(source, offset, attribyteArray, 0, attributeLength);
                var attribute = Attribute.Parse(attribyteArray);

                if (attribute != null)
                    result._attributes.Add(attribute);

                offset += attributeLength;
            }

            return result;
        }
        
        #endregion
    }
}
