using YooAsset;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹数据加载器 — ModelView 层
    /// 通过 YooAsset.LoadAllAssetsAsync 一次性加载所有子弹 JSON
    /// </summary>
    public static class BulletDataLoader
    {
        private static bool _loaded;

        public static async ETTask LoadAll()
        {
            if (_loaded) return;
            _loaded = true;

            var handles = YooAssets.LoadAllAssetsAsync<UnityEngine.TextAsset>(
                "Assets/Bundles/Bullet/bullet_1001.json");
            await handles.Task;
            if (handles == null) return;

            foreach (var ass in handles.AllAssetObjects)
            {
                var asset = ass as UnityEngine.TextAsset;
                if (asset == null) continue;

                var data = UnityEngine.JsonUtility.FromJson<BulletData>(asset.text);
                if (data != null && data.Id > 0)
                    BulletDataStore.Set(data.Id, data);
            }

            Log.Debug($"BulletDataLoader: 加载 {handles.AllAssetObjects.Length} 个子弹");
        }
    }
}
