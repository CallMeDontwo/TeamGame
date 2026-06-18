using System;
using FairyGUI;

namespace ET
{
    internal class ShiTuoLingFuMoSceneCreater1 : AViewSceneCreater
    {
        public override string GetSceneName() => "ShiTuoLingFuMo";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            await UIPackageHelper.AddPackageAsync(FUIPackage.ShiTuoLingFuMo);
            UIPackage.GetByName(FUIPackage.ShiTuoLingFuMo).LoadAllAssets();
            await this.LoadScene(parent, scene, this.GetSceneName());
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}