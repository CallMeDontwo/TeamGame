using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ET
{
    public static class StringHelper
    {
        [StaticField]
        public static readonly StringBuilder Builder = new StringBuilder(2048);

        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static byte[] ToBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static byte[] ToUtf8(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static byte[] HexToBytes(this string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            var hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = "";
                byteValue += hexString[index * 2];
                byteValue += hexString[index * 2 + 1];
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return hexAsBytes;
        }

        public static string Fmt(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        public static string ListToString<T>(this IList<T> list)
        {
            return new StringBuilder().Append("[").Append(string.Join(",", list)).Append("]").ToString();
        }
    }
}