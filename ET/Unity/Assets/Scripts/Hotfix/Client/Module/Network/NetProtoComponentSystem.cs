using System.Net;
using System.Net.Sockets;

namespace ET
{
    [EntitySystemOf(typeof(NetProtoComponent))]
    [FriendOf(typeof(NetProtoComponent))]
    public static partial class NetProtoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this NetProtoComponent self, AddressFamily addressFamily, NetworkProtocol protocol)
        {
            self.PService = new PService(addressFamily, ServiceType.Outer);
            self.PService.ReadCallback = self.OnRead;
            self.PService.ErrorCallback = self.OnError;
        }

        [EntitySystem]
        private static void Update(this NetProtoComponent self)
        {
            self.PService.Update();
        }

        [EntitySystem]
        private static void Destroy(this NetProtoComponent self)
        {
            self.PService.Dispose();
        }

        private static void OnRead(this NetProtoComponent self, long channelId, MemoryBuffer memoryBuffer)
        {
            PSession session = self.GetChild<PSession>(channelId);
            if (session != null)
            {
                session.LastRecvTime = TimeInfo.Instance.ClientNow();
                if (ProtoMessageHelper.TryToMessage(self.PService, memoryBuffer, out (int, object) res))
                {
                    int rpcId = res.Item1;
                    object message = res.Item2;
                    LogMsg.Instance.Debug(self.Fiber(), rpcId, message);
                    EventSystem.Instance.Invoke((long)self.IScene.SceneType, new NetProtoComponentOnRead() { Session = session, RpcId = rpcId, Message = message });
                }
            }
        }

        private static void OnError(this NetProtoComponent self, long channelId, int error)
        {
            PSession session = self.GetChild<PSession>(channelId);
            if (session != null)
            {
                session.Error = error;
                session.Dispose();
            }
        }

        public static async ETTask<PSession> CreateAsync(this NetProtoComponent self, IPEndPoint realIPEndPoint)
        {
            long channelId = NetServices.Instance.CreateConnectChannelId();
            PSession session = self.AddChildWithId<PSession, AService, IPEndPoint>(channelId, self.PService, realIPEndPoint);
            await self.PService.CreateAsync(session.Id, session.RemoteAddress);
            return session;
        }
    }
}