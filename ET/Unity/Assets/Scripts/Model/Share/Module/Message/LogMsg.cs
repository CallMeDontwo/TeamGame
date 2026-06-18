using System;
using System.Collections.Generic;

namespace ET
{
    public class LogMsg : Singleton<LogMsg>, ISingletonAwake
    {
        private readonly HashSet<Type> ignore = new()
        {
            //typeof(ReqHeartBeat),
            //typeof(RespHeartBeat),
            //typeof(ReqMove),
            //typeof(PushPlayerMove),
            //typeof(RespAckError),
        };

        public void Awake()
        {
        }

        public void Debug(Fiber fiber, object msg)
        {
            if (this.ignore.Contains(msg.GetType()))
            {
                return;
            }
            fiber.Log.Debug(msg.ToString());
        }

        public void Debug(Fiber fiber, int rpcId, object msg)
        {
            if (this.ignore.Contains(msg.GetType()))
            {
                return;
            }
            fiber.Log.Debug($"RpcId:{rpcId} Msg:{msg}");
        }
    }
}