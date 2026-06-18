using System;
using System.Collections.Generic;
using System.Net;

namespace ET
{
    public readonly struct PRpcInfo
    {
        public int RpcId { get; }

        public Type RequestType { get; }

        private readonly ETTask<MessageObject> tcs;

        public PRpcInfo(int rpcId, Type type)
        {
            this.RpcId = rpcId;
            this.RequestType = type;
            this.tcs = ETTask<MessageObject>.Create(true);
        }

        public void SetResult(MessageObject response)
        {
            this.tcs.SetResult(response);
        }

        public void SetException(Exception exception)
        {
            this.tcs.SetException(exception);
        }

        public ETTask<MessageObject> Wait()
        {
            return this.tcs;
        }
    }

    [ChildOf]
    public sealed class PSession : Entity, IAwake<AService>, IAwake<AService, IPEndPoint>, IDestroy
    {
        public readonly Dictionary<int, PRpcInfo> requestCallbacks = new();
        public AService AService { get; set; }
        public int RpcId { get; set; }
        public long LastRecvTime { get; set; }
        public long LastSendTime { get; set; }
        public int Error { get; set; }
        public IPEndPoint RemoteAddress { get; set; }
    }

    [EntitySystemOf(typeof(PSession))]
    [FriendOf(typeof(PSession))]
    public static partial class PSessionSystem
    {
        [EntitySystem]
        private static void Awake(this PSession self, AService aService)
        {
            long timeNow = TimeInfo.Instance.ClientNow();
            self.AService = aService;
            self.LastRecvTime = timeNow;
            self.LastSendTime = timeNow;
            self.requestCallbacks.Clear();
            Log.Info($"session create: zone: {self.Zone()} id: {self.Id} {timeNow} ");
        }

        [EntitySystem]
        private static void Awake(this PSession self, AService args2, IPEndPoint args3)
        {
            long timeNow = TimeInfo.Instance.ClientNow();
            self.AService = args2;
            self.RemoteAddress = args3;
            self.LastRecvTime = timeNow;
            self.LastSendTime = timeNow;
            self.requestCallbacks.Clear();
            Log.Info($"session create: zone: {self.Zone()} id: {self.Id} {timeNow} RemoteAddress:{args3}");
        }

        [EntitySystem]
        private static void Destroy(this PSession self)
        {
            self.AService.Remove(self.Id, self.Error);
            self.requestCallbacks.Values.Foreach(callback => callback.SetException(new RpcException(self.Error, $"session dispose: {self.Id} {self.RemoteAddress} RpcId:{callback.RpcId} ResponseType:{callback.RequestType.Name}")));
            self.requestCallbacks.Clear();
            Log.Info($"session dispose: {self.RemoteAddress} id: {self.Id} ErrorCode: {self.Error}, please see ErrorCode.cs! {TimeInfo.Instance.ClientNow()}");
        }

        public static void OnResponse(this PSession self, int rpcId, MessageObject response)
        {
            if (self.requestCallbacks.Remove(rpcId, out PRpcInfo action))
            {
                response.Error = 0;
                response.Message = string.Empty;
                action.SetResult(response);
            }
        }

        public static void OnResponse(this PSession self, int rpcId, int error, string message)
        {
            if (self.requestCallbacks.Remove(rpcId, out PRpcInfo action))
            {
                object obj = ObjectPool.Instance.Fetch(action.RequestType);
                MessageObject response = obj as MessageObject;
                response.Error = error;
                response.Message = message;
                action.SetResult(response);
            }
        }

        private static void Send(this PSession self, int rpcId, MessageObject message)
        {
            self.LastSendTime = TimeInfo.Instance.ClientNow();
            LogMsg.Instance.Debug(self.Fiber(), rpcId, message);
            MemoryBuffer memoryBuffer = ProtoMessageHelper.ToMemoryBuffer(self.AService, rpcId, message);
            self.AService.Send(self.Id, memoryBuffer);
        }

        public static void Send(this PSession self, MessageObject message)
        {
            self.Send(0, message);
        }

        public static async ETTask<T> Call<T>(this PSession self, MessageObject request, ETCancellationToken cancellationToken) where T : MessageObject
        {
            int rpcId = ++self.RpcId;
            PRpcInfo rpcInfo = new(rpcId, typeof(T));
            self.requestCallbacks[rpcId] = rpcInfo;
            self.Send(rpcId, request);

            void CancelAction()
            {
                if (self.requestCallbacks.Remove(rpcId, out PRpcInfo action))
                {
                    T response = ObjectPool.Instance.Fetch<T>();
                    response.Error = ErrorCore.ERR_Cancel;
                    action.SetResult(response);
                }
            }

            try
            {
                cancellationToken?.Add(CancelAction);
                return await rpcInfo.Wait() as T;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }
        }

        public static async ETTask<T> Call<T>(this PSession self, MessageObject request, int time = 0) where T : MessageObject
        {
            int rpcId = ++self.RpcId;
            PRpcInfo rpcInfo = new(rpcId, typeof(T));
            self.requestCallbacks[rpcId] = rpcInfo;
            self.Send(rpcId, request);

            if (time > 0)
            {
                Timeout().Coroutine();

                async ETTask Timeout()
                {
                    await self.Root().GetComponent<TimerComponent>().WaitAsync(time * 1000);

                    if (self.requestCallbacks.Remove(rpcId, out PRpcInfo action))
                    {
                        action.SetException(new RpcException(ErrorCore.ERR_Timeout, $"session call timeout: {request.GetType().FullName} {time}"));
                    }
                }
            }

            return await rpcInfo.Wait() as T;
        }
    }
}