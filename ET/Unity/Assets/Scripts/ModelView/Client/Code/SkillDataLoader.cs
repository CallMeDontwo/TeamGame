using System.Threading.Tasks;
using YooAsset;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能数据加载器 — ModelView 层
    /// 通过 YooAsset.LoadAllAssetsSync 一次性加载所有技能 JSON
    /// </summary>
    public static class SkillDataLoader
    {
        [StaticField]
        private static bool _loaded;

        public static async ETTask LoadAll()
        {
            if (_loaded) return;
            _loaded = true;

            var handles =  YooAssets.LoadAllAssetsAsync<UnityEngine.TextAsset>("Assets/Bundles/Skill/skill_1001.json");
            await handles.Task;
            if (handles == null) return;

            foreach (var ass in handles.AllAssetObjects)
            {
                var asset = ass as UnityEngine.TextAsset;
                if (asset == null) continue;

                var data = UnityEngine.JsonUtility.FromJson<SkillData>(asset.text);
                if (data != null && data.SkillId > 0)
                    SkillDataStore.Set(data.SkillId, data);
            }

            Log.Debug($"SkillDataLoader: 加载 {handles.AllAssetObjects.Length} 个技能");
        }
    }
}
