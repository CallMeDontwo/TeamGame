using System;
using ET.Client;

namespace ET
{
    internal sealed class MainSceneCreater3 : ASceneCreater
    {
        public override int GetOrder() => 3;
        public override string GetSceneName() => "Main";

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            scene.AddComponent<ResourcesLoaderComponent>();
            await ETTask.CompletedTask;
        }

        public override async ETTask OnCreateComplete(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}