using System;
using System.IO;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace ET
{
    public static class ProtoMessageHelper
    {
        public static MessageObject Deserialize(Type type, MemoryBuffer stream)
        {
            object o = ObjectPool.Instance.Fetch(type);
            RuntimeTypeModel.Default.Deserialize(stream, o, type);
            return o as MessageObject;
        }

        public static async ETTask<MessageObject> DeserializeAsync(Type type, MemoryBuffer stream)
        {
            object o = ObjectPool.Instance.Fetch(type);
            await Task.Run(() => RuntimeTypeModel.Default.Deserialize(stream, o, type));
            return o as MessageObject;
        }

        public static void Serialize(MessageObject message, MemoryBuffer stream)
        {
            MemoryPackHelper.Serialize(message, stream);
        }

        public static MemoryBuffer ToMemoryBuffer(AService service, int rpcId, object message)
        {
            MemoryBuffer memoryBuffer = service.Fetch();
            MemoryBuffer messageBuffer = service.Fetch();
            RuntimeTypeModel.Default.Serialize(messageBuffer, message);
            int opcode = ProtoType.Instance.TypeCode[message.GetType()];
            memoryBuffer.SetLength(12);
            memoryBuffer.Position = 0;
            BitHelper.WriteBytes(memoryBuffer.GetBuffer(), (int)memoryBuffer.Position, opcode);
            memoryBuffer.Position += 4;
            BitHelper.WriteBytes(memoryBuffer.GetBuffer(), (int)memoryBuffer.Position, rpcId);
            memoryBuffer.Position += 4;
            BitHelper.WriteBytes(memoryBuffer.GetBuffer(), (int)memoryBuffer.Position, (int)messageBuffer.Length);
            memoryBuffer.Position += 4;
            messageBuffer.WriteTo(memoryBuffer);
            memoryBuffer.Position = 0;
            ((MessageObject)message).Dispose();
            service.Recycle(messageBuffer);
            return memoryBuffer;
        }

        public static (int, object) ToMessage(AService service, MemoryBuffer memoryStream)
        {
            int opcode = BitHelper.ToInt32(memoryStream.GetBuffer(), 0);
            int rpcId = BitHelper.ToInt32(memoryStream.GetBuffer(), 4);
            Type type = ProtoType.Instance.CodeType[(int)opcode];
            memoryStream.Seek(12, SeekOrigin.Begin);
            object message = Deserialize(type, memoryStream);
            service.Recycle(memoryStream);
            return (rpcId, message);
        }

        public static bool TryToMessage(AService service, MemoryBuffer memoryStream, out (int, object) result)
        {
            int opcode = BitHelper.ToInt32(memoryStream.GetBuffer(), 0);
            int rpcId = BitHelper.ToInt32(memoryStream.GetBuffer(), 4);
            if (ProtoType.Instance.CodeType.TryGetValue(opcode, out var type))
            {
                memoryStream.Seek(12, SeekOrigin.Begin);
                object message = Deserialize(type, memoryStream);
                service.Recycle(memoryStream);
                result = (rpcId, message);
                return true;
            }
            else
            {
                Log.Warning($"Opcode {opcode} was not present in the dictionary");
                result = (rpcId, null);
                return false;
            }
        }

        public static async ETTask<(int, object)> ToMessageAsync(AService service, MemoryBuffer memoryStream)
        {
            int opcode = BitHelper.ToInt32(memoryStream.GetBuffer(), 0);
            int rpcId = BitHelper.ToInt32(memoryStream.GetBuffer(), 4);
            Type type = ProtoType.Instance.CodeType[(int)opcode];
            memoryStream.Seek(12, SeekOrigin.Begin);
            object message = await DeserializeAsync(type, memoryStream);
            service.Recycle(memoryStream);
            return (rpcId, message);
        }
    }
}