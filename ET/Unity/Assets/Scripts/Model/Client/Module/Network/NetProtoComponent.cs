using System.Net.Sockets;

namespace ET
{
    public struct NetProtoComponentOnRead
    {
        public int RpcId;
        public PSession Session;
        public object Message;
    }

    [ComponentOf(typeof(Scene))]
    public sealed class NetProtoComponent : Entity, IAwake<AddressFamily, NetworkProtocol>, IDestroy, IUpdate
    {
        public PService PService { get; set; }
    }
}