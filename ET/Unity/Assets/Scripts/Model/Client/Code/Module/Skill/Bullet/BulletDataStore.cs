using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹数据存储 — Model 层静态字典
    /// 由 ModelView 层的 BulletDataLoader 填充
    /// Hotfix 层读取
    /// </summary>
    public static class BulletDataStore
    {
        [StaticField]
        private static readonly Dictionary<int, BulletData> dict = new();

        public static void Set(int bulletId, BulletData data)
        {
            dict[bulletId] = data;
        }

        public static BulletData Get(int bulletId)
        {
            dict.TryGetValue(bulletId, out var data);
            if (data == null)
                Log.Error($"BulletDataStore: 找不到子弹 bulletId={bulletId}");
            return data;
        }

        public static bool Contains(int bulletId) => dict.ContainsKey(bulletId);

        public static void Clear() => dict.Clear();
    }
}
