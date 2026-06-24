namespace ET.TeamGame
{
    /// <summary>
    /// 单位身份组件 — 挂载在 Unit 上，记录类型、名称、等级
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class IdentityComponent : Entity, IAwake, IDestroy
    {
        /// <summary>单位类型：英雄/怪物/NPC</summary>
        public UnitType UnitType { get; set; }

        /// <summary>显示名称</summary>
        public string Name { get; set; }

        /// <summary>等级</summary>
        public int Level { get; set; } = 1;

        /// <summary>高度层（0=地面,1=低空,2+=高空）</summary>
        public int Height { get; set; }
    }
}
