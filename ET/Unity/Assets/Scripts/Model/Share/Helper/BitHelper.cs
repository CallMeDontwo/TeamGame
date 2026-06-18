namespace ET
{
    public static class BitHelper
    {
        public static void WriteBytes(byte[] bytes, int offset, int num, bool littleEndian = false)
        {
            if (littleEndian)
            {
                bytes[offset] = (byte)(num & 0xff);
                bytes[offset + 1] = (byte)((num & 0xff00) >> 8);
                bytes[offset + 2] = (byte)((num & 0xff0000) >> 16);
                bytes[offset + 3] = (byte)((num & 0xff000000) >> 24);
            }
            else
            {
                bytes[offset] = (byte)((num & 0xff000000) >> 24);
                bytes[offset + 1] = (byte)((num & 0xff0000) >> 16);
                bytes[offset + 2] = (byte)((num & 0xff00) >> 8);
                bytes[offset + 3] = (byte)(num & 0xff);
            }
        }

        public static int ToInt32(byte[] bytes, int offset, bool littleEndian = false)
        {
            int value = 0;
            if (littleEndian)
            {
                value |= bytes[offset + 3];
                value <<= 8;
                value |= bytes[offset + 2];
                value <<= 8;
                value |= bytes[offset + 1];
                value <<= 8;
                value |= bytes[offset];
            }
            else
            {
                value |= bytes[offset];
                value <<= 8;
                value |= bytes[offset + 1];
                value <<= 8;
                value |= bytes[offset + 2];
                value <<= 8;
                value |= bytes[offset + 3];
            }
            return value;
        }
    }
}