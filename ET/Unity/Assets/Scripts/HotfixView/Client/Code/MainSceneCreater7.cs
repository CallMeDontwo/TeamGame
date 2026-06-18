using System;
using System.Threading.Tasks;
using ET._Component_Main;
using FairyGUI;

namespace ET
{
    internal sealed class MainSceneCreater7 : ASceneCreater
    {
        public override int GetOrder() => 7;

        public override string GetSceneName() => "Main";

        public override ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            throw new NotImplementedException();
        }

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            using ListComponent<ETTask> tasks = new ListComponent<ETTask>();
            tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Component_Main));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Resources_Icon));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Resources_Pic));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Component_World));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.DiTuJieMian));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.DaTingJieMian));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.JinBiDaZhan));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.YouJianXiTong));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.QianDaoXiTong));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.RenWuXiTong));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.PaiXingBang));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.ChongZhiZhongXin));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.ShouChongLiBao));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.GeRenZhuYe));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.SheZhiJieMian));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.DuiHuanXiTong));
            //tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage.TiaoZhanSai));
            await ETTaskHelper.WaitAll(tasks);

            await this.CreateAsync(Layer0Component.CreateInstanceAsync());
            await this.CreateAsync(Layer1Component.CreateInstanceAsync());
            await this.CreateAsync(Layer2Component.CreateInstanceAsync());
            await this.CreateAsync(Layer3Component.CreateInstanceAsync());
            await this.CreateAsync(Layer4Component.CreateInstanceAsync());
            await this.CreateAsync(Layer5Component.CreateInstanceAsync());
            await this.CreateAsync(Layer6Component.CreateInstanceAsync());
            await this.CreateAsync(Layer7Component.CreateInstanceAsync());
            await this.CreateAsync(Layer8Component.CreateInstanceAsync());
            await this.CreateAsync(Layer9Component.CreateInstanceAsync());
        }

        private async ETTask CreateAsync<T>(Task<T> task) where T : GComponent
        {
            T component = await task;
            GRoot.inst.AddChild(component);
            component.fairyBatching = true;
            component.MakeFullScreen();
            component.Center();
            component.AddRelation(GRoot.inst, RelationType.Size);
            await ViewController.Instance.InitChildren(component);
        }

        public override ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            throw new NotImplementedException();
        }
    }
}