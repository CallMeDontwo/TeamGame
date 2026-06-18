namespace ET.TeamGame
{
    /// <summary>
    /// 英雄专属数据组件 — 挂载在 Unit 实体上
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class HeroComponent : Entity, IAwake, IDestroy
    {
        /// <summary>当前经验值</summary>
        public long EXP { get; set; }

        /// <summary>已学会的技能 ConfigId 列表</summary>
        public int[] SkillIds { get; set; }

        /// <summary>已装备的装备 ConfigId 列表</summary>
        public int[] EquipIds { get; set; }

        /// <summary>在队伍中的槽位（0=未入队，1~4=队伍位置）</summary>
        public int TeamSlot { get; set; }

        /// <summary>是否由玩家直接操控</summary>
        public bool IsPlayerControlled { get; set; }
    }
}
