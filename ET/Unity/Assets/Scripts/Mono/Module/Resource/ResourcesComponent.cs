using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace ET
{
    public class ResourcesComponent : Singleton<ResourcesComponent>, ISingletonAwake
    {
        private bool simulate;
        private EPlayMode playMode;
        private string buildTarget = "Unset";
        private string environment = "production";

        public void Awake()
        {
            YooAssets.Initialize();
            GlobalConfig globalConfig = Resources.Load<GlobalConfig>("GlobalConfig");
            this.playMode = globalConfig.EPlayMode;
            this.simulate = globalConfig.SimulateMode;
            this.environment = globalConfig.GetCurrentEnvironment();
            Resources.UnloadAsset(globalConfig);
            BuildinFileManifest manifest = Resources.Load<BuildinFileManifest>("BuildinFileManifest");
            this.buildTarget = manifest != null ? manifest.BuildTarget : Application.platform.ToString();
            Resources.UnloadAsset(manifest);
        }

        protected override void Destroy()
        {
#if !UNITY_WEBGL
            YooAssets.Destroy();
#endif
        }

        private string GetHostServerURL(string packageName)
        {
            const string hostServerIP = "https://oss.miwanbuluo.com/ArcadeMaster/Bundles";
            return $"{hostServerIP}/{this.environment}/{this.buildTarget}/{Application.version}/{packageName}";
        }

        public InitializationOperation CreatePackage(string packageName, YooAsset.EPlayMode playMode, bool isDefault = false)
        {
            ResourcePackage package = YooAssets.CreatePackage(packageName);
            if (isDefault)
            {
                YooAssets.SetDefaultPackage(package);
            }

            switch (playMode)
            {
#if UNITY_EDITOR
                case YooAsset.EPlayMode.EditorSimulateMode:
                    {
                        EditorSimulateModeParameters createParameters = new();
                        createParameters.AutoDestroyAssetProvider = true;
                        createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("ScriptableBuildPipeline", packageName);
                        return package.InitializeAsync(createParameters);
                    }
#endif
                case YooAsset.EPlayMode.OfflinePlayMode:
                    {
                        OfflinePlayModeParameters createParameters = new();
                        createParameters.AutoDestroyAssetProvider = true;
                        return package.InitializeAsync(createParameters);
                    }
                case YooAsset.EPlayMode.HostPlayMode:
                    {
                        string defaultHostServer = this.GetHostServerURL(packageName);
                        string fallbackHostServer = this.GetHostServerURL(packageName);
                        HostPlayModeParameters createParameters = new();
                        createParameters.AutoDestroyAssetProvider = true;
                        createParameters.BuildinQueryServices = new GameQueryServices();
                        createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                        return package.InitializeAsync(createParameters);
                    }
                case YooAsset.EPlayMode.WebPlayMode:
                    {
                        string defaultHostServer = this.GetHostServerURL(packageName);
                        string fallbackHostServer = this.GetHostServerURL(packageName);
                        WebPlayModeParameters createParameters = new();
                        createParameters.AutoDestroyAssetProvider = true;
                        createParameters.BuildinQueryServices = new GameQueryServices();
                        createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                        return package.InitializeAsync(createParameters);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public InitializationOperation CreatePackage(string packageName, bool isDefault = false)
        {
#if UNITY_EDITOR
            // 编辑器下的模拟模式
            if (this.simulate)
            {
                return this.CreatePackage(packageName, YooAsset.EPlayMode.EditorSimulateMode, isDefault);
            }
#endif
            switch (this.playMode)
            {
                case EPlayMode.OfflinePlayMode:
                    return this.CreatePackage(packageName, YooAsset.EPlayMode.OfflinePlayMode, isDefault);
                case EPlayMode.HostPlayMode:
                    return this.CreatePackage(packageName, YooAsset.EPlayMode.HostPlayMode, isDefault);
                case EPlayMode.WebPlayMode:
                    return this.CreatePackage(packageName, YooAsset.EPlayMode.WebPlayMode, isDefault);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async ETTask<ResourcePackage> CreatePackageAsync(string packageName, bool isDefault = false)
        {
            InitializationOperation operation = this.CreatePackage(packageName, isDefault);
            if (this.playMode != EPlayMode.WebPlayMode)
                await operation.Task;
            return YooAssets.GetPackage(operation.PackageName);
        }

        public void DestroyPackage(string packageName)
        {
            YooAssets.GetPackage(packageName).UnloadUnusedAssets();
        }

        /// <summary>
        /// 主要用来加载dll config aotdll，因为这时候纤程还没创建，无法使用ResourcesLoaderComponent。
        /// 游戏中的资源应该使用ResourcesLoaderComponent来加载
        /// </summary>
        public async ETTask<T> LoadAssetAsync<T>(string location) where T : UnityEngine.Object
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(location);
            await handle.Task;
            T t = (T)handle.AssetObject;
            handle.Release();
            return t;
        }

        /// <summary>
        /// 主要用来加载dll config aotdll，因为这时候纤程还没创建，无法使用ResourcesLoaderComponent。
        /// 游戏中的资源应该使用ResourcesLoaderComponent来加载
        /// </summary>
        public async ETTask<Dictionary<string, T>> LoadAllAssetsAsync<T>(string location) where T : UnityEngine.Object
        {
            AllAssetsHandle allAssetsOperationHandle = YooAssets.LoadAllAssetsAsync<T>(location);
            await allAssetsOperationHandle.Task;
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (UnityEngine.Object assetObj in allAssetsOperationHandle.AllAssetObjects)
            {
                T t = assetObj as T;
                dictionary.Add(t.name, t);
            }

            allAssetsOperationHandle.Release();
            return dictionary;
        }
    }
}