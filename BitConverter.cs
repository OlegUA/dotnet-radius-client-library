using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ais.Net.Radius
{
    /// <summary>
    /// Converts base data types to an array of bytes, and an array of bytes to base data types. All info taken from the meta data of System.BitConverter. This implementation allows for Endianness consideration.
    /// </summary>
    public static class BitConverter
    {
        #region Properties

        /// <summary>
        /// Indicates the byte order ("endianess") in which data is stored in this computer architecture.
        /// </summary>
        public static bool IsLittleEndian { get; set; } // should default to false, which is what we want for Empire

        #endregion

        #region Methods

        #region GetBytes

        /// <summary>
        /// Returns the specified Boolean value as an array of bytes.
        /// </summary>
        /// <param name="value">A Boolean value.</param>
        /// <returns>An array of bytes with length 1.</returns>
        public static byte[] GetBytes(bool value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified Unicode character value as an array of bytes.
        /// </summary>
        /// <param name="value">A character to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        public static byte[] GetBytes(char value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified double-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        public static byte[] GetBytes(double value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified single-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        public static byte[] GetBytes(float value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 32-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        public static byte[] GetBytes(int value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 64-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        public static byte[] GetBytes(long value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 16-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        public static byte[] GetBytes(short value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 32-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        public static byte[] GetBytes(uint value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 64-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        public static byte[] GetBytes(ulong value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        /// <summary>
        /// Returns the specified 16-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        public static byte[] GetBytes(ushort value)
        {
            return IsLittleEndian ? System.BitConverter.GetBytes(value) : System.BitConverter.GetBytes(value).Reverse().ToArray();
        }

        #endregion

        #region ToType

        /// <summary>
        /// Returns a 16-bit signed integer converted from two bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 16-bit signed integer formed by two bytes beginning at startIndex.</returns>
        public static short ToInt16(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToInt16(value, startIndex) : System.BitConverter.ToInt16(value.Reverse().ToArray(), value.Length - sizeof(Int16) - startIndex);
        }

        /// <summary>
        /// Returns a 32-bit signed integer converted from four bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 32-bit signed integer formed by four bytes beginning at startIndex.</returns>
        public static int ToInt32(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToInt32(value, startIndex) : System.BitConverter.ToInt32(value.Reverse().ToArray(), value.Length - sizeof(Int32) - startIndex);
        }

        /// <summary>
        /// Returns a 64-bit signed integer converted from eight bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at startIndex.</returns>
        public static long ToInt64(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToInt64(value, startIndex) : System.BitConverter.ToInt64(value.Reverse().ToArray(), value.Length - sizeof(Int64) - startIndex);
        }

        /// <summary>
        /// Returns a single-precision floating point number converted from four bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A single-precision floating point number formed by four bytes beginning at startIndex.</returns>
        public static float ToSingle(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToSingle(value, startIndex) : System.BitConverter.ToSingle(value.Reverse().ToArray(), value.Length - sizeof(Single) - startIndex);
        }

        /// <summary>
        /// Converts the numeric value of each element of a specified array of bytes to its equivalent hexadecimal string representation.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <returns>A System.String of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in value; for example, "7F-2C-4A".</returns>
        public static string ToString(byte[] value)
        {
            return System.BitConverter.ToString(IsLittleEndian ? value : value.Reverse().ToArray());
        }

        /// <summary>
        /// Converts the numeric value of each element of a specified subarray of bytes to its equivalent hexadecimal string representation.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A System.String of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in a subarray of value; for example, "7F-2C-4A".</returns>
        public static string ToString(byte[] value, int startIndex)
        {
            return System.BitConverter.ToString(IsLittleEndian ? value : value.Reverse().ToArray(), startIndex);
        }

        /// <summary>
        /// Converts the numeric value of each element of a specified subarray of bytes to its equivalent hexadecimal string representation.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <param name="length">The number of array elements in value to convert.</param>
        /// <returns>A System.String of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in a subarray of value; for example, "7F-2C-4A".</returns>
        public static string ToString(byte[] value, int startIndex, int length)
        {
            return System.BitConverter.ToString(IsLittleEndian ? value : value.Reverse().ToArray(), startIndex, length);
        }

        /// <summary>
        /// Returns a 16-bit unsigned integer converted from two bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">The array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 16-bit unsigned integer formed by two bytes beginning at startIndex.</returns>
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToUInt16(value, startIndex) : System.BitConverter.ToUInt16(value.Reverse().ToArray(), value.Length - sizeof(UInt16) - startIndex);
        }

        /// <summary>
        /// Returns a 32-bit unsigned integer converted from four bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 32-bit unsigned integer formed by four bytes beginning at startIndex.</returns>
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToUInt32(value, startIndex) : System.BitConverter.ToUInt32(value.Reverse().ToArray(), value.Length - sizeof(UInt32) - startIndex);
        }

        /// <summary>
        /// Returns a 64-bit unsigned integer converted from eight bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A 64-bit unsigned integer formed by the eight bytes beginning at startIndex.</returns>
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            return IsLittleEndian ? System.BitConverter.ToUInt64(value, startIndex) : System.BitConverter.ToUInt64(value.Reverse().ToArray(), value.Length - sizeof(UInt64) - startIndex);
        }

        #endregion

        #endregion
    }
}