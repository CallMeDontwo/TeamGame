using System;
using System.IO;

namespace ET
{
    [EnableClass]
    public sealed class PPacketParser
    {
        public const int InnerPacketSizeLength = 12;
        public const int OuterPacketSizeLength = 12;

        private readonly AService service;
        private readonly CircularBuffer buffer;
        private readonly byte[] cache = new byte[12];

        private int packetSize;
        private ParserState state;
        public MemoryBuffer MemoryBuffer;

        public PPacketParser(AService service, CircularBuffer buffer)
        {
            this.buffer = buffer;
            this.service = service;
        }

        public bool Parse(out MemoryBuffer memoryBuffer)
        {
            while (true)
            {
                switch (this.state)
                {
                    case ParserState.PacketSize:
                        {
                            if (this.buffer.Length < OuterPacketSizeLength)
                            {
                                memoryBuffer = null;
                                return false;
                            }

                            this.buffer.Peek(this.cache, 0, OuterPacketSizeLength);
                            this.packetSize = BitHelper.ToInt32(this.cache, 8);
                            //if (this.packetSize < Packet.MinPacketSize)
                            //{
                            //    throw new Exception($"recv packet size error, 可能是外网探测端口: {this.packetSize}");
                            //}
                            this.state = ParserState.PacketBody;
                            break;
                        }
                    case ParserState.PacketBody:
                        {
                            if (this.buffer.Length < this.packetSize + 12)
                            {
                                memoryBuffer = null;
                                return false;
                            }

                            memoryBuffer = this.service.Fetch(this.packetSize + 12);
                            this.buffer.Read(memoryBuffer, this.packetSize + 12);
                            memoryBuffer.Seek(0, SeekOrigin.Begin);
                            this.state = ParserState.PacketSize;
                            return true;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}