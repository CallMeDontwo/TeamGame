using System.Net;
using ET.Client;

namespace ET
{
    [EntitySystemOf(typeof(ClientSessionErrorComponent))]
    public static partial class ClientSessionErrorComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ClientSessionErrorComponent self)
        {
            self.Times = 0;
        }

        [EntitySystem]
        private static void Destroy(this ClientSessionErrorComponent self)
        {
            self.SessionDisposeTip().Coroutine();
        }

        private static async ETTask SessionDisposeTip(this ClientSessionErrorComponent self)
        {
            PSession oldSession = self.GetParent<PSession>();
            if (oldSession.Error == 0)
            {
                return;
            }
            Scene scene = self.Scene();
            IPEndPoint remote = oldSession.RemoteAddress;
            await TipsWindow.Instance.ShowTip("断开链接!");
            ETCancellationToken token = new ETCancellationToken();
            ShowTipLater(scene, token).Coroutine();
            for (int i = 0; i < 3; i++)
            {
                PSession newSession = await scene.GetComponent<NetProtoComponent>().CreateAsync(remote);
                try
                {
                    //ReqReConnect req = ReqReConnect.Create(true);
                    //using RespAckError resp = await newSession.Call<RespAckError>(req, 6);
                    //if (resp.Error == 0)
                    //{
                    //    scene.GetComponent<SessionComponent>().Session = newSession;
                    //    newSession.AddComponent<PingComponent>();
                    //    newSession.AddComponent<ClientSessionErrorComponent>(true);
                    //    token.Cancel();
                    //    return;
                    //}
                    //else
                    //{
                    //    await TipsWindow.Instance.ShowTip("重连失败!");
                    //    EventSystem.Instance.Invoke(new BackLogin());
                    //    return;
                    //}
                }
                catch (RpcException e)
                {
                    newSession.Dispose();
                    if (e.Error == 10061)
                    {
                        await TipsWindow.Instance.ShowTip("连接被拒绝!");
                        EventSystem.Instance.Invoke(new BackLogin());
                        return;
                    }
                    if (e.Error == ErrorCore.ERR_Timeout)
                    {
                        await TipsWindow.Instance.ShowTip("重连超时!");
                        EventSystem.Instance.Invoke(new BackLogin());
                        return;
                    }
                }
                catch
                {
                    newSession.Dispose();
                    continue;
                }
            }
            await TipsWindow.Instance.ShowTip("重连失败多次!");
            EventSystem.Instance.Invoke(new BackLogin());
        }

        private static async ETTask ShowTipLater(Scene scene, ETCancellationToken token)
        {
            await scene.GetComponent<TimerComponent>().WaitAsync(500, token);
            if (token.IsCancel())
                return;
        }
    }
}