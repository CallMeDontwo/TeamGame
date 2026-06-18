using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能数据存储 — Model 层静态字典
    /// 由 ModelView 层的 SkillDataManager 填充
    /// Hotfix 层读取
    /// </summary>
    public static class SkillDataStore
    {
        [StaticField]
        private static readonly Dictionary<int, SkillData> dict = new();

        public static void Set(int skillId, SkillData data)
        {
            dict[skillId] = data;
        }

        public static SkillData Get(int skillId)
        {
            dict.TryGetValue(skillId, out var data);
            if (data == null)
                Log.Error($"SkillDataStore: 找不到技能 skillId={skillId}");
            return data;
        }

        public static bool Contains(int skillId) => dict.ContainsKey(skillId);

        public static void Clear() => dict.Clear();
    }
}
