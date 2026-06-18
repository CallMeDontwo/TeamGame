using System;

namespace ET
{
    internal class MainSceneCreater5 : ASceneCreater
    {
        public override int GetOrder() => 5;

        public override string GetSceneName() => "Main";

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}