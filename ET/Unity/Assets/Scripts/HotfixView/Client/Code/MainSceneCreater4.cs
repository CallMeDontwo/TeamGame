using System;
using ET._Component_Public;

namespace ET
{
    internal class MainSceneCreater4 : ASceneCreater
    {
        public override int GetOrder() => 4;

        public override string GetSceneName() => "Main";

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            ViewController.Instance.RootScene = scene;
            ViewController.Instance.HideByGType<LoadingComponent>();
            await ETTask.CompletedTask;
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}