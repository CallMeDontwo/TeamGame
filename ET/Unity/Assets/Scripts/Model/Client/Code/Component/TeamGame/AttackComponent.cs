namespace ET.TeamGame
{
    /// <summary>
    /// 攻击组件 — 挂载在 Unit 上，管理攻击冷却时间
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class AttackComponent : Entity, IAwake, IDestroy
    {
        /// <summary>下次允许攻击的时间戳（客户端帧时间）</summary>
        public long AttackNextTime { get; set; }

        /// <summary>普攻对应的技能ID（走 SkillComponent 管道）</summary>
        public int BasicAttackSkillId { get; set; }

        /// <summary>攻击可达高度（int/100，从普攻技能缓存，0=仅同层）</summary>
        public int ReachHeight { get; set; }
    }
}
