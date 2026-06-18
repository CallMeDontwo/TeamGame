using System;
using System.Net;
using System.Net.Sockets;
using ET.Client;

namespace ET.GemBoom
{
    internal sealed class GemBoomSceneCreater0 : ADataSceneCreater
    {
        public override string GetSceneName() => "GemBoom";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            scene.AddComponent<SessionComponent>();
            scene.AddComponent<NetProtoComponent, AddressFamily, NetworkProtocol>(AddressFamily.InterNetwork, NetworkProtocol.TCP);
            await ETTask.CompletedTask;
        }

        public override async ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }
    }
}