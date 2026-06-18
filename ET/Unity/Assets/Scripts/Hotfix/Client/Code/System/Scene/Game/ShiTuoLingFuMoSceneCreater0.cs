using System.Net.Sockets;

namespace ET.Game
{
    internal class ShiTuoLingFuMoSceneCreater0 : ADataSceneCreater
    {
        public override string GetSceneName() => "ShiTuoLingFuMo";

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