namespace ET.TeamGame
{
    /// <summary>
    /// 怪物专属数据组件 — 挂载在 Unit 实体上
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class MonsterComponent : Entity, IAwake, IDestroy
    {
        /// <summary>击杀后掉落物品 ConfigId 列表</summary>
        public int[] DropItemIds { get; set; }

        /// <summary>巡逻路径名（对应巡逻配置或序列化路径数据）</summary>
        public string PatrolPath { get; set; }

        /// <summary>警戒/仇恨范围（格子或世界单位）</summary>
        public float AggroRange { get; set; }

        /// <summary>死亡后重生时间（秒），0=不重生</summary>
        public float RespawnTime { get; set; }

        /// <summary>是否为精英怪</summary>
        public bool IsElite { get; set; }

        /// <summary>是否为 Boss</summary>
        public bool IsBoss { get; set; }

        /// <summary>关联的 AI 配置 ID（对应 AIConfig 表）</summary>
        public int AIConfigId { get; set; }
    }
}
