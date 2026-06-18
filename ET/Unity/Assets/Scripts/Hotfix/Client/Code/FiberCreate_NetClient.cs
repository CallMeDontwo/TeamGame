using System.Net.Sockets;

namespace ET.Client
{
    [Invoke((long)SceneType.NetClient)]
    public class FiberCreate_NetClient : AInvokeHandler<FiberCreate, ETTask>
    {
        public override async ETTask Handle(FiberCreate fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<FiberParentComponent>();
            root.AddComponent<NetComponent, AddressFamily, NetworkProtocol>(AddressFamily.InterNetwork, NetworkProtocol.TCP);
            await ETTask.CompletedTask;
        }
    }
}