using System;
using ET._Component_Public;

namespace ET
{
    public sealed class MainSceneCreater6 : ASceneCreater
    {
        public override int GetOrder() => 6;

        public override string GetSceneName() => "Main";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            var window2 = await TipWindowComponent.CreateInstanceAsync();
            await ViewController.Instance.InitPanel(window2);
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}