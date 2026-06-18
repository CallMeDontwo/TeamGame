using System;
using System.Net.Sockets;

namespace ET
{
    internal sealed class MainSceneCreater1 : ASceneCreater
    {
        public override int GetOrder() => 1;
        public override string GetSceneName() => "Main";

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            scene.AddComponent<TimerComponent>();
            scene.AddComponent<CoroutineLockComponent>();
            scene.AddComponent<ObjectWait>();
            scene.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            scene.AddComponent<ProcessInnerSender>();
            scene.AddComponent<SessionComponent>();
            scene.AddComponent<CurrentScenesComponent>();
            scene.AddComponent<SceneArgumentsComponent>();
            scene.AddComponent<NetProtoComponent, AddressFamily, NetworkProtocol>(AddressFamily.InterNetwork, NetworkProtocol.TCP);
            await ETTask.CompletedTask;
        }

        public override async ETTask OnCreateComplete(Scene parent, Scene scene, SceneArguments args)
        {
            await CurrentSceneFactory.Create(parent, 0, "TeamGame");
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}