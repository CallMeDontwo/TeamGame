using ET._Component_Public;
using ET.Client;
using FairyGUI;
using UnityEngine;

namespace ET
{
    [Invoke]
    internal sealed class AppInit_Invoke : AInvokeHandler<AppInit, ETTask>
    {
        public override async ETTask Handle(AppInit args)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            //Application.targetFrameRate = 240;
            StageCamera.DefaultConstantSize = false;
            StageCamera.DefaultUnitsPerPixel = 0.01f;

            World.Instance.AddSingleton<PackageBinder>();
            World.Instance.AddSingleton<ViewController>();
            World.Instance.AddSingleton<UIPackageHelper>();

            GRoot.inst.SetContentScaleFactor(1920, 1080);
            Stage.inst.onStageResized.Add(OnStageResized);
            Stage.inst.onStageResized.Call();

            using ListComponent<ETTask> tasks = new ListComponent<ETTask>();
            tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Component_Public, true));
            tasks.Add(UIPackageHelper.AddPackageAsync(FUIPackage._Resources_Misc, true));
            await ETTaskHelper.WaitAll(tasks);
            LoadingComponent window1 = await LoadingComponent.CreateInstanceAsync();
            await ViewController.Instance.InitPanel(window1, true);
            LoadingWindow.Instance.Show();
            AssetLoader.Instance.Dispose();
        }

        private static void OnStageResized(EventContext context)
        {
            Rect safeArea = Screen.safeArea;
            Stage.inst.gameObject.GetComponent<UIContentScaler>().ApplyChange();
            GRoot.inst.SetScale(UIContentScaler.scaleFactor, UIContentScaler.scaleFactor);
            GRoot.inst.SetSize(safeArea.size.x / UIContentScaler.scaleFactor, safeArea.size.y / UIContentScaler.scaleFactor);
            GRoot.inst.SetXY(safeArea.x, Screen.height - safeArea.yMax);
            context.PreventDefault();
        }
    }
}