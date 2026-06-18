using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using UnityEngine;
using YooAsset;

namespace ET
{
    public class UIPackageHelper : Singleton<UIPackageHelper>, ISingletonAwake
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private const string PATH = "Assets/Bundles/FGUI";
        /// <summary>
        /// 手动释放的包
        /// </summary>
        private readonly HashSet<string> ManualPackages = new HashSet<string>();
        /// <summary>
        /// PackItem名-Asset键值对
        /// </summary>
        private readonly Dictionary<string, AssetHandle> PackageItemMap = new Dictionary<string, AssetHandle>();

        public void Awake()
        {
        }

        /// <summary>
        /// 加载UI资源包
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static void AddPackage(string packageName, bool manual = false)
        {
            if (Contains(packageName))
            {
                return;
            }

            if (manual)
            {
                Instance.ManualPackages.Add(packageName);
            }

            UIPackage uiPackage = UIPackage.AddPackage(LoadDesc(packageName), packageName, LoadRes);

            static byte[] LoadDesc(string packageName)
            {
                AssetHandle asset = YooAssets.LoadAssetSync<TextAsset>($"{PATH}/{packageName}/{packageName}_fui.bytes");
                byte[] bytes = asset.GetAssetObject<TextAsset>().bytes;
                asset.Release();
                return bytes;
            }

            object LoadRes(string name, string extension, Type type, out DestroyMethod destroyMethod)
            {
                destroyMethod = DestroyMethod.None;
                if (Instance.PackageItemMap.ContainsKey($"{name}{extension}"))
                {
                    return Instance.PackageItemMap[$"{name}{extension}"].AssetObject;
                }
                else
                {
                    AssetHandle asset = YooAssets.LoadAssetSync($"{PATH}/{packageName}/{name}{extension}", type);
                    Instance.PackageItemMap[$"{name}{extension}"] = asset;
                    return asset.AssetObject;
                }
            }
        }

        /// <summary>
        /// 异步加载UI资源包
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static async ETTask AddPackageAsync(string packageName, bool manual = false)
        {
            if (Contains(packageName))
            {
                return;
            }

            if (manual)
            {
                Instance.ManualPackages.Add(packageName);
            }

            UIPackage uiPackage = UIPackage.AddPackage(await LoadDescAsync(packageName), packageName, LoadResourceAsync);

            static async ETTask<byte[]> LoadDescAsync(string packageName)
            {
                AssetHandle asset = YooAssets.LoadAssetAsync<TextAsset>($"{PATH}/{packageName}/{packageName}_fui.bytes");
                await asset.Task;
                var bytes = asset.GetAssetObject<TextAsset>().bytes;
                asset.Release();
                return bytes;
            }

            static void LoadResourceAsync(string name, string extension, Type type, PackageItem item)
            {
                LoadResourceTask(name, extension, type, item).Coroutine();
            }

            static async ETTask LoadResourceTask(string name, string extension, Type type, PackageItem item)
            {
                if (!Instance.PackageItemMap.ContainsKey(item.file))
                {
                    AssetHandle asset = YooAssets.LoadAssetAsync($"{PATH}/{item.owner.name}/{item.file}", type);
                    Instance.PackageItemMap[item.file] = asset;
                    await asset.Task;
                    item.owner.SetItemAsset(item, asset.AssetObject, DestroyMethod.None);
                }
                else
                {
                    AssetHandle assetHandle = Instance.PackageItemMap[item.file];
                    if (!assetHandle.IsDone)
                    {
                        await assetHandle.Task;
                    }
                    item.owner.SetItemAsset(item, assetHandle.AssetObject, DestroyMethod.None);
                }
            }
        }

        /// <summary>
        /// 卸载包
        /// </summary>
        /// <param name="packageName"></param>
        public static void RemovePackage(string packageName)
        {
            UIPackage package = UIPackage.GetByName(packageName);
            if (package != null)
            {
                foreach (PackageItem item in package.GetItems())
                {
                    if (item.file != null && Instance.PackageItemMap.Remove(item.file, out AssetHandle asset))
                    {
                        asset.Release();
                    }
                }

                UIPackage.RemovePackage(packageName);
            }
        }

        public static bool Contains(string packageName)
        {
            return UIPackage.GetByName(packageName) != null;
        }

        public static void RemoveAllPackages()
        {
            UIPackage.GetPackages().Where(pkg => !Instance.ManualPackages.Contains(pkg.name)).ToList().Foreach(pkg => RemovePackage(pkg.name));
        }
    }
}