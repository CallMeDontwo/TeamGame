using System;
using System.Text;

namespace ET
{
    public static class ByteSizeUtil
    {
        [ThreadStatic]
        private static StringBuilder stringBuilder = null;

        public static string SizeToString(long byteSize)
        {
            stringBuilder ??= new StringBuilder(16);
            stringBuilder.Clear();
            if (byteSize < 1048576)
            {
                double value = byteSize / 1024d;
                return stringBuilder.Append(value.ToString("F2")).Append("KB").ToString();
            }
            else
            {
                double value = byteSize / 1048576d;
                return stringBuilder.Append(value.ToString("F2")).Append("MB").ToString();
            }
        }

        public static string SizeToMB(long byteSize)
        {
            stringBuilder ??= new StringBuilder(16);
            stringBuilder.Clear();
            double value = byteSize / 1048576d;
            return stringBuilder.Append(value.ToString("F2")).Append("MB").ToString();
        }
    }
}