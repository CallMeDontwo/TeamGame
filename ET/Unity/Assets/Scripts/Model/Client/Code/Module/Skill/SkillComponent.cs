using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能组件 — 挂载在 Unit 上，管理技能列表和冷却
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class SkillComponent : Entity, IAwake, IDestroy
    {
        /// <summary>拥有的技能配置ID列表</summary>
        public int[] SkillIds { get; set; }

        /// <summary>技能ID → 冷却结束帧时间</summary>
        public Dictionary<int, long> CooldownEndTime;
    }
}
