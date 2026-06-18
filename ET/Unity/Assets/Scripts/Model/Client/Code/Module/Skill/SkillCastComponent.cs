namespace ET.TeamGame
{
    /// <summary>
    /// 技能施法状态组件 — 驱动时间轴事件列表
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class SkillCastComponent : Entity, IAwake, IDestroy
    {
        /// <summary>正在释放的技能配置ID（0=空闲）</summary>
        public int SkillConfigId;

        /// <summary>技能开始帧时间(ms)</summary>
        public long SkillStartTime;

        /// <summary>下一个待执行事件的索引</summary>
        public int NextEventIndex;

        /// <summary>施法目标</summary>
        public EntityRef<Unit> Target { get; set; }

        /// <summary>缓存的技能数据（Cast时赋值，避免TickCasting每帧查表）</summary>
        public SkillData CachedData;

        /// <summary>缓存的事件数据列表引用（指向 CachedData.Events）</summary>
        public System.Collections.Generic.List<SkillEventData> CachedEvents;
    }
}
