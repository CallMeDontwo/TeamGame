namespace ET
{
    internal sealed class CoinPusherSceneCreater0 : ADataSceneCreater
    {
        public override string GetSceneName() => "CoinPusher";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }

        public override async ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }
    }
}