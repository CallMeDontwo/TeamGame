using System;
using System.Collections;
using ET.ZiYuanGengXin;
using FairyGUI;
using UnityEngine;
using YooAsset;

namespace ET
{
    public sealed class AssetLoader : IDisposable
    {
        public static AssetLoader Instance = new AssetLoader();

        private double startTime;
        private HotfixComponent hotfixComponent;

        public void Dispose()
        {
            if (Instance != null)
            {
                Instance = null;
                this.hotfixComponent.Dispose();
                UIPackage.RemovePackage("资源更新");
            }
        }

        internal ETTask LoadAsync()
        {
            ETTask task = ETTask.Create(true);
            Global.Coroutine(this.LoadAsyncInternal(task));
            return task;
        }

        private IEnumerator LoadAsyncInternal(ETTask task)
        {
            ZiYuanGengXinBinder.BindAll();
            UIPackage.AddPackage("FGUI/资源更新/资源更新");
            GRoot.inst.SetContentScaleFactor(1080, 1920);
            this.hotfixComponent = HotfixComponent.CreateInstance();
            this.hotfixComponent.MakeFullScreen();
            this.hotfixComponent.AddRelation(GRoot.inst, RelationType.Size);
            GRoot.inst.AddChild(this.hotfixComponent);
            this.hotfixComponent.C_Status.selectedIndex = 0;
            InitializationOperation initialization = ResourcesComponent.Instance.CreatePackage("DefaultPackage", true);
            yield return initialization;
            ResourcePackage package = YooAssets.GetPackage(initialization.PackageName);
            UpdatePackageVersionOperation updatePackageVersion = package.UpdatePackageVersionAsync(false);
            yield return updatePackageVersion;
            UpdatePackageManifestOperation updatePackageManifest = package.UpdatePackageManifestAsync(updatePackageVersion.PackageVersion);
            yield return updatePackageManifest;
            ResourceDownloaderOperation resourceDownloader = package.CreateResourceDownloader(10, 3);
            if (resourceDownloader.TotalDownloadCount > 0)
            {
                this.startTime = Time.timeAsDouble;
                this.hotfixComponent.C_Status.selectedIndex = 1;
                this.hotfixComponent.Bar_Download.min = 0;
                this.hotfixComponent.Bar_Download.max = resourceDownloader.TotalDownloadBytes;
                this.UpdateDownloadProgress(0, 0, resourceDownloader.TotalDownloadBytes, resourceDownloader.CurrentDownloadBytes);
                resourceDownloader.OnDownloadProgressCallback = this.UpdateDownloadProgress;
                resourceDownloader.BeginDownload();
                yield return resourceDownloader;
            }
            this.hotfixComponent.C_Status.selectedIndex = 2;
            UIObjectFactory.Clear();
            task.SetResult();
        }

        private void UpdateDownloadProgress(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
        {
            double costTime = Time.timeAsDouble - this.startTime;
            long downloadSpeed = (long)(costTime > 0 ? currentDownloadBytes / costTime : 0);
            this.hotfixComponent.Text_DownloadSpeed
                .SetVar("var1", ByteSizeUtil.SizeToString(downloadSpeed))
                .FlushVars();
            this.hotfixComponent.Text_DownloadSize
                .SetVar("var1", ByteSizeUtil.SizeToString(currentDownloadBytes))
                .SetVar("var2", ByteSizeUtil.SizeToString(totalDownloadBytes))
                .FlushVars();
            this.hotfixComponent.Bar_Download.value = currentDownloadBytes;
        }
    }
}