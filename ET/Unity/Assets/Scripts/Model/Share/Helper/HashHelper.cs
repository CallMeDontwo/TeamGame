using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ET
{
    public static class HashHelper
    {
        public static string BytesMD5(byte[] bytes)
        {
            using MD5 md5 = MD5.Create();
            return md5.ComputeHash(bytes).ToHex();
        }

        public static string StreamMD5(Stream stream)
        {
            using MD5 md5 = MD5.Create();
            return md5.ComputeHash(stream).ToHex();
        }

        public static string FileMD5(string filePath)
        {
            using FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return StreamMD5(file);
        }

        public static string StringMD5(string input)
        {
            return BytesMD5(Encoding.UTF8.GetBytes(input));
        }

        public static string BytesSHA256(byte[] input)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(input).ToHex();
        }

        public static string StreamSHA256(Stream input)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(input).ToHex();
        }

        public static string StringSHA256(string input)
        {
            return BytesSHA256(Encoding.UTF8.GetBytes(input));
        }

        public static string FileSHA256(string filePath)
        {
            using FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return StreamSHA256(file);
        }
    }
}