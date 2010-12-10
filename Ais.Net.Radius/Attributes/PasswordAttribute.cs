using System;
using System.Security.Cryptography;
using System.Text;

namespace Ais.Net.Radius.Attributes
{
    /// <summary>
    /// Uses for "User-Password" radius attribute type.
    /// </summary>
    public class PasswordAttribute : StringAttribute
    {
        public string Secret { get; set; }
        public byte[] RequestAuthenticator { get; set; }

        public PasswordAttribute()
            : base(AttributeType.UserPassword, null)
        {
            
        }

        public PasswordAttribute(string secret, byte[] requestAuthenticator, string value)
            : base(AttributeType.UserPassword, value)
        {
            Secret = secret;
            RequestAuthenticator = requestAuthenticator;
        }

        protected override byte[] ValueToByteArray()
        {
            if (Value == null)
                return null;

            var md5 = new MD5CryptoServiceProvider();
            var key = Secret + Encoding.Default.GetString(RequestAuthenticator);
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(key));
            var hashed = System.BitConverter.ToString(hash).Replace("-", string.Empty);

            var rounds = (int)Math.Ceiling((double)Value.Length / 16);
            var result = new byte[rounds * 16];

            for (var j = 0; j < rounds; j++)
            {
                var currentChunkStr = Value.Length < (j + 1) * 16 ? Value.Substring(j * 16, Value.Length - j * 16) : Value.Substring(j * 16, 16);

                for (var i = 0; i <= 15; i++)
                {
                    var pm = 2 * i > hashed.Length ? 0 : Convert.ToInt32(hashed.Substring(2 * i, 2), 16);
                    var pp = i >= currentChunkStr.Length ? 0 : currentChunkStr[i];

                    var pc = pm ^ pp;
                    result[(j * 16) + i] = (byte)pc;
                }

                var currentChunk = new byte[16];
                Array.Copy(result, j * 16, currentChunk, 0, 16);
                var currentKey = Secret + Encoding.Default.GetString(currentChunk);
                hash = md5.ComputeHash(Encoding.Default.GetBytes(currentKey));
                hashed = System.BitConverter.ToString(hash).Replace("-", string.Empty);
            }

            return result;
        }
    }
}
